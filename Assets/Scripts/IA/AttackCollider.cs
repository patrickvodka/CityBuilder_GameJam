using UnityEngine;
using System.Collections;

public class AttackCollider : MonoBehaviour
{
    public LayerMask targetLayers; // Layers des entités pouvant être touchées par l'attaque
    [HideInInspector]
    public float damageDeal = 10f; // Dégâts infligés par l'attaque

    private bool isAttacking = false; // Indique si l'attaque est en cours
    private Collider attackTrigger; // Collider du trigger d'attaque

    private void Start()
    {
        // Récupérer le Collider du GameObject
        attackTrigger = GetComponent<Collider>();
        // Désactiver le Collider au départ
        attackTrigger.enabled = false;
    }

    public void Attack(GameObject target)
    {
        // Vérifier si l'attaque est en cours
        if (!isAttacking)
        {
            // Lancer la coroutine d'attaque
            StartCoroutine(PerformAttack(target));
        }
    }

    private IEnumerator PerformAttack(GameObject target)
    {
        // Activer le Collider d'attaque
        attackTrigger.enabled = true;
        isAttacking = true;

        // Vérifier si le collider entre en collision avec une entité pouvant être touchée par l'attaque
        if (((1 << target.layer) & targetLayers) != 0)
        {
            // Vérifier si l'entité touchée est différente du lanceur de l'attaque pour éviter de s'infliger des dégâts
            if (target != gameObject)
            {
                // Vérifier si l'entité touchée implémente l'interface IDamageable
                IDamageable damageable = target.GetComponent<IDamageable>();
                damageable?.TakeDamage(damageDeal, DamageType.Magic); // Utiliser damageDeal pour infliger des dégâts
            }
        }

        // Attendre pendant un court instant
        yield return new WaitForSeconds(0.3f);

        // Désactiver le Collider d'attaque
        attackTrigger.enabled = false;
        isAttacking = false;
    }
}
