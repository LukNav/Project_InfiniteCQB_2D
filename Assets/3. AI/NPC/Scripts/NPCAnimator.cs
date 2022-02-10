using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimator : MonoBehaviour
{
    public Animator animator;
    public float animatorSprintAnimation_Pos = 1.5f; //Blend tree named "Movement" has sprint animation, which y position we place here

    [Header("Weapon Animator settings")]
    public Animator rigController;
    public string drawAnimationName = "Weapon_PistolDraw_Anim";
    
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
            Debug.LogError("NavMeshAgent component is missing");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        if (_navMeshAgent != null && _navMeshAgent.path != null && _navMeshAgent.path.corners.Length > 1 && !_navMeshAgent.isStopped)
        {
            //Debug.Log(_navMeshAgent.path.corners[0]);
            direction = _navMeshAgent.path.corners[1] - transform.position;
            direction.Normalize();
            direction = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z) * direction;//rotate the movement direction relative to characters rotation (Can't use euler directly 'transform.rotation' since it is not as accurate for some reason)
        }

        //Debug.Log("remainingDistance: " + _navMeshAgent.remainingDistance + " isStopped: " + _navMeshAgent.isStopped);
        
        //animator.SetFloat("VelocityZ", direction.z, 0.1f, Time.fixedDeltaTime); No animations for you, NPC
        //animator.SetFloat("VelocityX", direction.x, 0.1f, Time.fixedDeltaTime);
        
    }
}
