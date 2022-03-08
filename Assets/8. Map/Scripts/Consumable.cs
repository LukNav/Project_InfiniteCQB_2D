using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public float respawnDelay;
    public GameObject consumableBody;
    private ParticleSystem _particleSystem;
    public delegate void HealthConsumableDelegate(float healAmount);
    public static HealthConsumableDelegate healthConsumableDelegate;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        if (!_particleSystem)
            Debug.LogError("No Particle system was found");
    }
    public void Trigger(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            return;

        _particleSystem.Play();
        consumableBody.SetActive(false);
        Invoke("Respawn", 5f);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            healthConsumableDelegate(20f);
    }

    private void Respawn()
    {
        consumableBody.SetActive(true);
    }
}
