using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestFlag : MonoBehaviour
{
    public List<Transform> flagToCompare; 
    private Transform closestObject; 
    public string tagToFind = "Flag"; // Tag à rechercher
    public IA_zombies zombie;

    void Start()
    {
        
    }

   public void FindClosestObject()
   {
       
        if (flagToCompare != null && flagToCompare.Count > 0)
        {
           
            float minDistance = Mathf.Infinity;

            // Parcourir la liste des objets à comparer
            foreach (Transform obj in flagToCompare)
            {
                // Calculer la distance 
                float distance = Vector3.Distance(transform.position, obj.position);

                // Vérifier si cette distance est plus petite que la distance minimale actuelle
                if (distance < minDistance)
                {
                    // Mettre à jour la distance minimale et le flag le plus proche
                    minDistance = distance;
                    closestObject = obj;
                }
            }
            
            Debug.Log("L'objet le plus proche est : " + closestObject.name);
            zombie.flagTarget = closestObject.transform;
        }
        else
        {
             Debug.LogWarning("La liste des objets à comparer est vide.");
        }
    }
   public void RechercheFlag()
   {
       GameObject[] objects = GameObject.FindGameObjectsWithTag(tagToFind);

       // Ajouter les Transform de ces objets à la liste
       foreach (GameObject obj in objects)
       {
           flagToCompare.Add(obj.transform);
       }

       // Afficher le nombre d'objets trouvés avec le tag spécifié
       Debug.Log("Nombre d'objets avec le tag '" + tagToFind + "' trouvés : " + flagToCompare.Count);
       FindClosestObject();
   }
}
