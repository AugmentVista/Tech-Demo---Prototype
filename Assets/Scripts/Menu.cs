using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    private bool isPaused = false;
    private GameObject resumeButton;
    private GameObject abilityReminder;
    private GameObject endScreen;
    public GameObject EndSkull;
    private bool MenuIsOpen = false;
    private bool MenuActive = false;

    void Start()
    {
        firstPersonController = FindObjectOfType<FirstPersonController>();
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            resumeButton = canvas.transform.Find("Resume Button").gameObject;
            abilityReminder = canvas.transform.Find("Jump Reminder Border").gameObject;
            endScreen = canvas.transform.Find("GameOver").gameObject;

            abilityReminder.SetActive(false);
            resumeButton.SetActive(false);
            endScreen.SetActive(false);
        }
    }
    public void OnPauseButtonClick()
    {
        if (!isPaused)
        {
            isPaused = true;
        }
        if (isPaused)
        {
            Time.timeScale = 0.0f;
        }
    }
    public void OnResumeButtonClick()
    {
    if (isPaused)
    {
        isPaused = false;
    }
    if (!isPaused)
    {
        Time.timeScale = 1.0f;
    }
    MenuIsOpen = false;
    }

    void Update()
    {
        if (!EndSkull.activeSelf)
        { 
        endScreen.SetActive(true);
        MenuIsOpen = true;
        resumeButton.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            MenuIsOpen = true;
            if (MenuIsOpen && Input.GetKeyDown(KeyCode.Escape) && MenuActive)
            { 
            MenuIsOpen = false;
            }
        }
        if (MenuIsOpen)
        {
            firstPersonController.cameraCanMove = false;
            resumeButton.SetActive(true);
            abilityReminder.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            MenuActive = true;
            Time.timeScale = 0.0f;
        }
        else if (!MenuIsOpen)
        {
            Time.timeScale = 1.0f;
            firstPersonController.cameraCanMove = true;
            resumeButton.SetActive(false);  
            abilityReminder.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            MenuActive = false;
        }
    }
}