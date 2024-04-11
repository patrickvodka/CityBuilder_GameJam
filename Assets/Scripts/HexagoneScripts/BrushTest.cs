#if UNITY_EDITOR


using UnityEngine;
[ExecuteInEditMode]
public class BrushTest : MonoBehaviour
{
    public GameObject[] allTiles;
    public int wantedTileInArray;
    public bool isSpawned = false;
    [HideInInspector]
    public bool playTest=false;
    // destroy UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(child);

    private void OnValidate()
    {
        if(playTest)
            return;
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
        }
        else
        {

            var grid = GetComponentInParent<HexagonGridRules>(); 
            GameObject goToSpawn = allTiles[wantedTileInArray];
            var goSpawned = Instantiate(goToSpawn, transform.position, Quaternion.Euler(0, 0, 0), gameObject.GetComponentInParent<HexagonGridRules>().transform);
            goSpawned.GetComponent<BrushTest>().isSpawned = true;
            UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(gameObject);

        }
    }
}
#endif