using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class  IA_zombies : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator animator;
    private int attackDamage;
    public Transform flagTarget;
    public float changeDirectionInterval;
    public GameObject target;
    public ClosestFlag closestFlag;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        
    }


    public virtual void FixedUpdate()
    {
        
        
        if (target!=null)
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
    
    
    
    //freerun 
    // Look UP
    //chases
    //attack 
    //Head Towards The Flag

    public  void FreeRun()
    {
        
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
        
        agent.SetDestination(target.transform.position);
        
    }

    public void HeadTowardsTheFlag()
    {
       
        agent.SetDestination(flagTarget.transform.position);

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("humain"))
        {
            //attack 
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("humain"))
        {
            
        }
    }
}