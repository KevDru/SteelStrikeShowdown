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
    public float lockOnRadius = 10f; // Radius to detect enemies
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        HandleMovement();
        HandleLockOn();
    }

    void HandleMovement()
    {
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
        Collider[] enemies = Physics.OverlapSphere(transform.position, lockOnRadius, LayerMask.GetMask("Enemy"));
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    void LockOntoTarget()
    {
        if (lockOnTarget != null)
        {
            Vector3 direction = lockOnTarget.position - transform.position;
            direction.y = 0; // Keep the rotation horizontal
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);

            playerCamera.transform.LookAt(lockOnTarget.position);
        }
    }
}
