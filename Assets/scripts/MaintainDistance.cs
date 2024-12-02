using UnityEngine;

public class MaintainDistance : MonoBehaviour
{
    public string playerTag = "Player"; // Tag to identify the player
    public float desiredDistance = 2f; // The distance to maintain
    public float moveSpeed = 5f; // Enemy speed
    public float stopThreshold = 0.1f; // Threshold for fine-tuning stop distance

    private Transform player; // Player's Transform

    void Start()
    {
        // Find the player by tag
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

    void Update()
    {
        if (player != null)
        {
            // Calculate the vector between the enemy and the player
            Vector3 direction = transform.position - player.position;
            float currentDistance = direction.magnitude;

            // If the enemy is too close or too far, adjust the position
            if (Mathf.Abs(currentDistance - desiredDistance) > stopThreshold)
            {
                // Normalize the direction and calculate the target position
                direction = direction.normalized;
                Vector3 targetPosition = player.position + direction * desiredDistance;

                // Move the enemy towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }
}
