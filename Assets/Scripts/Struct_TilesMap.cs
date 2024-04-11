using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public partial class HexagonGridRules : MonoBehaviour
{
    public GameObject[] prefabs; // Stockez vos préfabriqués ici

    // Méthode pour sauvegarder les GameObjects et leurs positions dans le ScriptableObject
    public void SaveMap(GameObject parentObject)
    {
        SO_SaveMap.gameObjectDataList.Clear();
        SaveChildren(parentObject.transform);
    }

    // Méthode pour parcourir les enfants et sauvegarder les GameObjects et leurs positions
    private void SaveChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            string tag = child.tag;
            if (!string.IsNullOrEmpty(tag))
            {
                SO_SaveMap.AddGameObject(tag, child.position);
            }
            else
            {
                Debug.LogWarning("Tag not found for GameObject: " + child.gameObject.name);
            }
        }
    }

    // Méthode pour générer les GameObjects à partir de la sauvegarde dans le ScriptableObject
    private void GenerateFromSaveMap()
    {
        foreach (var gameObjectData in SO_SaveMap.gameObjectDataList)
        {
            string tag = gameObjectData.tag;
            Vector3 position = gameObjectData.position;

            // Trouver le préfabriqué correspondant au tag
            GameObject prefab = GetPrefabByTag(tag);
            if (prefab != null)
            {
                Instantiate(prefab, position, Quaternion.Euler(90, 0, 0),gameObject.transform);
                // Vous pouvez ajouter d'autres traitements ou composants au besoin
            }
            else
            {
                Debug.LogWarning("Prefab not found for tag: " + tag);
            }
        }
    }

    // Méthode pour obtenir le préfabriqué correspondant à un tag
    private GameObject GetPrefabByTag(string tag)
    {
        foreach (var prefab in prefabs)
        {
            if (prefab.CompareTag(tag))
            {
                return prefab;
            }
        }
        return null;
    }
}
