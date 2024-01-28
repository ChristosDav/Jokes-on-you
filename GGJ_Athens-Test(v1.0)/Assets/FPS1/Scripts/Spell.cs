using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private float damage = 100f;
    [SerializeField] private Vector3 positionOffset = Vector3.zero;
    private Collider col;
    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponentInChildren<Collider>();
        col.isTrigger = true;
    }

    public virtual void Setup(SpellCast caster)
    {
        transform.position =  new Vector3(caster.transform.position.x, 0, caster.transform.position.z) + positionOffset;
        gameObject.SetActive(true);
    }

    public virtual void Activate()
    {
        activated = true;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!activated) return;

        if (other.CompareTag(targetTag))
        {
            DealDamage(other.gameObject);
        }
    }

    private void DealDamage(GameObject target)
    {
        Health health = target.GetComponentInChildren<Health>();
        if (health)
        {
            health.TakeDamage(damage);
        }
    }
}
