using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenu;
    public GameObject victoryMenu;
    public Player player;

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Pause()
    {
        if (player.isDead)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
        }
        else
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Victory()
    {
        if (player.isDead)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            victoryMenu.SetActive(false);
        }
        else
        {
            isPaused = true;
            victoryMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        victoryMenu.SetActive(false);
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
