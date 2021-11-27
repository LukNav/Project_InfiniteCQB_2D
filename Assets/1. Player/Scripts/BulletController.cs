using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public float damage = 15f;
    public float selfDestroyTime = 2f;

    private bool _isActive = false;
    private Vector3 _lastPos;
    private RaycastHit _hit;
    //We don't want to destroy and instantiate bullets all the time - we want to enable and disable them, which is more effective
    private void OnEnable()
    {
        trailRenderer.Clear();
        Invoke("DestroySelf", selfDestroyTime);
        _isActive = true;
        _lastPos = Vector3.down;
    }

    private void OnDisable()
    {
        CancelInvoke("DestroySelf");
        _isActive = false;
    }

    public void Update()
    {
        if (_isActive)// PROBLEM <--- This activates when instantiating it - could affect the performance, could be optimised
        {
            if (_lastPos == Vector3.down)
            {
                _lastPos = transform.position;
                return;
            }

            Vector3 velocityDir = transform.position - _lastPos;
            RaycastHit _hit;

            if (Physics.Raycast(transform.position, velocityDir, out _hit, 0.3f))
            {
                OnHit(_hit.collider, _hit);
            }
        }
    }

    public void OnHit(Collider other, RaycastHit hit)
    {
        Debug.Log("Bullet HIT");
        StatsController statsController = other.GetComponentInParent<StatsController>();
        if (statsController != null)
        {
            Vector3 hitDir =  transform.position - hit.point;//This direction is from the contact's perspective
            statsController.TakeDamage(damage, hit.point, hitDir);
        }
        DestroySelf();
    }

    public void OnCollisionEnter(Collision collision)
    {
    OnHit(collision.collider, _hit);

    }



    private void DestroySelf()
    {
        gameObject.SetActive(false);
    }

}
