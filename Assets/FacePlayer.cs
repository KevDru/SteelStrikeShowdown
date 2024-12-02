using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public string playerTag = "Player"; // Tag used to identify the player
    public float rotationSpeed = 5f; // Speed at which the enemy turns to face the player

    void Start()
    {
        // Automatically find the player using the tag
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag(playerTag);
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player not found! Ensure the player has the correct tag.");
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Calculate the target rotation
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
