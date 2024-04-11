using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class StartNavmeshBuild : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    
    void Awake()
    {
       navMeshSurface = GetComponent<NavMeshSurface>();
    }
    void Start()
    {
        // Générer le nav mesh à partir du NavMeshSurface
        navMeshSurface.BuildNavMesh();
    }
}
