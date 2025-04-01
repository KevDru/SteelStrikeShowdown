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
        Debug.Log($"{gameObject.name} took {amount} damage, current health: {currentHealth}"); // Debug log
        if (currentHealth <= 0)
        {
            //delay for animation finish
           Invoke(nameof(Death), 1f);
        }
    }


    void Death()
    {

        Debug.Log(gameObject.name + " has died! Destroying...");
       
        ShowVictoryMenu();
        Destroy(gameObject);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ShowVictoryMenu()
    {
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(true);  
        }
    }
}