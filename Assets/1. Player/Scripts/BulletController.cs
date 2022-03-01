using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public LayerMask colliderLayers;
    public float damage = 15f;
    public float selfDestroyTime = 2f;
    public float selfDestroyDistance = 0.3f;

    private bool _isActive = false;
    private RaycastHit2D _rayHit;

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
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, velocityDir.normalized, selfDestroyDistance, colliderLayers);
            if (rayHit)
            {
                OnHit(rayHit.collider, rayHit.point);
            }
        }
    }
     
    public void OnHit(Collider2D other, Vector2 hitPoint)
    {
        Debug.Log("Bullet HIT");
        StatsController statsController;
        if(other.transform.parent.TryGetComponent<StatsController>(out statsController))
            statsController.TakeDamage(damage, hitPoint);
        else if(other.transform.TryGetComponent<StatsController>(out statsController))
            statsController.TakeDamage(damage, hitPoint);

        DestroySelf();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit(collision.collider, collision.GetContact(0).point);
    }



    private void DestroySelf()
    {
        gameObject.SetActive(false);
    }

}
