using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Required for accessing UI elements

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;

    // Reference to the Victory Menu UI (drag the actual Victory Menu object in the Inspector)
    public GameObject victoryMenu;  // This will be your victory menu UI (Canvas)

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log(gameObject.name + " has died! Destroying...");

        // Show the victory menu when the enemy dies
        ShowVictoryMenu();

        // Destroy the enemy game object
        Destroy(gameObject);
    }

    void ShowVictoryMenu()
    {
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(true);  // Enable the victory menu (show it)
        }
    }
}
