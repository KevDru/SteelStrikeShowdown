using UnityEngine;

public class MaintainDistance : MonoBehaviour
{
    public string playerTag = "Player";
    public float desiredDistance = 2f;
    public float moveSpeed = 5f;
    public float stopThreshold = 0.1f;

    private Transform player;

    void Start()
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

    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player);

            Vector3 direction = transform.position - player.position;
            float currentDistance = direction.magnitude;

            if (Mathf.Abs(currentDistance - desiredDistance) > stopThreshold)
            {
                direction = direction.normalized;

                float targetY = transform.position.y;

                Vector3 targetPosition = player.position + direction * desiredDistance;
                targetPosition.y = targetY;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }
}
