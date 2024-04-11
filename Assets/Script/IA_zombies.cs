using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class  IA_zombies : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator animator;
    private int attackDamage;
    public Transform flagTarget;
    public List<Transform> targetList;
    private Transform target;
    private Vector3 randomDirection;
    private float radiusFreeRun= 5;
    public float detectionRange = 5f;

    private void Start()
    {
        FreeRun();
        agent = GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
    }


    public virtual void Update()
    {

        
        if ((target!=null))
        {
            Debug.Log("hahahah");
            Chase();
            
        }
        else
        {
            if (flagTarget !=null)
            {
               
                HeadTowardsTheFlag();
            }
            else
            {
                
            }
            //freerun 
        }
       
        
        
    }
    

    public  void FreeRun()
    {
        agent.SetDestination(RandomNavmeshLocation(radiusFreeRun));
        
    }
    public void Attack()
    {
       
        
       
    }

    public  void LookUp()
    {
        
    }
    public void Chase()
    {
        Debug.Log("chase");
        
    }
    

    public void HeadTowardsTheFlag()
    {
       
        agent.SetDestination(flagTarget.transform.position);

    }
    
    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
    private void DetectPlayerFootsteps()
    {
        // Utiliser la détection des pas du joueur pour déclencher des actions de l'IA agricole
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider collider in hitColliders)
        {
            // Réagir à la présence du joueur (exemple : activer l'IA agricole)
            if (collider.CompareTag("Player"))
            {
                // Faire réagir l'IA agricole en fonction des pas détectés
                // Exemple : lancer une animation, déclencher une action agricole, etc.
            }
        }
    }

    
    
    
}