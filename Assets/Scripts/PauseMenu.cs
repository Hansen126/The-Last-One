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
    public LevelMenu levelMenu;

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

            int completedChapter = DetermineCompletedChapter();

            int currentProgress = PlayerPrefs.GetInt("ChapterProgress", 0);
            if (completedChapter > currentProgress)
            {
                PlayerPrefs.SetInt("ChapterProgress", completedChapter);
                PlayerPrefs.Save();
                Debug.Log($"Progress updated to chapter: {completedChapter}");
            }
            else
            {
                Debug.Log("No progress update needed.");
            }
        }
    }

    private int DetermineCompletedChapter()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Chapter 1")
            return 1;
        else if (sceneName == "Chapter 2")
            return 2;
        else if (sceneName == "Chapter 3")
            return 3;

        return 0;
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