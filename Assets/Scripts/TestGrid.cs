using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour {

	int x = 10;
	int y = 10;
	float size = 10f;
	// Use this for initialization
	void Start () {
		
		CreateGrid ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateGrid(){
		Vector2[] vertices2D = new Vector2[] {
			new Vector2(0,0),
			new Vector2(0,size),
			new Vector2(size,size),
			new Vector2(size,0)
		};
			var tr = new Triangulator (vertices2D);
				int[] indices = tr.Triangulate();
//
				// Create the Vector3 vertices
				Vector3[] vertices = new Vector3[vertices2D.Length];
				Vector2[] uvs = new Vector2[vertices2D.Length];
				for (int i=0; i<vertices.Length; i++) {
					vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
				}

				// Create the mesh
				Mesh msh = new Mesh();
				msh.vertices = vertices;
				msh.triangles = indices;
				msh.RecalculateNormals();
				msh.RecalculateBounds();
				for (var j = 0; j < vertices.Length; j++) {
			uvs [j] = new Vector2 (vertices [j].x / (size*2), vertices[j].y / (size*2));
				};
				msh.uv = uvs;
//
//				// Set up game object with mesh;
				gameObject.AddComponent(typeof(MeshRenderer));
				MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
				filter.mesh = msh;



	}
}
