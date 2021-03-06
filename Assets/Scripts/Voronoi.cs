﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using MIConvexHull;
using System.Linq;
public class Voronoi : MonoBehaviour {

	//https://stackoverflow.com/questions/17634480/return-c-array-to-c-sharp
	//since we allocate the voronoi vertices in C++ we need to deallocate in the plugin
	public Material material;

	[DllImport ("voronoi-gen")]
	private static extern int val();
	[DllImport ("voronoi-gen")]
	private static extern double rnd();

//	[DllImport ("voronoi-gen")]
//	private static extern int VoronoiCells(Vector3[] verts, int vertSize, ref IntPtr result, ref int size);

	[DllImport ("voronoi-gen")]
	private static extern int GenerateVoronoiCells(Vector3[] verts, int vertSize, ref IntPtr result, ref int size);

	[DllImport("voronoi-gen")]
	private static extern int ReleaseMemory (IntPtr array);

	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			var start = System.DateTime.Now;
			//run ();

			Debug.Log ("Running generate");
			var go = run ();
			var point = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0f,0f,-2f);
			go.transform.position = point;
			var end = System.DateTime.Now;
			Debug.Log(String.Format("{0}, {1}, {2}" , start, end, end-start));
		}
	}
	const float mean = 0f;
	const float stdDev = 0.25f;
	static float randomGenerator(){
		
		 //reuse this if you are generating many
		float u1 = UnityEngine.Random.Range(0f,1f); //uniform(0,1] random doubles
		float u2 =UnityEngine.Random.Range(0f,1f);

		float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
			Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
		return 
			mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
	}




	unsafe GameObject run()
	{
		
		const int dimension = 3;
		int count =0;
		IntPtr cellPtr = IntPtr.Zero;
		int cellCount;
		Vector3[] randomeVerts = new Vector3[35];
		for(int k =0; k < 35; k++){
			randomeVerts[k] = new Vector3(randomGenerator(),randomGenerator(),
				0f); 
		}
		//these random points will be managed by us, the result will be within the plugin
		//Vector3[] verts = new Vector3[]{new Vector3(1.0f,1.0f,1.0f),new Vector3(2.5f,1.0f,1.0f),new Vector3(3.0f,1.0f,1.0f)};
		GenerateVoronoiCells(randomeVerts,randomeVerts.Length, ref cellPtr, ref count);


//		for (var j = 0; j < verts.Length; j++){
//			Debug.Log(verts[j].x);
//		}

		float[] cells = new float[count];
		Marshal.Copy(cellPtr, cells, 0, count);
		//lets release now that we have copied the data;
		ReleaseMemory(cellPtr);

		List<List<Vector3>> vc = new List<List<Vector3>>();

		List<Vector3> tempList = new List<Vector3>(); 
		Debug.Log(cells.Length/3f);
		for (var j = 0; j < cells.Length; j+=3){
			if(cells[j] == -9999f){
				//Debug.Log(String.Format("{0} distinct to - {1}", tempList.Count(), tempList.Distinct().Count()) );
				//vc.Add(tempList.Distinct().ToList());
				vc.Add(tempList);
				tempList = new List<Vector3>();
			}else{
				tempList.Add(new Vector3(cells[j], cells[j+1], cells[j+2]));
			}
			//Debug.Log(String.Format("{0},{1},{2}",cells[j],cells[j+1],cells[j+2]));
		}


		var cc = 0;
		var cube = new GameObject(String.Format("parent-{0}", UnityEngine.Random.Range(0,1000000)));

		foreach (var vv in vc) {
			cc++;
			var randomVerts = vv;
			var vertices = new List<Vertex> ();
				

//			//BUG AMIT: add a random control point in for triangulation?
//			vv.Add(new Vector3(randomVerts[0].x, randomVerts[0].y, 0f));
//			Debug.Log(String.Format("{0} - {1} - {2}", cc, vv.Count(), randomVerts.Count));
//
			foreach (var v in vv) {

				var location = new double[dimension];
				location [0] = v.x;
				location [1] = v.y;
				location [2] = v.z;
				vertices.Add (new Vertex (location));
			}

//			var parent =  
//			GameObject.CreatePrimitive(PrimitiveType.Cube);
//			parent.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
//			parent.name = cc.ToString();
//			parent.GetComponent<MeshRenderer>().enabled = false;
//			parent.AddComponent<Rigidbody>().useGravity = true;
//			parent.GetComponent<Rigidbody>().AddExplosionForce(0.1f, Vector3.up,0.1f);

			var convexHull = ConvexHull.Create<Vertex>(vertices,new ConvexHullComputationConfig
				{
					PlaneDistanceTolerance = 0.00000001f,

				});
			

			var go = _CreatePiece(convexHull,Resources.Load("BlockMaterial", typeof(Material)) as Material);
//			foreach (var f in convexHull.Faces) {
//				//Normal are populated for us
//				//Debug.Log(String.Format("{0}, {1}, {2}, {3}", cc, f.Normal[0], f.Normal[1], f.Normal[2]));
//				//f.Normal = Vector3.Normalize(Vector3.Cross(f.Vertices[1] - f.Vertices[0], f.Vertices[2] - f.Vertices[0]));
//				_CreateTriangle(cc,f.ToString (), new Vector3[]{ 
//					_CreateVector3FromPosition(f.Vertices[0]),
//					_CreateVector3FromPosition(f.Vertices[1]),
//					_CreateVector3FromPosition(f.Vertices[2])
//				}, Resources.Load("BlockMaterial", typeof(Material)) as Material);
//
//
//			}
			go.transform.parent = cube.transform;
		}

		return cube;

	}

	static GameObject _CreatePiece(ConvexHull<Vertex,DefaultConvexFace<Vertex>> ch, Material mat){
		var go = new GameObject();
		go.AddComponent<MeshFilter> ();
		go.AddComponent<MeshRenderer> ();
		var rb = go.AddComponent<Rigidbody> ();
		rb.useGravity = true;
		//rb.isKinematic = true;

		Mesh mesh = new Mesh ();
		go.GetComponent<MeshFilter> ().mesh = mesh;
		//var count = 0;
		var vertices = new List<Vector3> ();
		var uv = new List<Vector2> ();
		foreach (var f in ch.Faces) {
			vertices.AddRange(f.Vertices.Select (x => _CreateVector3FromPosition (x)).ToArray());
			uv.AddRange (new Vector2[] { new Vector2 (0, 0), new Vector2 (0, 1), new Vector2 (1, 1) });
		}
		mesh.vertices = vertices.ToArray();
		mesh.uv = uv.ToArray ();
		mesh.triangles = Enumerable.Range(0, vertices.Count).ToArray();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		go.GetComponent<Renderer> ().material =mat ;
		go.transform.localScale = new Vector3 (1f, 1f, 1f);
		var mc = go.AddComponent<MeshCollider> ();
		mc.convex = true;
		mc.enabled = true;
		mc.inflateMesh = true;
		mc.skinWidth = -0.1f;
		return go;

	}

	static Vector3 _CreateVector3FromPosition(Vertex p){
		return new Vector3 ((float)p.Position [0], (float)p.Position [1], (float)p.Position [2]);
	}

	static void _CreateTriangle(int cc, String e, Vector3[] vertices, Material mat){
		var parent = GameObject.Find(cc.ToString ());

		var go = new GameObject(cc.ToString() + "-" + UnityEngine.Random.Range(0,100000).ToString());
		go.transform.parent = parent.transform;
		go.AddComponent<MeshFilter> ();
		go.AddComponent<MeshRenderer> ();

		Mesh mesh = new Mesh ();
		go.GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = vertices;

		//			foreach (Vertex sv in e.Source.Vertices) {
		//				
		//				mesh.vertices =  new Vector3[] {new Vector3(sv.Position[0], 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)};
		//			
		//			}
		//TODO set uv based on vertex position
		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
		mesh.triangles = new int[] { 0, 1, 2 };

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		go.GetComponent<Renderer> ().material =mat ;
	}
	static void CreateTetrahedron(int cc,DefaultTriangulationCell<Vertex> e, Color color){
		var p0 = new Vector3 ((float)e.Vertices [0].Position [0], (float)e.Vertices [0].Position [1], (float)e.Vertices [0].Position [2]);
		var p1 =new Vector3 ((float)e.Vertices [1].Position[0], (float)e.Vertices [1].Position [1],(float)e.Vertices [1].Position [2]);
		var p2 =new Vector3 ((float)e.Vertices [2].Position[0], (float)e.Vertices [2].Position [1],(float)e.Vertices [2].Position [2]);
		var p3 =new Vector3 ((float)e.Vertices [3].Position[0], (float)e.Vertices [3].Position [1],(float)e.Vertices [3].Position [2]);
		//Debug.Log ("here");
		var parent = GameObject.Find(cc.ToString ());

		var go = new GameObject(cc.ToString() + "-" + UnityEngine.Random.Range(0,100000).ToString());
		go.transform.parent = parent.transform;
		go.AddComponent<MeshFilter> ();
		go.AddComponent<MeshRenderer> ();
		Mesh mesh = new Mesh ();
		go.GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = new Vector3[] { 
			p0,p1,p2,
			p0,p2,p3,
			p2,p1,p3,
			p0,p3,p1
		};
		mesh.triangles = new int[]{
			0,1,2,
			3,4,5,
			6,7,8,
			9,10,11
		};
		Vector2[] uvs = new Vector2[mesh.vertices.Length];

		for (int i=0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(mesh.vertices[i].y,mesh.vertices[i].x);
		}
		mesh.uv = uvs;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();


		go.GetComponent<Renderer> ().material =  (Material)Resources.Load("ToonLit", typeof(Material));//Color.red;
			//		new Color (UnityEngine.Random.Range (0.0f, 1.0f), 
			//			UnityEngine.Random.Range (0.0f, 1.0f), 
			//			UnityEngine.Random.Range (0.0f, 1.0f),
			//			0f);
			//color;
	}


	static void CreateTriangles(int cc, DefaultTriangulationCell<Vertex> e,Color color){
		//var go = new GameObject(e.ToString());

		var go = new GameObject(cc.ToString() + "-" + UnityEngine.Random.Range(0,1000).ToString());
		go.AddComponent<MeshFilter> ();
		go.AddComponent<MeshRenderer> ();
		Mesh mesh = new Mesh ();
		go.GetComponent<MeshFilter> ().mesh = mesh;

		mesh.vertices = new Vector3[] { new Vector3 ((float)e.Vertices[0].Position[0], (float)e.Vertices[0].Position [1], 0f),
			new Vector3 ((float)e.Vertices [1].Position[0], (float)e.Vertices [1].Position [1], 0f),
			new Vector3 ((float)e.Vertices [2].Position[0], (float)e.Vertices [2].Position [1],0f)
		};

		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
		if (SurfaceNormalForTriangle (mesh.vertices [0], mesh.vertices [1], mesh.vertices [2]).z < 0f) {
			mesh.triangles = new int[] { 0, 1, 2 };
		} else {
			mesh.triangles = new int[] { 2, 1, 0 };
		}
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		go.GetComponent<Renderer> ().material.color = color;

	}

	static Vector3 SurfaceNormalForTriangle(Vector3 v1, Vector3 v2, Vector3 v3){
		Vector3 a = v2 - v1;
		Vector3 b = v3 - v1;
		return Vector3.Cross (a, b);

	}

	void Start(){
		var result = val();

		
		//Debug.Log(Marshal.PtrToStringAuto(d));

		//Debug.Log (rnd());
		var start = System.DateTime.Now;
		//run ();
		var end = System.DateTime.Now;
		Debug.Log(String.Format("{0}, {1}, {2}" , start, end, end-start));
	}

	public class ClockwiseVector2Comparer : IComparer<Vertex>
	{
		public int Compare(Vertex v1, Vertex v2)
		{
			if (v1.Position[0] >= 0f)
			{
				if (v2.Position[0] < 0f)
				{
					return -1;
				}
				return -Comparer<double>.Default.Compare(v1.Position[1], v2.Position[1]);
			}
			else
			{
				if (v2.Position[0] >= 0f)
				{
					return 1;
				}
				return Comparer<double>.Default.Compare(v1.Position[1], v2.Position[1]);
			}
		}
	}

}

