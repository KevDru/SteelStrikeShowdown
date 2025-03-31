using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Animator playerAnimator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleAction("Attack");
        }
        if (Input.GetMouseButtonDown(1))
        {
            HandleAction("Defend");
        }
    }

    void HandleAction(string actionType)
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX < 0 && Mathf.Abs(mouseY) < 0.15f)
        {
            playerAnimator.SetTrigger($"{actionType}Left");
        }
        else if (mouseX > 0 && Mathf.Abs(mouseY) < 0.15f)
        {
            playerAnimator.SetTrigger($"{actionType}Right");
        }
        else if (mouseY > 0 && Mathf.Abs(mouseX) < 0.15f)
        {
            playerAnimator.SetTrigger($"{actionType}Up");
        }
        else if (mouseY < 0 && Mathf.Abs(mouseX) < 0.15f)
        {
            playerAnimator.SetTrigger($"{actionType}Down");
        }
    }
}
