using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject tilePrefab; // The prefab to use for this tile

    // Method to instantiate the tile prefab
    public void InstantiateTilePrefab()
    {
        // Remove existing tile object
        DestroyImmediate(gameObject);

        // Instantiate new tile prefab
        if (tilePrefab != null)
        {
            GameObject newTile = Instantiate(tilePrefab, transform.position, transform.rotation);
            // Ensure the new tile maintains the same parent
            newTile.transform.SetParent(transform.parent);
        }
        else
        {
            Debug.LogWarning("Tile prefab is not assigned!");
        }
    }
}