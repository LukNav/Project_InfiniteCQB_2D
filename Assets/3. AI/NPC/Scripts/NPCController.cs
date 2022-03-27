using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour, IBTScanningController
{
    //Scriptable Object properties:
    public float CalmRotationSpeed = 500f;
    public float QuickRotationSpeed = 1000f;


    public bool shouldFollowTarget = true;

    [Header("Scan settings")]
    public float scanDelay = 2f;
    public float angleChangeDelay = 3;
    public AnimationCurve rotationSpeedCurve;
    //public GameObject weaponGO;


    public float scanningRotationSpeed { get { return CalmRotationSpeed; } }
    

    //private NavMeshAgent _agent;
    private FieldOfView _fovController;
    private ShootingController _shootingController;
    //private NPCAnimator _animatorController;
    public StatsController statsController { get; private set; }
    private BTSelector _rootBT;
    private Timer scanTimer;
    private Timer angleChangeTimer;

    public float elapsedRotationTime = 0f;

    public bool isRotating { get { return elapsedRotationTime < 1; } }
    public bool isScanning = false;
    public bool isFollowingTarget = false;

    private Vector3 _rotationDiorection;
    public Vector3 rotationDirection
    {
        get
        {
            return _rotationDiorection;
        }
        set
        {
            elapsedRotationTime = 0f;
            _rotationDiorection = value;
        }
    }

    public float scanningRotationAngle { get; set; }
    public Quaternion rotation
    {
        get 
        { 
            return transform.rotation; 
        }
        set
        {
            transform.rotation = value;
        }
    }

    public bool hasRotated
    {
        get
        {
            return Mathf.DeltaAngle(transform.rotation.eulerAngles.z, scanningRotationAngle) < 0.005f;
        }
    }

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {

        GetComponent<StatsController>().deathDelegate += OnDeath;

        //_agent = GetComponent<NavMeshAgent>();
        //if(_agent == null)
        //{
        //    Debug.LogError("Missing component: NavMeshAgent");
        //}

        _shootingController = GetComponent<ShootingController>();
        if (_shootingController == null)
        {
            Debug.LogError("Missing component: ShootingController");
            return;
        }

        //_animatorController = GetComponent<NPCAnimator>();
        //if (_animatorController == null)
        //{
        //    Debug.LogError("Missing component: NPCAnimator");
        //}

        statsController = GetComponent<StatsController>();
        if (statsController == null)
        {
            Debug.LogError("Missing component: StatsController");
            return;
        }

        _fovController = GetComponent<FieldOfView>();
        if (_fovController == null)
        {
            Debug.LogError("Missing component: FieldOfView");
            return;
        }

        scanTimer = new Timer(scanDelay);
        angleChangeTimer = new Timer(angleChangeDelay);


        BTSequence followTargetSequence = new BTSequence(new List<BTNode>
        {
            new BTCanSeeTarget(_fovController),
            new BTTimer_Stop(scanTimer),
            new BTTimer_Stop(angleChangeTimer),
            new BTRotateToTarget(this, _fovController),
            new BTMoveToTarget(_fovController, this),
            new BTShootTarget(_shootingController, _fovController)
        });

        BTSequence resetFollowTargetStates = new BTSequence(new List<BTNode>
        {
            new BTSetFollowTargetStateFalse(this)
        });

        BTSequence resetScanningStates = new BTSequence(new List<BTNode>
        {
            new BTTimer_Stop(scanTimer),
            new BTTimer_Stop(angleChangeTimer)
        });

        BTSequence isHitSequence = new BTSequence(new List<BTNode>
        {
            new BTIsHit(statsController),
            resetFollowTargetStates,
            resetScanningStates,
            new BTRotateToHitDirection(this)
        });

        BTSequence scanSequence = new BTSequence(new List<BTNode>
        {
            resetFollowTargetStates,
            new BTTimer_Start(scanTimer),
            new BTSelector(new List<BTNode>//yEd Graph name: DelayTheScanningStart
            {
                new BTSequence(new List<BTNode>
                {
                    new BTTimer_HasEnded(scanTimer),
                    new BTTimer_Start(angleChangeTimer),
                    new BTSelector(new List<BTNode> //yEd Graph name: AngleChangeDelay
                    {
                        new BTSequence(new List<BTNode>
                        {
                            new BTIsRotatingToScanningAngle(this),
                            new BTRotateToScanningAngle(this)
                        }),
                        new BTSequence(new List<BTNode>
                        {
                            new BTTimer_HasEnded(angleChangeTimer),
                            new BTSetScanningAngle(this, 160f, true),
                            new BTTimer_Restart(angleChangeTimer)
                        }),
                        
                    }),
                }),
                new BTSuccess()
            })
        });

        _rootBT = new BTSelector(new List<BTNode>
        {
            followTargetSequence,
            isHitSequence,
            scanSequence
        });


    }

    private void OnDeath()
    {
        //weaponGO.SetActive(false);
        isDead = true;
        if(_followPathCoroutine != null)
            StopCoroutine(_followPathCoroutine);
        Destroy(gameObject, Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFollowTarget)
            _rootBT.Evaluate();

        scanTimer.Update();
        angleChangeTimer.Update();

        RotateToSetAngle();
    }


    private void RotateToSetAngle()
    {
        if (elapsedRotationTime > 1)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotationDirection);
        elapsedRotationTime += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeedCurve.Evaluate(elapsedRotationTime));
    }

    Coroutine _followPathCoroutine;
    public float speed = 1f;

    public void FollowTarget(Vector3 targetPos)
    {
        PathRequestManager.RequestPath(transform.position, targetPos, OnPathFound);
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && !isDead)
        {
            if (_followPathCoroutine != null)
                StopCoroutine(_followPathCoroutine);
            _followPathCoroutine = StartCoroutine(FollowPath(newPath));
        }
    }

    IEnumerator FollowPath(Vector3[] path)
    {
        Vector3 currentWaypoint = path[0];
        int targetIndex = 0;
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDisable()
    {
        _fovController.enabled = false;
    }
}
