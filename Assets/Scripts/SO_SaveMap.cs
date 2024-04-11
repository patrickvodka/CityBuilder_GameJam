using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
[CreateAssetMenu(fileName = "MapData", menuName = "Map/MapData", order = 1)]
public class MapData : ScriptableObject
{
    public Dictionary<GameObject, Vector3> DIcoTest = new Dictionary<GameObject, Vector3>();
    [System.Serializable]
    public class MapObjectData
    {
        public GameObject gameObject;
        public Vector3 position;

        public MapObjectData(GameObject _gameObject, Vector3 _position)
        {
            gameObject = _gameObject;
            position = _position;
        }
    }

    public List<MapObjectData> objectDataList = new List<MapObjectData>();
}
