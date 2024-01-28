using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;

    Transform target;

    [SerializeField] private Transform rightHand;
    [SerializeField] private GameObject tomatoPrefab;

    [SerializeField] private AudioClip tomatoAudio;

    public bool running;
    bool onAttack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        onAttack = false;

        animator.SetInteger("RunningIndex", Random.Range(0, 3));
        animator.SetInteger("AttackingIndex", Random.Range(0, 3));
        animator.SetInteger("IdleIndex", Random.Range(0, 2));
        animator.SetInteger("LaughingIndex1", Random.Range(0, 3));
        animator.SetInteger("LaughingIndex2", Random.Range(0, 3));

        int r;
        do
        {
            r = Random.Range(0, transform.childCount - 3);
        } while (r == 8);
        for (int i = 0; i < transform.childCount-3; i++)
        {
            transform.GetChild(i).gameObject.SetActive(r == i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!target || onAttack)
        {
            agent.SetDestination(transform.position);
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }

    public void SawPlayer(GameObject player)
    {
        target = player.transform;

        running = target != null;
        animator.SetBool("Running", running);
    }

    public void LostPlayer(GameObject player)
    {
        target = player.transform == target? null : target;

        running = target != null;
        animator.SetBool("Running", running);
    }

    public void AttackPlayer()
    {
        if (onAttack) return;

        onAttack = true;
        GameObject tomatoObject = Instantiate(tomatoPrefab, transform);
        Tomato tomato = tomatoObject.GetComponent<Tomato>();
        tomato.Setup(transform, rightHand);
        animator.SetTrigger("Attack");
    }

    public void ResetAttack()
    {
        onAttack = false;
    }

     public void ReleaseTomato()
    {
        GetComponentInChildren<Tomato>()?.Release();

        AudioSource.PlayClipAtPoint(tomatoAudio, transform.position);
    }
}
