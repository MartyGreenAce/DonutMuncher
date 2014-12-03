using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleProgressBar : MonoBehaviour
{
	public float radiusSize = 0.2f;
    private float progressAmount = 0;
    
    public tk2dUIMask progressMask;
	private MeshFilter meshFilter;
		
	void Start () 
	{
		meshFilter = GetComponent<MeshFilter>();
	}
    
    public void UpdateProgress(float newProgress)
    {
        progressAmount = newProgress;
        UpdateVerts();
    }
    
    public void UpdateVerts()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
                
        int radiusAmount = 20;
        float twoPI = (Mathf.PI * 2);
                
        vertices.Add(new Vector3(0, 0, -0.2f));
                
        for (int i = 0; i < radiusAmount; ++i)
        {
            float vertsZeroOne = (float)i / radiusAmount;
            
            vertices.Add(new Vector3(Mathf.Sin(twoPI * vertsZeroOne * ((1-progressAmount) * 1.25f)) * radiusSize, 
                                     Mathf.Cos(twoPI * vertsZeroOne * ((1-progressAmount) * 1.25f)) * radiusSize, -0.2f));
        }
            
        //Loop every radius (triangle)
        for (int j = 0; j < radiusAmount; ++j)
        {
            //Center vert
            triangles.Add(0);
                        
            //Two verts
            for (int i = 0; i < 2; ++i)
            {
                int offsetVert = i + 1 + j;
                                
                if (offsetVert < radiusAmount)
                    triangles.Add(i + 1 + j);
            }
        }
                
        meshFilter.mesh.vertices = vertices.ToArray();
        meshFilter.mesh.SetIndices(triangles.ToArray(), MeshTopology.Triangles, 0);
        meshFilter.mesh.triangles = triangles.ToArray();
        
        progressMask.Build();
    }
	
	void Update () 
	{
	}
}
