using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class  IA_zombies : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public int moveSpeed;
    public int attackDamage;
    public List<GameObject> flag;
    public float changeDirectionInterval;
    public GameObject target;
    
    
    
    
    
    
    
    
    
    
    public virtual void Update()
    {
        // Code for updating the AI behavior
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
        
        agent.SetDestination(target.transform.position);
    }

    public void HeadTowardsTheFlag()
    {
        if (target != null)
        {
            Chase();
        }
        agent.SetDestination(flag[0].transform.position);

    }
}