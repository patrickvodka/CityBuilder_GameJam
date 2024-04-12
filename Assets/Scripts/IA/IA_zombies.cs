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
    public float maxHealth;
    public float currentHealth;


    [SerializeField] private float DamageAfterTime;
    [SerializeField] private DamageType Normal;
    private Vector3 FreeRunTarget;
    private ClosestFlag closestFlag;
    private AttackArea attackArea;
    


    private void Awake()
    {
        attackArea = GetComponent<AttackArea>();
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
    }
    public void Attack()
    {

        StartCoroutine(hit());

    }
    
    public void Chase()
    {
        if ((!targetList.Any()))
        {
            animator.SetBool("voir",false);
            
        }
        else
        {
            Vector3 _zombiesPosistion=transform.position;
            _zombiesPosistion.y = 0;
        
            float distance =Vector3.Distance(_zombiesPosistion, targetList[0].transform.position );
            if (distance<=1.6)
            {
            
                Debug.Log("attack");
                Attack();

            }
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

    private IEnumerator hit()
    {
        yield return new WaitForSeconds( DamageAfterTime);
        foreach (var damageable in attackArea.Damageables)
        {
            damageable.Damage(attackDamage);
        }
        
    }

    

    public void Damage(int damageAmount)
    {
        currentHealth = currentHealth -damageAmount;
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}