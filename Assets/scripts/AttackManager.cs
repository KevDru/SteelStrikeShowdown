using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
=======
    public Camera PlayerCamera;  
>>>>>>> Stashed changes
    public Animator playerAnimator;
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DirectionCheck();
        }

    }

<<<<<<< Updated upstream
    void DirectionCheck()
    {
        if (Input.GetAxis("Mouse X") < 0 && Input.GetAxis("Mouse Y") < 0.15f && Input.GetAxis("Mouse Y") > -0.15f)
=======
    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRayCast()
    {
        RaycastHit hit; // Declare the variable properly
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);

            if (hit.transform.TryGetComponent<Actor>(out Actor T))
            {
                T.TakeDamage(attackDamage);
            }
        }
    }

    public GameObject hitEffectPrefab; // Your prefab to instantiate

    void HitTarget(Vector3 pos)
    {
        GameObject GO = Instantiate(hitEffectPrefab, pos, Quaternion.identity);
        Destroy(GO, 20f); // Destroy after 20 seconds
    }


    void HandleAction(string actionType)
    {

        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);  
        Invoke(nameof(AttackRayCast), attackDelay);  


        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX < 0 && Mathf.Abs(mouseY) < 0.15f)
>>>>>>> Stashed changes
        {
            playerAnimator.SetTrigger("AttackLeft");
        }
        else if (Input.GetAxis("Mouse X") > 0 && Input.GetAxis("Mouse Y") < 0.15f && Input.GetAxis("Mouse Y") > -0.15f)
        {
            playerAnimator.SetTrigger("AttackRight");
        }
        else if (Input.GetAxis("Mouse Y") > 0 && Input.GetAxis("Mouse X") < 0.15f && Input.GetAxis("Mouse X") > -0.15f)
        {
            playerAnimator.SetTrigger("AttackUp");
        }
        else if (Input.GetAxis("Mouse Y") < 0 && Input.GetAxis("Mouse X") < 0.15f && Input.GetAxis("Mouse X") > -0.15f)
        {
            playerAnimator.SetTrigger("AttackDown");
        }
    }
}
