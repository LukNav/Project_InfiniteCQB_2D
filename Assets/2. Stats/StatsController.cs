using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public float health = 100;
    public float initialHealth { get; private set; }

    ////public Animator animator;// move this to other script, e.g. rigid body controller and subscribe to Die event
    //public Collider2D collider;
    //public GameObject weaponCollider; // This is not pretty, but works for demo - Has a task to fix in "Mid-Late Phase" epic
    //public Rigidbody rigidBody;
    //public Rigidbody rigsRigidBody;
    //public GameObject hips;
    //public MonoBehaviour controller;

    public delegate void DeathDelegate();
    public DeathDelegate deathDelegate;

    //public delegate void DamageDelegate();
    //public DamageDelegate damageDelegate;

    public bool isHit { get; private set; }
    public Vector3 hitPoint { get; private set; }
    public Vector3 hitDirection { get; private set; }
    bool isDead { get { return health <= 0; } }

    public void Awake()
    {
        initialHealth = health;
        isHit = false;
        deathDelegate += Die;
    }

    public void TakeDamage(float damage, Vector3 bulletPos)
    {
        isHit = true;
        this.hitPoint = hitPoint;
        this.hitDirection = bulletPos - transform.position;
        Debug.DrawLine(transform.position, transform.position + hitDirection * 5, Color.red, 1f);

        health -= damage;
        if (health <= 0)
        {
            deathDelegate();
            return;
        }
        //damageDelegate();
    }

    private void Die()
    {
        //Implement this on controllers using subscribtions to OnDeath delegate

        ////animator.enabled = false;
        //collider.enabled = false;
        //rigidBody.isKinematic = true;
        
        //hips.SetActive(true);
        ////rigsRigidBody.constraints = RigidbodyConstraints.None;
        //controller.enabled = false;
        //weaponCollider.SetActive(true);

        
        //Destroy(gameObject, Time.deltaTime);//destroy on next frame
    }

    public void ResetHitInfo()
    {
        isHit = false;
        hitPoint = Vector3.zero;
        hitDirection = Vector3.zero;
    }
}
