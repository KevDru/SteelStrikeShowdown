using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Camera playerCamera;  // Reference to the player's camera
    public Animator playerAnimator;
    public GameObject hitEffectPrefab; // Prefab for hit effect
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

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

    void resetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void attackRayCast()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        
        Debug.DrawRay(ray.origin, ray.direction * attackDistance, Color.red, 0.1f);  

        
        if (Physics.Raycast(ray, out hit, attackDistance, attackLayer))
        {
            Debug.Log($"Hit: {hit.collider.gameObject.name}");  

            
            Actor actor = hit.collider.GetComponent<Actor>();
            if (actor != null)
            {
                
                actor.TakeDamage(attackDamage);

                
            }

            HitTarget(hit.point);
        }
    }



    void HitTarget(Vector3 pos)
    {
        GameObject GO = Instantiate(hitEffectPrefab, pos, Quaternion.identity);
        Destroy(GO, 20f);
    }

    void HandleAction(string actionType)
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(resetAttack), attackSpeed);
        Invoke(nameof(attackRayCast), attackDelay);

        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (mouseInput.x < 0 && Mathf.Abs(mouseInput.y) < 0.15f)
        {
            playerAnimator.SetTrigger($"{actionType}Left");
        }
        else if (mouseInput.x > 0 && Mathf.Abs(mouseInput.y) < 0.15f)
        {
            playerAnimator.SetTrigger($"{actionType}Right");
        }
        else if (mouseInput.y > 0 && Mathf.Abs(mouseInput.x) < 0.15f)
        {
            playerAnimator.SetTrigger($"{actionType}Up");
        }
        else if (mouseInput.y < 0 && Mathf.Abs(mouseInput.x) < 0.15f)
        {
            playerAnimator.SetTrigger($"{actionType}Down");
        }
    }
}

