using System.Collections.Generic;
using UnityEngine;

public class HexagonGridRules : MonoBehaviour
{
    // []
    public int width;
    public int height;
    public float scale = 1f;
    public GameObject tile;
    public GameObject WaterTile;
    public Transform gridTransform;
    public bool lancezGeneration = false;
    public bool clearChildrens;
    private bool isClear;
    public float OffsetZ = 0.76f;
    public float OffsetX = 0.88f;
    public Grid grid;

    public List<GameObject> tilesSpawnList = new List<GameObject>();
    [Tooltip("Scale = largeur de riviere ")]
    public float riverScale = 0.4f;
    [Tooltip("Scale = Seuil de detection de riviere  ")]
    public float riverThreshold = 0.4f; // Seuil de détection de la rivière

    void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    void GenerateTiles()
    {
        // Clear existing tiles
       

        // Define the size of the hexagon
        float hexSize = 10f; // Adjust as needed

        // Calculate the offsets for x and z to ensure all edges have the same size
        float OffsetX = hexSize * Mathf.Sqrt(3f);
        float OffsetZ = hexSize * 1.5f;

        for (int q = -width; q <= width; q++)
        {
            int r1 = Mathf.Max(-height, -q - height);
            int r2 = Mathf.Min(height, -q + height);

            for (int r = r1; r <= r2; r++)
            {
                // Calculate x and z coordinates based on hexagonal grid pattern
                float x = q * OffsetX;
                float z = r * OffsetZ;

                // Adjust even columns
                if (q % 2 == 1)
                    z += OffsetZ / 2;

                Vector3 gridPosition = new Vector3(x, 0, z);
                Vector3 worldPosition = gridTransform.TransformPoint(gridPosition);

                // Instantiate tile at the calculated position
                GameObject currentTile;
                if (Random.Range(0f, 1f) > riverThreshold)
                    currentTile = Instantiate(WaterTile, worldPosition, Quaternion.identity, gameObject.transform);
                else
                    currentTile = Instantiate(tile, worldPosition, Quaternion.identity, gameObject.transform);

                tilesSpawnList.Add(currentTile);
            }
        }
    }



    private void OnValidate()
    {
        if (lancezGeneration)
        {
            lancezGeneration = false;
            // Générer de nouvelles tuiles avec les nouvelles valeurs de hauteur et de largeur
            ClearObjects(false);
        }
        if (clearChildrens)
        {
            ClearObjects(true);
        }
    }

    private void ClearChildrens()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject child = transform.GetChild(i).gameObject;
#if UNITY_EDITOR
            // Disable children in editor mode
            child.SetActive(false);
            UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(child);
#else
        // Destroy children during play mode
        DestroyImmediate(child);
#endif
        }
    }


    /// <summary>
    /// ClearObjects gl with that 
    /// </summary>
    /// <param name="only">only objects or WholeMap</param>
    private void ClearObjects(bool only)
    {
        if (only)
        {
            clearChildrens = false;
            foreach (var tile in tilesSpawnList)
            {
                Destroy(tile);
            }
            ClearChildrens();
            tilesSpawnList.Clear();
        }
        else
        {
            lancezGeneration = false;
            foreach (var tile in tilesSpawnList)
            {
                Destroy(tile);
            }
            ClearChildrens();
            tilesSpawnList.Clear();

            GenerateTiles();
        }
    }

}