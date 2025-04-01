using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management

public class VictoryMenuHandler : MonoBehaviour
{
    // Reference to the victory menu UI (Canvas or Panel)
    public GameObject victoryMenu;

    // Called when the enemy is destroyed
    public void ShowVictoryMenu()
    {
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(true);  // Show the victory menu
        }
    }

    // Called when the "Go to Menu" button is clicked
    public void GoToMenu()
    {
        // Load the main menu scene (replace "MainMenu" with the actual name of your main menu scene)
        SceneManager.LoadScene("MainMenu");
    }
}

