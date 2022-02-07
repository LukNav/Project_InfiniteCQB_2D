using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public float damage = 15f;
    public float selfDestroyTime = 2f;
    public float selfDestroyDistance = 0.3f;

    private bool _isActive = false;
    private Vector2 _lastPos;
    private RaycastHit2D _rayHit;
    //We don't want to destroy and instantiate bullets all the time - we want to enable and disable them, which is more effective
    private void OnEnable()
    {
        trailRenderer.Clear();
        Invoke("DestroySelf", selfDestroyTime);
        _isActive = true;
        _lastPos = Vector2.down;
    }

    private void OnDisable()
    {
        CancelInvoke("DestroySelf");
        _isActive = false;
    }

    public void Update()
    {
        _lastPos = new Vector2(transform.position.x, transform.position.y);
        if (_isActive)// PROBLEM <--- This activates when instantiating it - could affect the performance, could be optimised
        {
            if (_lastPos == Vector2.down)
            {
                _lastPos = transform.position;
                return;
            }

            Vector3 velocityDir =  - _lastPos;
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, velocityDir, selfDestroyDistance);

            if (rayHit)//Convert this to 2D
            {
                OnHit(_rayHit.collider, rayHit);
            }
        }
    }

    public void OnHit(Collider2D other, RaycastHit2D hit)
    {
        Debug.Log("Bullet HIT");
        StatsController statsController = other.GetComponentInParent<StatsController>();
        if (statsController != null)
        {
            Vector3 hitDir = _lastPos - hit.point;//This direction is from the contact's perspective
            statsController.TakeDamage(damage, hit.point, hitDir);
        }
        DestroySelf();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit(collision.collider, _rayHit);
    }



    private void DestroySelf()
    {
        gameObject.SetActive(false);
    }

}
