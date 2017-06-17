using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using MIConvexHull;
using System.Linq;
public class Voronoi : MonoBehaviour {

	//https://stackoverflow.com/questions/17634480/return-c-array-to-c-sharp
	//since we allocate the voronoi vertices in C++ we need to deallocate in the plugin

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
		
	}
	const float mean = 0f;
	const float stdDev = 0.15f;
	static float randomGenerator(){
		
		 //reuse this if you are generating many
		float u1 = UnityEngine.Random.Range(0f,1f); //uniform(0,1] random doubles
		float u2 =UnityEngine.Random.Range(0f,1f);

		float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
			Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
		return 
			mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
	}




	unsafe static   void run()
	{
		const int NumberOfVertices = 10;
		const float size = 20;
		const int dimension = 3;

		IntPtr items;
		int count =0;

		IntPtr cellPtr = IntPtr.Zero;
		int cellCount;
		Vector3[] randomeVerts = new Vector3[15];
		for(int k =0; k < 15; k++){
			randomeVerts[k] = new Vector3(.8f+randomGenerator(),.8f+randomGenerator(),0.2f); 
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
		VoronoiMesh<Vertex,DefaultTriangulationCell<Vertex>,VoronoiEdge<Vertex, DefaultTriangulationCell<Vertex>>> voronoi;

		foreach (var vv in vc) {
			cc++;
			var randomVerts = vv;
			var vertices = new List<Vertex> ();
			var color = new Color (139.0f/255f, 244f/255f, 23f/255f,1);
			if (cc % 2 == 0) {
				color = new Color (255f/255f, 0, 240f/255f);
			}
			if (cc % 3 == 0) {
				color = new Color (0f/255f, 255f/255f, 252f/255f);
			}	

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
//
//

//			voronoi = VoronoiMesh.Create (vertices);
//			//Todo:consider this
//			//https://github.com/gusmanb/MIConvexHull/blob/master/Examples/7DelaunayWPF/Tetrahedron.cs
//			//var delaunay = Triangulation.CreateDelaunay<Vertex> (vertices);
//			var parent =  new GameObject(cc.ToString());
//
//			foreach (var e in  voronoi.Vertices) {
//				
//				//CreateTriangles (cc,e,color);
//				//Debug.Log(String.Format("{0} - [{1} ]",cc, );
//
//				CreateTetrahedron (cc, e,color);
//			}
			var parent =  new GameObject(cc.ToString());
			var convexHull = ConvexHull.Create<Vertex>(vertices);
			foreach (var f in convexHull.Faces) {

				_CreateTriangle(cc,f.ToString (), new Vector3[]{ 
					_CreateVector3FromPosition(f.Vertices[0]),
					_CreateVector3FromPosition(f.Vertices[1]),
					_CreateVector3FromPosition(f.Vertices[2])
				}, color);


			}
		}

	}


	static Vector3 _CreateVector3FromPosition(Vertex p){
		return new Vector3 ((float)p.Position [0], (float)p.Position [1], (float)p.Position [2]);
	}

	static void _CreateTriangle(int cc, String e, Vector3[] vertices, Color color){
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
		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
		mesh.triangles = new int[] { 0, 1, 2 };

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		go.GetComponent<Renderer> ().material.color = color;
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

		Debug.Log (rnd());
		run ();
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

