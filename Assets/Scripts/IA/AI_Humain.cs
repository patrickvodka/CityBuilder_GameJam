using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using UnityEngine;
public class AI_Humain : MonoBehaviour,IDamageable
{
    public float maxHealth;
    public float currentHealth;
    public NavMeshAgent agent;
    private Animator animator;
    private float radiusFreeRun = 5;
    private Vector3 FreeRunTarget;
    public Transform flagTarget;
    private closestHumain _ClosestHumain;


    private void Awake()
    {
        _ClosestHumain = GetComponent<closestHumain>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        FreeRunTarget = transform.position;
        FreeRunTarget.y = 0;
       
    }


    private void Update()
    {
        _ClosestHumain.RechercheFlag();
        if (flagTarget !=null)
        {
            animator.SetBool("drapeau poser",true);
        }
    }


    public void FreeRun()
    { 

        Vector3 zombiesPosistion=transform.position;
        zombiesPosistion.y = 0;
        
        float distance =Vector3.Distance(zombiesPosistion, FreeRunTarget );
        if (distance<= 1)
        {
            
            FreeRunTarget = RandomNavmeshLocation(radiusFreeRun);
            agent.SetDestination(FreeRunTarget);

        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
    public void HeadTowardsTheHumain()
    {
       
        agent.SetDestination(flagTarget.transform.position);

    }

    public void Damage(int damageAmount)
    {
        Debug.Log("aiaiai");
    }
}
