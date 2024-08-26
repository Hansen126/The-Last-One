using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class inputDisplay : MonoBehaviour
{

    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject victoryMenu;
    private bool isPaused;
    private bool isVictory;
    public Player player;
    public TMP_Text displayText;
    private string currentInput = "";

    void Start()
    {
        if (displayText == null)
        {
            Debug.LogError("Display Text is not assigned in Start.");
        }
        else
        {
            Debug.Log("Display Text is assigned correctly in Start.");
        }
    }

    void Update()
    {
        isPaused = pauseMenu.activeSelf;
        isVictory = victoryMenu.activeSelf;

        if (player.isDead)
        {
            displayText.text = "";
            currentInput = "";
            return;
        }

        if (isPaused || isVictory)
        {
            return;
        }

        try
        {
            foreach (char c in Input.inputString)
            {
                Debug.Log("Current input character: " + c);

                if (c == '\b')
                {
                    if (currentInput.Length != 0)
                    {
                        currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    }
                }
                else if ((c == '\n') || (c == '\r'))
                {
                    Debug.Log("Enter key pressed. Current input: " + currentInput);
                    currentInput = "";
                }
                else
                {
                    currentInput += c;
                }

                displayText.text = currentInput;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error in InputDisplay script: " + ex.Message);
        }
    }
}
