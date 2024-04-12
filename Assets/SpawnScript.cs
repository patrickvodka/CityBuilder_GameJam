using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class ButtonClickHandler : MonoBehaviour
{
    public List<GameObject> prefabToSpawn = new List<GameObject>();
    public HexagonGridRules HexagonalRuleTile;
    public GameObject selectedPrefab;

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void OnButtonClick()
    {
        
            selectedPrefab = prefabToSpawn[1];
        
        HexagonalRuleTile.objectToSpawn = selectedPrefab;
    }
}