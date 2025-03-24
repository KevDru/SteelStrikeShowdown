using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera startCamera;
    public Camera[] allCameras;
    public void PlayGame()
    {
        SceneManager.LoadScene("Save1");
        foreach (Camera cam in allCameras)
        {
            cam.gameObject.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); 
        Application.Quit();
    }
}
