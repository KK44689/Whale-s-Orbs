using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public static bool gameIsPause = false;

    public GameObject PauseMenu;

    public GameObject PauseButton;

    private PlayerController playerScript;

    void Start()
    {
        playerScript =
            GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerScript.isGameActive)
        {
            PauseButton.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BacktoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1f;
        gameIsPause = false;
    }
}
