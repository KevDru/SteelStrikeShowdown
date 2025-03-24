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

    public float sensX = 2f;
    public float sensY = 2f;
    private Vector3 moveDirection;

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
        }
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // If the enemy exists, lock the camera to face the enemy
        if (enemy != null)
        {
            Vector3 directionToEnemy = enemy.position - transform.position;
            directionToEnemy.y = 0; // Ignore vertical axis for camera rotation

            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}