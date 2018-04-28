using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

[System.Serializable]
public class Puppet 
{
	public GameObject obj;
	public Vector3 origin;
	public float rayLength;
	public Vector3[] vertices;
	public List<Vector2> pointsList=new List<Vector2>();
	public List<Vector3> shadowVerts = new List<Vector3>();
	
	public void Create (GameObject go, Vector3 v)
	{
		obj = go;
		origin = v;
	}
	
	public void GetShadowBounds()
	{
		pointsList.Clear();
		Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;
		for(int j=0; j<vertices.Length; j++)
			vertices[j] = obj.transform.TransformPoint(vertices[j]);
		shadowVerts = vertices.OfType<Vector3>().ToList();
		for(int i=0; i<vertices.Length; i++)
		{
			Ray ray = new Ray(origin, (vertices[i]-origin));
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit, rayLength))
			{
				if(hit.collider == obj.GetComponent<Collider>())
				{
					shadowVerts.Remove(vertices[i]);
				}
			}
		}
		for(int jj=0; jj<shadowVerts.Count; jj++)
		{
			Ray ray = new Ray(origin, (shadowVerts[jj]-origin));
			RaycastHit hit;
			LayerMask layermask = 1<<LayerMask.NameToLayer("BG");
			
			if(Physics.Raycast(ray, out hit, rayLength, layermask))
			{
				Debug.DrawRay(ray.origin, ray.direction*rayLength,Color.red);
				Vector2 point = new Vector2(hit.point.x, hit.point.z);
				if(!pointsList.Contains(point))
					pointsList.Add(point);
			}
		}
	}
}