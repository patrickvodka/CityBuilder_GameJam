using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class TileInteraction : MonoBehaviour
{
    
    public GameObject fleshPrefab;
    public GameObject spawnerPrefab;
    private Vector3 originalPosition; // Stores the original position of the tile
    private bool isHighlighted = false; // Indicates whether the tile is currently highlighted
    private GameObject previewPrefabInstance; // Instance of the preview prefab
    private bool canCraft;
    private bool isSpawnerSet=false;
    private bool blockInput { get; set; } = false;
    private void Start()
    {
        // Save the initial position of the tile
        originalPosition = transform.position;
        canCraft = true;
    }
    

    private void OnMouseEnter()
    {
       
        

        // Instantiate the preview prefab at the tile's position
        if (fleshPrefab != null && canCraft)
        {
            previewPrefabInstance = Instantiate(fleshPrefab, transform.position, Quaternion.identity);
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
        if (!blockInput)
        {
            // If a preview prefab instance exists and is not null, instantiate the final prefab at the tile's position
            if (previewPrefabInstance != null && canCraft)
            {
                // Check if the previewPrefabInstance has the same tag as spawnerPrefab
                if (previewPrefabInstance.CompareTag(spawnerPrefab.tag))
                {
                    isSpawnerSet=true;
                    GameObject newBuilding = PrefabUtility.InstantiatePrefab(spawnerPrefab) as GameObject;
                    newBuilding.transform.position = transform.position;
                    newBuilding.transform.rotation = transform.rotation;
                }
                else
                {
                    GameObject newBuilding = PrefabUtility.InstantiatePrefab(fleshPrefab) as GameObject;
                    newBuilding.transform.position = transform.position;
                    newBuilding.transform.rotation = transform.rotation;
                }

                canCraft = false; // Prevent further crafting on this tile
            }
        }
    }
}
