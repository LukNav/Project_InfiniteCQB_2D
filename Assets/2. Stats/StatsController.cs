using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StatsController : MonoBehaviour// NOTE - player must be in layer of Player in order for this script to work with UI -- need to think of something more clear
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

    public delegate void HealthUpdateDelegate();
    public HealthUpdateDelegate healthUpdateDelegate;

    public bool isHit { get; private set; }
    public Vector3 hitDirection { get; private set; }
    private bool isDead { get { return health <= 0; } }
    private bool isPlayer;

    public void Awake()
    {
        initialHealth = health;
        isHit = false;
        deathDelegate += Die;
        Consumable.healthConsumableDelegate += Heal;
        isPlayer = LayerMask.NameToLayer("Player") == gameObject.layer;
    }

    public void TakeDamage(float damage, Vector3 hitDirection)
    {
        isHit = true;
        this.hitDirection = hitDirection;
        //Debug.DrawLine(transform.position, transform.position + hitDirection * 5, Color.red, 1f);

        health -= damage;
        if (health <= 0)
        {
            deathDelegate();
            return;
        }
        if(isPlayer)
            healthUpdateDelegate();
    }

    public void Heal(float healAmount)
    {
        health = Mathf.Clamp(health + healAmount, 0f, initialHealth);
        if (isPlayer)
        {
            healthUpdateDelegate();
        }        
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

        
        //Destroy(gameObject, Time.deltaTime);///destroy on next frame
    }

    public void ResetHitInfo()
    {
        isHit = false;
        hitDirection = Vector3.zero;
    }

    void OnGUI()
    {
        if (isPlayer && GUI.Button(new Rect(10, 100, 100, 30), "Get Consumable"))
        {
            Consumable.healthConsumableDelegate(20);
        }
    }
}
