using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        DealDamage(other.gameObject);
    }

    protected virtual void DealDamage(GameObject other)
    {
        if (other.CompareTag(targetTag))
        {
            Health foundHealth = other.GetComponentInParent<Health>();
            if (foundHealth != null)
            {
                foundHealth.TakeDamage(damage);
            }
        }
    }
}
