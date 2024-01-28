using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS_Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro previewDisplay;
    [SerializeField] private string[] messages;
    private string previewMessage;
    public float force = 100f;
    [SerializeField] private AudioClip shootAudio;


    // Start is called before the first frame update
    void Start()
    {
        previewMessage = RandomMessage();
        previewDisplay.text = previewMessage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            animator.SetTrigger("Shoot");
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation, null);
        projectile.SetActive(true);

        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);

        projectile.GetComponentInChildren<TextMeshPro>().text = previewMessage;
        previewMessage = RandomMessage();
        previewDisplay.text = previewMessage;

        AudioSource.PlayClipAtPoint(shootAudio, transform.position);

        Destroy(projectile, 4);
    }

    private string RandomMessage()
    {
        int r = Random.Range(0, messages.Length);
        return messages[r];
    }
}
