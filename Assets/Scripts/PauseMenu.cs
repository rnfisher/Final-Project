using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public  bool GameIsPaused;
    public GameObject pauseMenuUI;
    public AudioSource musicSource;

    private void Start()
    {
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        musicSource.volume = 0.3f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused == true)
            {
                musicSource.volume = 1f;
                Resume();
            }
            else if (GameIsPaused == false)
            {
                musicSource.volume = 0.3f;
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;

        if (GameObject.Find("Player").GetComponent<PlayerScript>().end != true)
        {
            Time.timeScale = 1f;
        }
    }
    void Pause()

    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;

        if (GameObject.Find("Player").GetComponent<PlayerScript>().end != true)
        {
            Time.timeScale = 0f;
        }
    }
}
