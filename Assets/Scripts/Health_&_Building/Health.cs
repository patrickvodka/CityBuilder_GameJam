using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable {
    public float maxHealth;
    public float currentHealth;

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
        Destroy(gameObject);
    }
}