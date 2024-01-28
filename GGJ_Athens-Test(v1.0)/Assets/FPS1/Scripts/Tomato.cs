using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    private Rigidbody rig;
    private Transform enemyParent;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float force = 10f;
    private bool released;

    // Start is called before the first frame update
    void Start()
    {
        released = false;
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!released) transform.position = transform.parent.position;
    }

    public void Setup(Transform enemy, Transform hand)
    {
        enemyParent = enemy;
        transform.parent = hand.Find("TomatoPivot");
    }

    public void Release()
    {
        released = true;
        transform.parent = null;
        rig.AddForce((enemyParent.forward + Vector3.up * 0.6f) * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponentInParent<PlayerHealth>();
            if (health)
            {
                health.TakeDamage(damage);
            }
        }

        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject, 15f);
            Destroy(this);
        }

        
    }
}
