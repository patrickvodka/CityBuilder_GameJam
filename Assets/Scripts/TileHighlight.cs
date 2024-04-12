using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    public GameObject prefabToInstantiate; // The prefab to instantiate when clicking on the tile
    private Vector3 originalPosition; // Stores the original position of the tile
    private bool isHighlighted = false; // Indicates whether the tile is currently highlighted
    private GameObject previewPrefabInstance;// Instance of the preview prefab
    private bool canCraft;

    private void Start()
    {
        // Save the initial position of the tile
        originalPosition = transform.position;
        canCraft = true;
    }

    private void Update()
    {
        prefabToInstantiate = GetComponentInParent<HexagonGridRules>().objectToSpawn;
        // If the preview prefab instance exists, update its position to follow the mouse cursor
        if (previewPrefabInstance != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the object is at the same z-coordinate
            previewPrefabInstance.transform.position = transform.position;
        }
    }

    private void OnMouseEnter()
    {
        isHighlighted = true; // Set the highlighted flag to true

        // Instantiate the preview prefab at the tile's position
        if (prefabToInstantiate != null && canCraft)
        {
            previewPrefabInstance = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        }
    }

    private void OnMouseExit()
    {
        // Restore the tile to its initial position when the mouse exits
        if (isHighlighted)
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
