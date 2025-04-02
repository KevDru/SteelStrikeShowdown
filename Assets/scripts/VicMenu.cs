using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mian : MonoBehaviour
{
    public GameObject VictoryScreenCanvas;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenuScript.Paused = true;
        Time.timeScale = 0f;
        VictoryScreenCanvas.SetActive(true);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}