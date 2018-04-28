using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteToMeshTest : MonoBehaviour
{

	private static bool InvertMeshNormals = false;

	public Sprite sprite;
	
	// Use this for initialization
	void Awake ()
	{
		var go = CreateChildSpriteRenderer();
		var polys = go.AddComponent<PolygonCollider2D>();
		
		var mesh = SpriteToMesh(sprite, polys.points);
		DestroyImmediate(polys);
		
		GetComponent<MeshFilter>().mesh = mesh;

		var collider = GetComponent<MeshCollider>();
		mesh.RecalculateBounds();
		collider.sharedMesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private Mesh SpriteToMesh(Sprite sprite, Vector2[] points = null)
	{
		int[] triangles;
		if (points == null)
		{
			points = sprite.vertices;
			triangles = Array.ConvertAll(sprite.triangles, i => (int) i);
		}
		else
		{
			var tria = new Triangulator(points);
			triangles = tria.Triangulate();
		}
		
		Mesh mesh = new Mesh();
		mesh.SetVertices(Array.ConvertAll(points, i => (Vector3)i).ToList());
		mesh.SetTriangles(triangles,0);

		if (InvertMeshNormals)
		{
			Vector3[] normals = mesh.normals;
			for (int i = 0; i < normals.Length; i++)
				normals[i] = -normals[i];
			mesh.normals = normals;

			for (int m = 0; m < mesh.subMeshCount; m++)
			{
				int[] t = mesh.GetTriangles(m);
				for (int i = 0; i < t.Length; i += 3)
				{
					int temp = t[i + 0];
					t[i + 0] = t[i + 1];
					t[i + 1] = temp;
				}

				mesh.SetTriangles(t, m);
			}
		}

		return mesh;
	}

	private GameObject CreateChildSpriteRenderer()
	{
		SpriteRenderer spriteRenderer;
		GameObject go;
		if (transform.childCount == 0)
		{
			go = new GameObject();
			spriteRenderer = go.AddComponent<SpriteRenderer>();
		}
		else
		{
			go = transform.GetChild(0).gameObject;
			spriteRenderer = go.GetComponent<SpriteRenderer>();
		}
		go.transform.parent = transform;
		go.transform.localPosition = new Vector3(0,0,-0.01f);
		
		spriteRenderer.sprite = sprite;

		return go;
	}
}
