using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public CharacterController controller; // Referentie naar de CharacterController
    public Camera playerCamera; // Referentie naar de camera
    public float speed = 5f; // Snelheid van de speler
    public float lookSpeed = 2f; // Kijk snelheid
    public float sensitivity = 2f; // Gevoeligheid van de muis
    private float verticalRotation = 0f; // Houdt de verticale rotatie bij

    void Update()
    {
        // Bewegingsinput
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Maak een beweging vector
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Beweeg de CharacterController
        controller.Move(move * speed * Time.deltaTime);

        // Muis beweging
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Verticale rotatie (op en neer)
        verticalRotation -= mouseY * sensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Beperk de kijkhoek

        // Rotatie van de camera
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        // Rotatie van de speler
        transform.Rotate(Vector3.up * mouseX);
    }
}
