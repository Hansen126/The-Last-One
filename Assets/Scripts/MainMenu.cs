using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject easyList;
    [SerializeField] private GameObject mediumList;
    [SerializeField] private GameObject hardList;
    [SerializeField] private GameObject hardcoreList;
    [SerializeField] private GameObject settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void easy()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Chapter 1");
    }
    public void medium()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Chapter 2");
    }
    public void hard()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Chapter 3");
    }

    public void hardcore()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Endless");
    }
    public void easyMonsters()
    {
        easyList.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void mediumMonsters()
    {
        mediumList.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void hardMonsters()
    {
        hardList.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void hardcoreWarning()
    {
        hardcoreList.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void backToLevelMenu()
    {
        levelMenu.SetActive(true);
        easyList.SetActive(false);
        mediumList.SetActive(false);
        hardList.SetActive(false);
        hardcoreList.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void playGame()
    {
        levelMenu.SetActive(true);
        mainMenu.SetActive(false);

    }

    public void backToMainMenu()
    {
        levelMenu.SetActive(false);
        mainMenu.SetActive(true);
        settingsPanel.SetActive(false);
    }


    public void goToSettings()
    {
        settingsPanel.SetActive(true);
        mainMenu.SetActive(false); 
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
