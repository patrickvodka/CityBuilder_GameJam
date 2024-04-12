#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public partial class  HexagonGridRules : MonoBehaviour
{
    // []
    public int width;
    public int height;
    public float scale = 1f;
    public GameObject tile;
    public GameObject WaterTile;
    public Transform gridTransform;
    public bool lancezGeneration = false;
    [Tooltip("mettre la variable a true si on lance le jeu en PlayMode")]
    public bool playTest = true;
    public bool clearChildrens;
    public GameObject objectToSpawn;
    [Space]
    [Space]
    
    public bool Save;
    [Space]
    public bool generateFromSave;
    public  GameObjectDictionarySO SO_SaveMap;
    public float OffsetZ = 0.30f;
    public float OffsetX = 0.35f;
    public Grid grid;
    

    public List<GameObject> tilesSpawnList = new List<GameObject>();
    [Tooltip("Scale = largeur de riviere ")]
    public float riverScale = 0.4f;
    [Tooltip("Scale = Seuil de detection de riviere  ")]
    public float riverThreshold = 0.4f; // Seuil de détection de la rivière

    void GenerateTiles()
    {
        if (playTest)
        {
            return;
        }

        if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // Effacer toutes les tuiles existantes avant de générer de nouvelles tuiles
                if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                    return;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Décalage de la moitié de l'OffsetX pour chaque ligne impaire
                        float offsetX = x * OffsetX + (y % 2 == 0 ? 0 : OffsetX * 0.5f);

                        Vector3 gridPosition = new Vector3(offsetX, 0, y * OffsetZ);
                        Vector3 worldPosition = gridTransform.TransformPoint(gridPosition);

                        // Utiliser le bruit de Perlin pour déterminer la présence des rivières
                        float perlinValue = Mathf.PerlinNoise((worldPosition.x + 0.1f) * scale * riverScale,
                            (worldPosition.z + 0.1f) * scale * riverScale);

                        // Vérifier si le bruit de Perlin dépasse le seuil de la rivière
                        GameObject tilePrefab = (perlinValue > riverThreshold) ? WaterTile : tile;
                        GameObject currentTile = Instantiate(tilePrefab, worldPosition, Quaternion.identity,
                            gameObject.transform);
                        tilesSpawnList.Add(currentTile);
                    }
                }

                if (!playTest)
                {
                    foreach (var item in tilesSpawnList)
                    {
                        item.GetComponent<BrushTest>().isSpawned = true;
                    }
                }
            }
        
    }

#endif
#if UNITY_EDITOR
    private void OnValidate()
    {
        ChangePlaymode(playTest);
        if (playTest)
        {
            return;
        }
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            return;
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
        if (Save)
        {
            Save = false;
            SaveMap(gameObject);
        }
        if (generateFromSave)
        {
            generateFromSave = false;
            GenerateFromSaveMap();
        }
        
    }
#endif
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

#if UNITY_EDITOR

    private void ChangePlaymode(bool currentBool)
    {
        // Parcourir tous les enfants du GameObject actuel
        foreach (Transform childTransform in transform)
        {
            // Récupérer le composant BrushTest de l'enfant s'il en a un
            BrushTest brushTest = childTransform.GetComponent<BrushTest>();
        
            // Si le composant BrushTest existe sur l'enfant
            if (brushTest != null)
            {
                // Définir la variable playTest sur la valeur spécifiée
                brushTest.playTest = currentBool;
            }
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
            if (playTest ||  EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }
            GenerateTiles();
        }
    }

}
#endif