using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEditor;

public class HexagonTileSelector : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private int selectedPrefabIndex = 0;

    private GameObject currentTile;

    [MenuItem("Tools/Select Hexagon Tile")]
    private static void SelectHexagonTile()
    {
        GameObject selectedObject = Selection.activeGameObject;
        HexagonTileSelector selector = selectedObject.GetComponent<HexagonTileSelector>();
        if (selector != null)
        {
            selector.SelectNextPrefab();
        }
    }

    private void SelectNextPrefab()
    {
        selectedPrefabIndex = (selectedPrefabIndex + 1) % tilePrefabs.Length;
        UpdateTilePrefab();
    }

    private void UpdateTilePrefab()
    {
        if (currentTile != null && selectedPrefabIndex < tilePrefabs.Length)
        {
            GameObject newTilePrefab = tilePrefabs[selectedPrefabIndex];
            SpriteRenderer spriteRenderer = currentTile.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = newTilePrefab.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Event.current.button == 0 && !EditorApplication.isPlaying)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 position = hit.point;
                position.y = 0; // Set to ground level

                if (selectedPrefabIndex < tilePrefabs.Length)
                {
                    GameObject selectedPrefab = tilePrefabs[selectedPrefabIndex];
                    if (currentTile != null)
                    {
                        DestroyImmediate(currentTile);
                    }
                    currentTile = Instantiate(selectedPrefab, position, Quaternion.identity);
                    currentTile.name = selectedPrefab.name;
                }
            }
        }
    }
}
