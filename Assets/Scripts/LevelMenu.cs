using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    public Button chapter2Button;
    public Button chapter3Button;
    public Button endlessButton;
    int currentProgress;
    int counter = 0;


    void Start()
    {
        UpdateButtonsState();
    }

    void Update()
    {

    }

    public void UpdateButtonsState()
    {
        Debug.Log("Updating button states based on saved progress...");

        if (currentProgress != 13)
        {
            currentProgress = PlayerPrefs.GetInt("ChapterProgress", 0);
        }
        Debug.Log($"Current saved progress: {currentProgress}");

        chapter2Button.interactable = currentProgress >= 1;
        chapter3Button.interactable = currentProgress >= 2;
        endlessButton.interactable = currentProgress >= 3;

        UpdateButtonUI(chapter2Button, 1, "Chapter 2");
        UpdateButtonUI(chapter3Button, 2, "Chapter 3");
        UpdateButtonUI(endlessButton, 3, "Endless");

        Debug.Log($"Chapter 2 unlocked: {chapter2Button.interactable}");
    }

    void UpdateButtonUI(Button button, int requiredChapter, string defaultText)
    {
        bool isUnlocked = button.interactable;

        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>(true);
        if (!isUnlocked)
        {
            buttonText.text = "Locked";
            button.interactable = false;
        }
        else
        {
            buttonText.text = defaultText;
            button.interactable = true;
        }
    }

    public void OnChapterComplete(int chapterNumber)
    {
        Debug.Log($"OnChapterComplete called for chapter: {chapterNumber}");

        int currentProgress = PlayerPrefs.GetInt("ChapterProgress", 0);
        if (chapterNumber > currentProgress)
        {
            PlayerPrefs.SetInt("ChapterProgress", chapterNumber);
            PlayerPrefs.Save();
            Debug.Log($"Progress updated to chapter: {chapterNumber}");
        }

        UpdateButtonsState();
    }

    public void ResetProgress()
    {
        counter += 1;
        if (counter == 13)
        {
            currentProgress = 13;
            Debug.Log("All Level Unlocked.");
            UpdateButtonsState();
            counter = 0;
        }
        else
        {
            currentProgress = 0;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("Player progress has been reset. Counter: " + counter + "/13");

            UpdateButtonsState();
        }
    }

}
