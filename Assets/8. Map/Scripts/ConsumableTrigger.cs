using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableTrigger : MonoBehaviour
{
    private Consumable _consumable;

    private void Start()
    {
        _consumable = gameObject.GetComponentInParent<Consumable>();
        if (!_consumable)
            Debug.LogError("Consumable Component not found in parent");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        _consumable.Trigger(collision);
    }
}
