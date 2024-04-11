using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CustomSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform positionOfSpawn;
    [Tooltip("Spawn timer by Second")]
    public float spawnInterval = 2f; 

    private void Start() {
        
        StartCoroutine(SpawnPrefab());
    }

    IEnumerator SpawnPrefab() {
        while (true) {
            GameObject newBuilding = PrefabUtility.InstantiatePrefab(prefabToSpawn) as GameObject;
            newBuilding.transform.position = positionOfSpawn.position;
            newBuilding.transform.rotation = positionOfSpawn.rotation;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
