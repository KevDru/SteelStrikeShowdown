using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // Referentie naar de CharacterController
    public Animator animator; // Referentie naar de Animator
    public float speed = 5f; // Snelheid van de speler

    void Update()
    {
        // Verkrijg input van de speler
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Maak een vector voor de beweging
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Beweeg de CharacterController
        controller.Move(move * speed * Time.deltaTime);

        // Update de animator
        if (move.magnitude > 0)
        {
            animator.SetBool("isWalking", true); // Loop animatie
        }
        else
        {
            animator.SetBool("isWalking", false); // Idle animatie
        }
    }
}
