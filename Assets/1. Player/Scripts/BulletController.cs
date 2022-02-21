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
    private RaycastHit2D _rayHit;
    //We don't want to destroy and instantiate bullets all the time - we want to enable and disable them, which is more effective
    private void OnEnable()
    {
        trailRenderer.Clear();
        Invoke("DestroySelf", selfDestroyTime);
        _isActive = true;
    }

    private void OnDisable()
    {
        CancelInvoke("DestroySelf");
        _isActive = false;
    }

    public void Update()
    {
        if (_isActive)
        {
            Vector3 velocityDir = gameObject.GetComponent<Rigidbody2D>().velocity;//constantly calculating - really inefficient since bullet's trajectory doesn't really change does it?
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, velocityDir.normalized, selfDestroyDistance);
            if (rayHit)
            {
                OnHit(_rayHit.collider, rayHit);
            }
        }
    }
     
    public void OnHit(Collider2D other, RaycastHit2D hit)
    {
        Debug.Log("Bullet HIT");
        StatsController statsController;
        
        if (other.transform.parent != null && other.transform.parent.TryGetComponent<StatsController>(out statsController))
        {
            statsController.TakeDamage(damage, hit.point);
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
