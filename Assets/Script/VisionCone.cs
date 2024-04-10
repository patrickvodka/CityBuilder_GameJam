using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class VisionCone : MonoBehaviour
{
    
    public Material VisionConeMaterial;
    public float VisionRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer;//couche avec des objets qui obstrue la vue de l'ennemi, comme des murs, par exemple
    public int VisionConeResolution = 120;//le cône de vision sera composé d'un triangles, plus cette valeur est élevée plus le cône de vision sera grand
    Mesh VisionConeMesh;
    private MeshFilter MeshFilter_;
    [SerializeField] private Transform target;
    public IA_zombies zombie;
    private const string HumanTag = "Human";

    

    

   

    void OnTriggerEnter(Collider other)
    {
        SetZombieTarget(other, other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        SetZombieTarget(other, null);
    }

    private void SetZombieTarget(Collider other, GameObject targetValue)
    {
        if (other.CompareTag(HumanTag))
        {
            zombie.target = targetValue;
        }
    }

    
    
    
    void Start()

    {
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_= transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad;
    }

    void Update()
    {
        DrawVisionCone(); 
        
        
        if (target != null)
        {
            
            zombie.target = target.gameObject; 
            
        }
       
        
    }

    void DrawVisionCone()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIcrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                Vertices[i + 1] = VertForward * VisionRange;
            }


            Currentangle += angleIcrement;
        }
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        MeshFilter_.mesh = VisionConeMesh;
    }
    
}
