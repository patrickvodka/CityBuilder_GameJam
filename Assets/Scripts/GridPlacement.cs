using UnityEngine;

public class GridPlacement : MonoBehaviour
{
    void Update()
    {
        // Vérifie si le bouton gauche de la souris est cliqué
        if (Input.GetMouseButtonDown(0))
        {
            // Envoie un rayon depuis la position de la souris dans l'espace de la caméra
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Vérifie s'il y a une collision avec un objet
            if (Physics.Raycast(ray, out hit))
            {
                // Récupère la position de l'objet touché dans l'espace mondial
                Vector3 hitPosition = hit.point;
                
                // Affiche la position dans la console
                Debug.Log("Position cliquée : " + hitPosition);
            }
        }
    }
}