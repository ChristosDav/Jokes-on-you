using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int totalEnemies = 50;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Slider[] victorySliders;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private FirstPersonController playerFPS;
    [SerializeField] private AudioClip victoryAudio;

    //[SerializeField] private FPS_Shooter fps;
    //[SerializeField] private SpellCast spellCast;
    public int enemyDefeated;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyDefeated = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < victorySliders.Length; i++)
        {
            victorySliders[i].value = (float)enemyDefeated / (float)totalEnemies;
        }

        if (enemyDefeated >= totalEnemies)
        {
            Victory();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Pause(Time.timeScale != 0);
        }
    }

    public void NotifyEnemyDefeat()
    {
        enemyDefeated++;
    }

    void Victory()
    {
        victoryScreen.SetActive(true);

        playerAnimator.SetTrigger("Death");
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
        }*/

        AudioSource.PlayClipAtPoint(victoryAudio, transform.position);
        playerFPS.enabled = false;

        Invoke("Restart", 12f);
    }

    public void Pause(bool pause)
    {
        playerFPS.enabled = !pause;
        Time.timeScale = pause ? 0 : 1;
        pauseScreen.SetActive(pause);

        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = pause;
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
