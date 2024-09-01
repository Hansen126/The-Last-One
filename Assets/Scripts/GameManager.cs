using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelMenu levelMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void CompleteChapter(int chapter)
    {
        int currentProgress = PlayerPrefs.GetInt("ChapterProgress", 0);

        Debug.Log($"Current progress: {currentProgress}, Attempting to complete chapter: {chapter}");

        if (chapter > currentProgress)
        {
            PlayerPrefs.SetInt("ChapterProgress", chapter);
            PlayerPrefs.Save();
            Debug.Log($"Progress updated to chapter: {chapter}");
        }
        else
        {
            Debug.Log("No progress update needed.");
        }
    }

    public bool IsChapterCompleted(int chapter)
    {
        int chapterProgress = PlayerPrefs.GetInt("ChapterProgress", 0);
        Debug.Log($"Checking if chapter {chapter} is completed. Current saved progress: {chapterProgress}");
        return chapterProgress >= chapter;
    }

}