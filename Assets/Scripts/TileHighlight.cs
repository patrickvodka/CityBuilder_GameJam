using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    public GameObject prefabToInstantiate; // The prefab to instantiate when clicking on the tile
    private Vector3 originalPosition; // Stores the original position of the tile
    private bool isHighlighted = false; // Indicates whether the tile is currently highlighted
    private GameObject previewPrefabInstance; // Instance of the preview prefab
    private bool canCraft;
    private void Start()
    {
        // Save the initial position of the tile
        originalPosition = transform.position;
        canCraft = true;
    }
    

    private void OnMouseEnter()
    {
        // Move the tile upwards when the mouse enters
        

        // Instantiate the preview prefab at the tile's position
        if (prefabToInstantiate != null && canCraft)
        {
            previewPrefabInstance = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        }
    }

    private void OnMouseExit()
    {
          transform.position = originalPosition;
            isHighlighted = false; // Reset the highlighted flag

            // Destroy the preview prefab instance
            if (previewPrefabInstance != null)
            {
                Destroy(previewPrefabInstance);
                previewPrefabInstance = null;
            }
        
    }

    private void OnMouseDown()
    {
        // If a preview prefab instance exists and is not null, instantiate the final prefab at the tile's position
        if (previewPrefabInstance != null && canCraft)
        {
            Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
            canCraft = false;
        }
    }
}
