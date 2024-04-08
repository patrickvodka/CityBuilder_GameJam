using UnityEngine;
public class SphereGrid : MonoBehaviour
{
    public GameObject prefab; // Le prefab que vous souhaitez utiliser pour chaque élément de la grille
    public int gridSize = 10; // Taille de la grille
    public float radius = 5f; // Rayon de la sphère

    void Start()
    {
        CreateSphereGrid();
    }

    void CreateSphereGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                float theta = Mathf.PI * i / gridSize;
                float phi = 2 * Mathf.PI * j / gridSize;

                float x = radius * Mathf.Sin(theta) * Mathf.Cos(phi);
                float y = radius * Mathf.Sin(theta) * Mathf.Sin(phi);
                float z = radius * Mathf.Cos(theta);

                Vector3 position = new Vector3(x, y, z);

                GameObject cube = Instantiate(prefab, position, Quaternion.identity);
                cube.transform.SetParent(transform);
            }
        }
    }
}
