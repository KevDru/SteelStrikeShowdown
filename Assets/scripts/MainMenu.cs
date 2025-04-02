using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera startCamera;
    public Camera[] allCameras;
    public GameObject crosshairUI; // Zet hier de Crosshair UI GameObject in de Inspector

    void Start()
    {
        // Zet alle gameplay-camera's uit en alleen de startCamera aan
        foreach (Camera cam in allCameras)
        {
            cam.gameObject.SetActive(false);
        }
        startCamera.gameObject.SetActive(true);

        // Zet de crosshair UI uit in het menu
        if (crosshairUI != null)
        {
            crosshairUI.SetActive(false);
        }
    }

    public void PlayGame()
    {
        // Activeer alle camera's en start de game
        foreach (Camera cam in allCameras)
        {
            cam.gameObject.SetActive(true);
        }

        // Activeer de crosshair UI
        if (crosshairUI != null)
        {
            crosshairUI.SetActive(true);
        }

        SceneManager.LoadScene("Save1");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
}
