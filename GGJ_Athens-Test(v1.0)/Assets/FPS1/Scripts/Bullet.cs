using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : DamageCollision
{
    [SerializeField] private Animator animator;
    Transform fx;
    bool activated;

    private void Start()
    {
        activated = true;
    }

    protected override void DealDamage(GameObject other)
    {

        if (!activated) return;

        activated = false;

        base.DealDamage(other);

        animator.SetTrigger("Hit");

        fx = transform.GetChild(0);
        fx.gameObject.SetActive(true);
        fx.parent = null;
        fx.localScale = Vector3.one;
    }

    public void Die()
    {
        if (!activated) return;
        Destroy(fx);
        StartCoroutine("DieEnd");
    }

    IEnumerator DieEnd()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
