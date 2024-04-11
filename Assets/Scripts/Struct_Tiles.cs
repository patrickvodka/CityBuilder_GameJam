using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu(fileName = "ManualSaver", menuName = "Map/SaveMap", order = 1)]
public class GameObjectDictionarySO : ScriptableObject
{
    [System.Serializable]
    public struct GameObjectData
    {
        public string tag; // Tag de l'objet
        public Vector3 position;

        public GameObjectData(string _tag, Vector3 pos)
        {
            tag = _tag;
            position = pos;
        }
    }

    public List<GameObjectData> gameObjectDataList = new List<GameObjectData>();

    // Méthode pour ajouter une entrée au dictionnaire
    public void AddGameObject(string tag, Vector3 pos)
    {
        gameObjectDataList.Add(new GameObjectData(tag, pos));
    }

    // Méthode pour supprimer une entrée du dictionnaire
    public void RemoveGameObject(string tag)
    {
        gameObjectDataList.RemoveAll(item => item.tag == tag);
    }
}