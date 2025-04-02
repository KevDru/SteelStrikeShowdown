using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    public GameObject CrosshairCanvas; // Zorg ervoor dat je dit instelt in de Unity Inspector

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (CrosshairCanvas != null)
            CrosshairCanvas.SetActive(true);
    }

    void Update()
    {
        // Voorkomen dat je kunt pauzeren in de Victory Scene of Main Menu
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "VictoryScreen" || sceneName == "Main Menu")
        {
            Paused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (CrosshairCanvas != null) CrosshairCanvas.SetActive(false); // Verberg crosshair
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    public void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (CrosshairCanvas != null)
            CrosshairCanvas.SetActive(false); // Verberg crosshair
    }

    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (CrosshairCanvas != null)
            CrosshairCanvas.SetActive(true); // Toon crosshair weer
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
