using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [Space]
    [Header("Player Health properties")]
    [Space]
    [SerializeField] bool immune;
    [SerializeField] Image healthScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Animator animator;
    [SerializeField] private AudioClip dieAudio;
    [SerializeField] private AudioClip damageAudio;

    protected override void Start()
    {
        base.Start();

        immune = false;

        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
        if (currentHealth <= 0)
        {
            Die();
        }
        currentHealth += Time.deltaTime * 4f;
        if (currentHealth > totalHealth) currentHealth = totalHealth;
    }

    public override void TakeDamage(float damage)
    {
        if (!alive) return;

        if (immune) return;

        //currentHealth -= damage;
        //if (currentHealth < 0) currentHealth = 0;

        currentHealth = Mathf.Max(currentHealth - damage, 0f);

        UpdateUI();

        AudioSource.PlayClipAtPoint(damageAudio, transform.position);

        //animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float value)
    {
        currentHealth = Mathf.Min(currentHealth + value, totalHealth);

        UpdateUI();
    }

    void UpdateUI()
    {
        Color c = healthScreen.color;
        c.a = (1 - currentHealth / totalHealth);
        healthScreen.color = c;
    }

    protected override void Die()
    {
        base.Die();

        Debug.Log("Game Over");

        animator.SetTrigger("Death");

        AudioSource.PlayClipAtPoint(dieAudio, transform.position);

        /*
        foreach (Component component in GetComponentsInChildren<Component>())
        {
            if (component.GetType() != typeof(SkinnedMeshRenderer)
                && component.GetType() != typeof(Transform)
                && component.GetType() != typeof(Animator)
               )
            {
                Debug.Log(component.ToString());
                Destroy(component);
            }
        }
        */

        Invoke("Restart", 5f);

        foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
        {
            enemy.LostPlayer(gameObject);
        }
        GameObject.FindObjectsOfType<Enemy>();

        gameOverScreen.SetActive(true);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
