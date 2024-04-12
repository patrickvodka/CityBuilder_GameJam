using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public List<IDamageable> Damageables { get; } = new();
    private IA_zombies Zombie;

    private void Awake()
    {
        Zombie = GetComponent<IA_zombies>();
    }

    public void OnTriggerEnter(Collider other)
   {
      var damageable = other.GetComponent<IDamageable>();
      if (damageable != null)
      {
          Damageables.Add(damageable);
      }
   }
   public void OnTriggerExit(Collider other)
   {
       var damageable = other.GetComponent<IDamageable>();
       if (damageable != null && Damageables.Contains(damageable))
       {
           Damageables.Remove(damageable);
       }
   }
}
