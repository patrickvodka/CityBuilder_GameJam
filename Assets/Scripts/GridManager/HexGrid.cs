using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexGrid : MonoBehaviour
{
    public int width;
    public int height;
    public float scale = 1f;
    public GameObject tilePrefab;
    public RuleTile ruleTileTest;
    public Transform gridTransform;
    public float OffsetZ = 0.76f;
    public float OffsetX = 0.88f;
    public Grid grid;

    

    void GenerateTiles()
    {
        for (int x = 1; x < width; x++)
        {
            
            for (int y = 1; y < height; y++)
            {
                Vector3 gridPosition = new Vector3(x * OffsetX + (y % 2 == 0 ? 0 : 0.44f), 0, y * OffsetZ);
                Vector3 worldPosition = gridTransform.TransformPoint(gridPosition);
                float perlinValue = Mathf.PerlinNoise(worldPosition.x * scale, worldPosition.y * scale);

                // Instantiate le prefab de tuile
                var tile = Instantiate(ruleTileTest, worldPosition, quaternion.identity, gridTransform);

                // Choix de la tuile en fonction de la valeur du bruit de Perlin
                
            }
        }
    }
/*
    void SetTileType(RuleTile tile, float perlinValue)
    {
        Renderer renderer = tile.GetComponent<Renderer>();
        if (perlinValue < 0.2f)
            renderer.material.color = Color.green; // Tile basse
        else if (perlinValue < 0.5f)
            renderer.material.color = Color.blue; // Tile moyenne
        else
            renderer.material.color = Color.red; // Tile haute
    }*/
}