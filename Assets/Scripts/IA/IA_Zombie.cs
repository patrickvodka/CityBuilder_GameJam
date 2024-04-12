using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float detectionRadius = 10f; // Rayon de détection autour de l'IA
    public float attackRange = 3f; // Distance à laquelle l'IA peut attaquer un humain
    public float attackDamage = 10f; // Dégâts infligés par l'IA aux villageois
    public AttackCollider attackColl;

    private NavMeshAgent agent; // Référence au composant NavMeshAgent
    private GameObject target; // Cible de l'IA
    private Vector3 randomDestination; // Destination aléatoire pour se déplacer
    private bool isMoving = false; // Indique si l'IA est en mouvement

    void Awake()
    {
        attackColl.damageDeal = attackDamage;
        agent = GetComponent<NavMeshAgent>(); // Récupérer le composant NavMeshAgent attaché à ce GameObject
    }

    void Update()
    {
        if (target != null)
        {
            // Si la cible est définie, déplacer l'IA vers la cible
            agent.SetDestination(target.transform.position);

            // Si la cible est à portée d'attaque, attaquer
            if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
            {
                Attack(target.transform);
            }
        }
        else
        {
            if (!isMoving)
            {
                // Trouver un nouveau point aléatoire et se déplacer vers lui
                randomDestination = GetRandomPoint(transform.position, 10f);
                agent.SetDestination(randomDestination);
                isMoving = true;
            }

            // Vérifier si l'IA a atteint sa destination aléatoire
            if (isMoving && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                isMoving = false;
            }

            // Vérifier si un humain est détecté dans la zone de détection
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Human"))
                {
                    target = collider.gameObject;
                    break;
                }
            }
        }
    }

    void Attack(Transform target)
    {
        // Gérer l'attaque de l'humain cible
        attackColl.Attack(target.gameObject);
    }

    Vector3 GetRandomPoint(Vector3 center, float range)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas);
        return hit.position;
    }
}
