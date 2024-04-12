using UnityEngine;
 using System;
 
 public class Health : MonoBehaviour, IDamageable {
     public float maxHealth;
     public float currentHealth;
     public GameObject zombiePrefab;
 
     // Evénement déclenché lorsque l'entité meurt
     public event Action OnDeath;
 
     void Start() {
         currentHealth = maxHealth;
     }
     /// <summary>
     /// 
     /// </summary>
     /// <param name="damage">damage taken</param>
     /// <param name="damageType">Type of damage (fire,normal,magic)</param>
     public void TakeDamage(float damage, DamageType damageType) {
         currentHealth -= damage;
         if (currentHealth <= 0) {
             Die();
         }
     }
 
     void Die() {
         // Event death
         OnDeath?.Invoke();
         if (gameObject.CompareTag("Human")) 
         {
             TransformToZombie();
         }
         Destroy(gameObject);   
     }

     private void TransformToZombie()
     {
         // Instantiate the zombie prefab at the position of the human
         Instantiate(zombiePrefab, transform.position, transform.rotation);
     }
 }