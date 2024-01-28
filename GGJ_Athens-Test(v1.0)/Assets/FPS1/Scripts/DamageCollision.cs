using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollision : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        DealDamage(collision.gameObject);
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
