using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public float respawnDelay;
    public GameObject consumableBody;

    public delegate void HealthConsumableDelegate(float healAmount);
    public static HealthConsumableDelegate healthConsumableDelegate;
    public void Trigger(Collider2D collision)
    {
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
