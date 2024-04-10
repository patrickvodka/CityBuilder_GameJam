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
    public List<Transform> flag;
    public GameObject flagTarget;
    public string tagToFind = "Flag"; // Tag à rechercher
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
        RechercheFalg();
        
        if (target!=null)
        {
            //chases
        }
        else
        {
            if (flagTarget !=null)
            {
                //falg
            }
            else
            {
                closestFlag.FindClosestObject();
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
        //animator.SetBool("");
       
    }

    public  void LookUp()
    {
        agent.SetDestination(target.transform.position);
    }
    public void Chase()
    {
        
        agent.SetDestination(target.transform.position);
    }

    public void HeadTowardsTheFlag()
    {
       
        agent.SetDestination(flagTarget.transform.position);

    }

    public void RechercheFalg()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagToFind);

        // Ajouter les Transform de ces objets à la liste
        foreach (GameObject obj in objects)
        {
            flag.Add(obj.transform);
        }

        // Afficher le nombre d'objets trouvés avec le tag spécifié
        Debug.Log("Nombre d'objets avec le tag '" + tagToFind + "' trouvés : " + flag.Count);
    }
}