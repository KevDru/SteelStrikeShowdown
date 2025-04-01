using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float lockOnRadius = 1000; // Radius to detect enemies
    public Transform lockOnTarget = null; // Current locked-on enemy

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!PauseMenuScript.Paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            HandleMovement();
            HandleLockOn();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void HandleMovement()
    {
        if (PauseMenuScript.Paused) return;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove && lockOnTarget == null) // Normal camera movement
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        else if (lockOnTarget != null) // Lock-on system
        {
            LockOntoTarget();
        }
    }

    void HandleLockOn()
    {
        if (PauseMenuScript.Paused) return;

        if (Input.GetKeyDown(KeyCode.Q)) // Press Q to toggle lock-on
        {
            if (lockOnTarget == null)
            {
                lockOnTarget = FindClosestEnemy();
            }
            else
            {
                lockOnTarget = null;
            }
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Find all objects with "Enemy" tag
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && distance <= lockOnRadius) // Check if within lock-on range
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    void LockOntoTarget()
    {
        if (PauseMenuScript.Paused) return;

        if (lockOnTarget != null)
        {
            Vector3 direction = lockOnTarget.position - transform.position;
            direction.y = 0; // Keep the rotation horizontal
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);

            playerCamera.transform.LookAt(lockOnTarget.position);
        }
    }
}
