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
        // Effacer toutes les tuiles existantes avant de générer de nouvelles tuiles

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 gridPosition = new Vector3(x * OffsetX + (y % 2 == 0 ? 0 : 35f), 0, y * OffsetZ);
                Vector3 worldPosition = gridTransform.TransformPoint(gridPosition);
                // Utiliser le bruit de Perlin pour déterminer la présence des rivières
                float perlinValue = Mathf.PerlinNoise((worldPosition.x + 0.1f) * scale * riverScale, (worldPosition.z + 0.1f) * scale * riverScale);

                // Vérifier si le bruit de Perlin dépasse le seuil de la rivière
                if (perlinValue > riverThreshold)
                {
                    // Placer une tuile d'eau
                    var waterTile = Instantiate(tile, worldPosition, Quaternion.identity, gameObject.transform);
                    tilesSpawnList.Add(waterTile);
                }
                else
                {
                    // Placer une tuile normale
                    var currentTile = Instantiate(tile, worldPosition, Quaternion.identity, gameObject.transform);
                    tilesSpawnList.Add(currentTile);
                }
            }
        }

        foreach (var item in tilesSpawnList)
        {
            Debug.Log(item.GetComponent<BrushTest>().isSpawned);
            item.GetComponent<BrushTest>().isSpawned = true;
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