using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Transform goal; // The target the agent should move towards

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 3f; // Stop 1 unit away from the target
    }

    void Update()
    {
        if (goal != null)
        {
            // Continuously update the agent's destination to the target's position
            agent.destination = goal.position;

            // Optional: If close enough, face the target
            if (Vector3.Distance(transform.position, goal.position) <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        // Rotate the agent to face the target
        Vector3 direction = (goal.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
