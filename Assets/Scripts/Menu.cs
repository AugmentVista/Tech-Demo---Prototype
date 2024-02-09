using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private bool isPaused = false;
    private Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
        if (isPaused)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void OnButtonClick()
    {
        if (isPaused)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
        if (!isPaused)
        {
            if (canvas != null)
            {
                //canvas.gameObject.SetActive(true);
                //Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1.0f;
            }
        }
        else
        {
            if (canvas != null)
            {
                //canvas.gameObject.SetActive(false);
                //Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 0.0f;
            }
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
}


