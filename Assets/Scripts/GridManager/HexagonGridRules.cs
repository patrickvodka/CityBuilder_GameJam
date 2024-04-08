using UnityEngine;
using UnityEngine.Tilemaps;

public class HexagonGridRules : MonoBehaviour
{
    public int width;
    public int height;
    public float scale = 1f;
    public RuleTile ruleTileTest;
    public Transform gridTransform;
    public bool lancezGeneration;
    public float OffsetZ = 0.76f;
    public float OffsetX = 0.88f;
    public Grid grid;

    void Start()
    {
        GenerateTiles();
    }

    void GenerateTiles()
    {
        lancezGeneration = false;
        // Effacer toutes les tuiles existantes avant de générer de nouvelles tuiles
        ClearTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 gridPosition = new Vector3(x * OffsetX + (y % 2 == 0 ? 0 : 0.44f), 0, y * OffsetZ);
                Vector3 worldPosition = gridTransform.TransformPoint(gridPosition);
                float perlinValue = Mathf.PerlinNoise(worldPosition.x * scale, worldPosition.y * scale);

                // Instantiate la règle de tuile
                Tilemap tilemap = grid.GetComponentInChildren<Tilemap>(); // Obtenez la Tilemap enfant du Grid
                tilemap.SetTile(tilemap.WorldToCell(worldPosition), ruleTileTest); // Définissez la règle de tuile sur la position de la grille
            }
        }
    }
    private void OnValidate()
    {
        if (lancezGeneration)
        {
            // Générer de nouvelles tuiles avec les nouvelles valeurs de hauteur et de largeur
            GenerateTiles();
        }
    }

    void ClearTiles()
    {
        Tilemap tilemap = grid.GetComponentInChildren<Tilemap>(); // Obtenez la Tilemap enfant du Grid
        tilemap.ClearAllTiles(); // Effacer toutes les tuiles de la Tilemap
    }
}