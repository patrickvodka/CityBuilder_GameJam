using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]

public class ChooseHexagone : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private int selectedPrefabIndex = 0;

    private static void SelectTilePrefab()
    {
        GameObject selectedObject = Selection.activeGameObject;
        ChooseHexagone selector = selectedObject.GetComponent<ChooseHexagone>();
        if (selector != null)
        {
            selector.SelectNextPrefab();
        }
    }

    private void SelectNextPrefab()
    {
        selectedPrefabIndex = (selectedPrefabIndex + 1) % tilePrefabs.Length;
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

                GameObject selectedPrefab = tilePrefabs[selectedPrefabIndex];
                GameObject tile = Instantiate(selectedPrefab, position, Quaternion.identity);
                tile.name = selectedPrefab.name;
            }
        }
    }
}

