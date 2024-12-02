using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController
    public Camera playerCamera; // Reference to the Camera
    public Transform enemy; // The enemy to lock onto
    public string enemyTag = "Enemy"; // Tag for identifying the enemy
    public float speed = 5f; // Player's movement speed
    public float lookSpeed = 2f; // Camera look speed
    public float sensitivity = 2f; // Mouse sensitivity
    private float verticalRotation = 0f; // Vertical camera rotation tracking
    public float rotationSpeed = 5f; // Rotation speed for the camera to follow the enemy

    void Start()
    {
        // Find the enemy if not already assigned
        if (enemy == null)
        {
            GameObject enemyObject = GameObject.FindWithTag(enemyTag);
            if (enemyObject != null)
            {
         
                enemy = enemyObject.transform;
            }
            else
            {
                Debug.LogError("Enemy not found! Ensure an enemy with the tag '" + enemyTag + "' exists in the scene.");
            }
        }
    }

    void Update()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Move the CharacterController
        controller.Move(move * speed * Time.deltaTime);

        // Mouse movement for looking around
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Vertical rotation (up and down)
        verticalRotation -= mouseY * sensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limit vertical look angle

        // Camera rotation (up and down)
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        // Player rotation (left and right)
        transform.Rotate(Vector3.up * mouseX);

        // If the enemy exists, lock the camera to face the enemy
        if (enemy != null)
        {
            // Calculate direction to the enemy
            Vector3 directionToEnemy = enemy.position - transform.position;
            directionToEnemy.y = 0; // Ignore vertical axis for camera rotation

            // Smoothly rotate towards the enemy
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            Debug.DrawLine(transform.position, enemy.position, Color.red);
        }


    }
}
