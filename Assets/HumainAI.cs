using UnityEngine;
using UnityEngine.AI;

public class RandomPatrol : MonoBehaviour
{
    public float patrolRadius = 10f; // Radius within which patrol points will be generated
    public float minDelay = 2f; // Minimum delay between reaching a patrol point and selecting a new one
    public float maxDelay = 5f; // Maximum delay between reaching a patrol point and selecting a new one
    public float waitTime = 3f; // Time to wait at a patrol point before selecting a new one

    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomPatrolPoint();
    }

    private void Update()
    {
        // If the agent has reached the destination and is currently moving
        if (isMoving && !agent.pathPending && agent.remainingDistance < 1f)
        {
            isMoving = false;
            Invoke("SetRandomPatrolPoint", Random.Range(minDelay, maxDelay));
        }
    }

    private void SetRandomPatrolPoint()
    {
        // Generate a random point within patrol radius
        Vector3 randomPoint = Random.insideUnitSphere * patrolRadius;
        randomPoint += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas);
        targetPosition = hit.position;

        // Set the destination for the agent
        agent.SetDestination(targetPosition);
        isMoving = true;

        // Optionally, you can wait at the patrol point for a specified time
        Invoke("ResumePatrolling", waitTime);
    }

    private void ResumePatrolling()
    {
        if (!isMoving)
            SetRandomPatrolPoint(); // If not already moving, resume patrolling immediately
    }
}