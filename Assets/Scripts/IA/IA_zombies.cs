using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;
using Random = UnityEngine.Random;

public class IA_zombies : MonoBehaviour, IDamageable
{
    public NavMeshAgent agent;
    private Animator animator;
    private int attackDamage;
    public Transform flagTarget;
    public List<Transform> targetList;
    private Transform target;
    private float radiusFreeRun= 5;
    public List<IDamageable> Damageable;
    public float maxHealth;
    public float currentHealth;


    [SerializeField] private float DamageAfterTime;
    [SerializeField] private float strongDamageAfterTime;
    [SerializeField] private DamageType Normal;
    [SerializeField] private int Damage;
    private Vector3 FreeRunTarget;
    private ClosestFlag closestFlag;
    private AttackArea attackArea;
    
    
    


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        FreeRunTarget = transform.position;
        FreeRunTarget.y = 0;
        closestFlag = GetComponent<ClosestFlag>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        
    }
    // Evénement déclenché lorsque l'entité meurt
    public event Action OnDeath;

    public virtual void Update()
    {
        



        if ((targetList.Any()))
        {
           animator.SetBool("voir",true);
           animator.SetBool("drapeau poser",false);
           /*if (Damageable.Any())
           {
               Debug.Log("test");
           }*/
           
        }
        else
        { 
            closestFlag.RechercheFlag();
            animator.SetBool("voir",false);
            if (flagTarget !=null)
            {
               
                animator.SetBool("drapeau poser",true);
            }
            else
            {
               return;
            }
        }
       
        
        
    }
    

    public  void FreeRun()
    {
        Debug.Log("free");

        Vector3 zombiesPosistion=transform.position;
        zombiesPosistion.y = 0;
        
        float distance =Vector3.Distance(zombiesPosistion, FreeRunTarget );
        if (distance<= 1)
        {
            
            FreeRunTarget = RandomNavmeshLocation(radiusFreeRun);
            agent.SetDestination(FreeRunTarget);

        }
        Debug.Log(distance);
    }
    public void Attack()
    {

        StartCoroutine(hit(false));

    }
    
    public void Chase()
    {
        if ((!targetList.Any()))
        {
            animator.SetBool("voir",false);
            
        }
        else
        {
            agent.SetDestination(targetList[0].transform.position);
        }
        
        
        
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

    private IEnumerator hit(bool strong)
    {
        yield return new WaitForSeconds(strong? DamageAfterTime: strongDamageAfterTime);
        foreach (var damageable in Damageable)
        {
            damageable.TakeDamage(Damage, Normal);
        }
        
    }


    public void TakeDamage(float damage, DamageType damageType)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}