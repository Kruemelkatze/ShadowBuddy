using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
 
public class ShadowPuppet : ScriptableObject
{
 
    public GameObject obj;
    public Vector3 origin;
    public Vector3[] vertices;
    public Vector3[] sortedVectors;
    public List<Vector2> pointsList=new List<Vector2>();
    public Vector2[] sortedPoints;
    public List<Vector3> shadowVerts = new List<Vector3>();
    public int index;
   
    public void Create (GameObject go, Vector3 v)
    {
        obj = go;
        origin = v;
        for(int i=0; i<3; i++)
        {
            Vector2 point = new Vector2(0,0);
            pointsList.Add(point);
        }
    }
 
    public void CreateShadowCollision()
    {
        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        for(int j=0; j<vertices.Length; j++)
            vertices[j] = obj.transform.TransformPoint(vertices[j]);
        sortedVectors = vertices.OrderBy(v => v.y).ToArray<Vector3>();
        Array.Reverse(sortedVectors);
        shadowVerts = sortedVectors.OfType<Vector3>().ToList();
        for(int i=0; i<sortedVectors.Length; i++)
        {
            Ray ray = new Ray(origin, (sortedVectors[i]-origin));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider == obj.GetComponent<Collider>())
                {
                    shadowVerts.Remove(sortedVectors[i]);
                }
            }
        }
        for(int jj=0; jj<7; jj+=3)
        {
            Ray ray = new Ray(origin, (shadowVerts[jj]-origin));
            RaycastHit hit;
            LayerMask layermask = 1<<LayerMask.NameToLayer("BG");
            if(Physics.Raycast(ray, out hit, 200, layermask))
            {
                index=jj/3;
                pointsList[index] = new Vector2(hit.point.x, hit.point.y);
                Debug.DrawRay(origin, (shadowVerts[jj]-origin).normalized*50);
            }
        }
        sortedPoints = pointsList.OrderBy(v => v.x).ToArray<Vector2>();
        //Vector2[] sortedPoints = pointsList.OrderBy(v => v.y).ToArray<Vector2>();
        Array.Reverse(sortedPoints);
    }
}