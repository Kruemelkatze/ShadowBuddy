using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteToMeshTest : MonoBehaviour
{

	public Sprite sprite;
	
	// Use this for initialization
	void Awake ()
	{
		var go = CreateChildSpriteRenderer();
		var polys = go.AddComponent<PolygonCollider2D>();
		var mesh = SpriteToMesh(sprite, polys.points);
		Destroy(polys);
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

		return mesh;
	}

	private GameObject CreateChildSpriteRenderer()
	{
		var go = new GameObject();
		go.transform.parent = this.transform;
		go.transform.localPosition = new Vector3(0,0,-0.01f);
		var spriteRenderer = go.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;

		return go;
	}
}
