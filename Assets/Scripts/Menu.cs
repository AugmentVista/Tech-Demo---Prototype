using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    private bool isPaused = false;
    private GameObject pauseButton;
    private GameObject resumeButton;
    private bool MenuIsOpen = false;
    float menuOpenDuration = 1.0f;
    float timeElapsed = 0.0f;

    void Start()
    {
        firstPersonController = FindObjectOfType<FirstPersonController>();
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            pauseButton = canvas.transform.Find("Pause Button").gameObject;
            resumeButton = canvas.transform.Find("Resume Button").gameObject;
            pauseButton.SetActive(false);
            resumeButton.SetActive(false);
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
    void MenuTimeSlow()
    {
        if (MenuIsOpen && !isPaused)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / menuOpenDuration);
            Time.timeScale = Mathf.Lerp(1.0f, 0.001f, t);
        }
        if (!MenuIsOpen)
        { 
            Time.timeScale = 1.0f;
            timeElapsed = 0.0f;
        }
    }
    void Update()
    {
        MenuTimeSlow();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuIsOpen)
            {
                MenuIsOpen = false;
            }
            else
            { 
                MenuIsOpen = true;
            }
            Debug.Log("Menu is" + MenuIsOpen);
        }
        if (MenuIsOpen)
        {
            firstPersonController.cameraCanMove = false;
            Debug.Log("Menu is open");
            pauseButton.SetActive(true);
            resumeButton.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        }
        else if (!MenuIsOpen)
        {
            firstPersonController.cameraCanMove = true;
            pauseButton.SetActive(false);
            resumeButton.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}