using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] 
public class AISensor : MonoBehaviour
{
    public float distence = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color meshColor= Color.red;
    public int scanFrequency = 30;
    public LayerMask layers;
    public List<GameObject> Objects = new List<GameObject>();

    private Collider[] Colliders = new Collider[50];
    private Mesh mesh;
    private int count;
    private float scanInterval;
    private float scanTimer;
    
    void Start()
    {
        scanInterval = 1.0f / scanFrequency;
    }

    public bool IsInSight(GameObject obj)
    {
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer<0)
        {
            scanTimer += scanInterval;
            scan();
        }
    }

    private void scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distence, Colliders, layers,
            QueryTriggerInteraction.Collide);
        Objects.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Colliders[i].gameObject;
            if (IsInSight(obj))
            {
                Objects.Add(obj);
            }
        }
        
        
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangle = (segments *4)+2+2;
        int numVerticers = numTriangle * 3;
        
        Vector3[] vertices = new Vector3[numVerticers];
        int[] triangles = new int[numVerticers];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distence;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distence;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;
        // left side 
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;
        
        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;
        
        //right side 
        vertices[vert++] = bottomCenter ;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;
        
        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;
        
        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distence; 
            bottomRight = Quaternion.Euler(0, currentAngle+deltaAngle, 0) * Vector3.forward * distence;
            
            topCenter = bottomCenter + Vector3.up * height; 
            topRight = bottomRight + Vector3.up * height;
            
            
        
           
        
            //far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;
        
            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;
        
        
            //top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;
        
        
        
            //bottom 
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
            
        }
        
       
        for (int i = 0; i < numVerticers; i++)
        {
            triangles[i] = i;
        }
        
        
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh,transform.position,transform.rotation);
        }
        Gizmos.DrawWireSphere(transform.position,distence);
        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawSphere(Colliders[i].transform.position,0.2f);
        }
        Gizmos.color = Color.green;

        foreach (var obj in Objects)
        {
            Gizmos.DrawSphere(obj.transform.position,0.2f);
        }
    }
}
