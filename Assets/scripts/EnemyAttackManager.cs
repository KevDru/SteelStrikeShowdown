using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public Animator enemyAnimator;
    public Transform Player; // Reference to the player
    public float attackRange = 10f; // Distance at which the enemy starts attacking
    public float attackInterval = 2f; // Time between attacks

    private float attackTimer;

    void Update()
    {
        if (Player == null) return; // Avoid errors if player is missing

        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if (distanceToPlayer <= attackRange)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                PerformRandomAttack();
                attackTimer = attackInterval; // Reset attack cooldown
            }
        }
    }

    void PerformRandomAttack()
    {
        int attackDirection = Random.Range(0, 4); // 0 = Left, 1 = Right, 2 = Up, 3 = Down

        switch (attackDirection)
        {
            case 0:
                enemyAnimator.SetTrigger("AttackLeft");
                break;
            case 1:
                enemyAnimator.SetTrigger("AttackRight");
                break;
            case 2:
                enemyAnimator.SetTrigger("AttackUp");
                break;
            case 3:
                enemyAnimator.SetTrigger("AttackDown");
                break;
        }
    }
}
