using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[ExecuteInEditMode]
public class BrushTest : MonoBehaviour
{
    public GameObject[] allTiles;
    public int wantedTileInArray;
    public bool isSpawned = false;
    // destroy UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(child);

    private void OnValidate()
    {
        if (isSpawned)
        {
            ChangeTile();
        }
    }

    private void ChangeTile()
    {
       if(wantedTileInArray > allTiles.Length && wantedTileInArray < 0  )
        {
            return;
<<<<<<< HEAD
       }
       else
       {
            Debug.Log("teftesfs");
=======
        }
        else
        {
            
>>>>>>> parent of 3ea76bf (Merge branch 'main' of https://github.com/patrickvodka/CityBuilder_GameJam)
            var grid = GetComponentInParent<HexagonGridRules>(); 
            GameObject goToSpawn = allTiles[wantedTileInArray];
           var goSpawned = Instantiate(goToSpawn, transform.position, Quaternion.Euler(90, 0, 0), gameObject.GetComponentInParent<HexagonGridRules>().transform);
            goSpawned.GetComponent<BrushTest>().isSpawned = true;
            UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(gameObject);
<<<<<<< HEAD
       }
=======
            
        }
>>>>>>> parent of 3ea76bf (Merge branch 'main' of https://github.com/patrickvodka/CityBuilder_GameJam)
    }
}
