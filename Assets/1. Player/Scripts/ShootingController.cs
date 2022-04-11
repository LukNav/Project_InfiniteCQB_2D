using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    [Header("GameObject references")]
    public Transform firePoint;
    public GameObject bullet;
    // Start is called before the first frame update

    [Header("Optimization settings")]
    public int bulletPoolSize = 15;// maximum count of pooled bullet objects
    private List<GameObject> _bulletPool; // currently instantiated bullets

    [Header("Visual effects - move this later")]
    public CinemachineImpulseSource cinemachineImpulseSource;
    public ParticleSystem particleSystem;

    [Header("Weapon Settings")]
    //public float rateOfFire = 0.7f; //M1911 pistol Rate of fire is 85 rounds/min, thus 60s/85 = 0.7s delay
    public float bulletSpeed = 10f;
    public float fireRate = 0.3f;
    private float shootTimer;
    private bool isSprinting = false;
    private bool _isWeaponDrawn = false;
    private bool isPlayer;

    void Start()
    {
        _bulletPool = new List<GameObject>();
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject go = Instantiate(bullet);
            go.SetActive(false);
            _bulletPool.Add(go);
        }
        shootTimer = 0f;
        isPlayer = LayerMask.NameToLayer("Player") == gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.started || context.performed;
    }

    public void Fire()
    {
        if(shootTimer > fireRate)
        {
            shootTimer = 0f;

            for (int i = 0; i < _bulletPool.Count; i++)
            {
                if (!_bulletPool[i].activeInHierarchy)
                {
                    if(isPlayer)
                        cinemachineImpulseSource.GenerateImpulse();
                    particleSystem.Play();

                    _bulletPool[i].transform.position = firePoint.position;
                    _bulletPool[i].transform.rotation = firePoint.rotation;
                    _bulletPool[i].SetActive(true);

                    //if (!isSprinting)
                    //{
                    //    ShootTowardMouse(i);
                    //    break;
                    //}

                    _bulletPool[i].GetComponent<Rigidbody2D>().velocity = firePoint.up * bulletSpeed;
                    break;
                }
            }
        }

        
    }

    public void ShootTowardMouse(int bulletPoolIndex)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());//---THIS COULD BE REUSED FROM PlayerController!!!!!!!!
        Plane plane = new Plane(Vector3.forward, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 firepointPos = firePoint.position;

            Vector3 targetPos = ray.GetPoint(distance);
            targetPos.z = firepointPos.z;
            Vector3 direction = targetPos - firepointPos;
            direction.Normalize();
            _bulletPool[bulletPoolIndex].GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }

    public void Fire(Transform target)
    {
        if (shootTimer > fireRate)
        {
            shootTimer = 0f;

            for (int i = 0; i < _bulletPool.Count; i++)
            {
                if (!_bulletPool[i].activeInHierarchy)
                {
                    particleSystem.Play();

                    _bulletPool[i].transform.position = firePoint.position;
                    _bulletPool[i].transform.rotation = firePoint.rotation;
                    _bulletPool[i].SetActive(true);

                    if (!isSprinting)
                    {
                        ShootTarget(i, target);
                        break;
                    }

                    _bulletPool[i].GetComponent<Rigidbody2D>().velocity = firePoint.up * bulletSpeed;
                    break;
                }
            }
        }
    }

    public void ShootTarget(int bulletPoolIndex, Transform target)//used mostly for NPCs
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());//---THIS COULD BE REUSED FROM PlayerController!!!!!!!!
        Plane plane = new Plane(Vector3.forward, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPos = target.position;
            Vector3 direction = targetPos - firePoint.position;
            direction.Normalize();
            _bulletPool[bulletPoolIndex].GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }
}
