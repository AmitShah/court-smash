using UnityEngine;
using System.Collections;
using MIConvexHull;
using System.Linq;
using System.Collections.Generic;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using System;

public class CreateGround : MonoBehaviour {



	//TODO: 
//	[DllImport("voronoi_gen", EntryPoint="FreePtr",CallingConvention = CallingConvention.Cdecl)]
//	private static extern void FreeFaces(IntPtr faces);

	List<Vector3> verticesList; 
	List<Vector3> randomVerts;
	List<Vector3[]> lines;
	VoronoiMesh<Vertex,DefaultTriangulationCell<Vertex>,VoronoiEdge<Vertex, DefaultTriangulationCell<Vertex>>> voronoi;
	// Use this for initialization
	void Start () {
		const int NumberOfVertices = 10;
		const float size = 20;
		const int dimension = 3;




		List<List<Vector3>> vc = new List<List<Vector3>> ();
//		vc.Add(new List<Vector3>(){
//			new Vector3(-1,-1,-1) ,
//			new Vector3(1,-1,-1),
//			new Vector3(-1,1,-1),
//			new Vector3(0,1,1),
//			new Vector3(-1,-1,1),
//			new Vector3(1,-1,1),
//			new Vector3(-1,1,1),
//			new Vector3(1,0,1),
//			new Vector3(1,0,-1),
//			new Vector3(0,1,-1)

//		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(-10f,-10f,-1f),
			new Vector3(-7.94882f,-10f,-1f),
			new Vector3(-10f,-3.31584f,-1f),
			new Vector3(-5.98044f,-3.85547f,-1f),
			new Vector3(-10f,-10f,1f),
			new Vector3(-7.94882f,-10f,1f),
			new Vector3(-10f,-3.31584f,1f),
			new Vector3(-5.98044f,-3.85547f,1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(-7.94882f,-10f,-1f),
			new Vector3(-3.3582f,-10f,-1f),
			new Vector3(-4.29742f,-4.13704f,1f),
			new Vector3(-4.29742f,-4.13704f,-1f),
			new Vector3(-7.94882f,-10f,1f),
			new Vector3(-3.3582f,-10f,1f),
			new Vector3(-5.98044f,-3.85547f,1f),
			new Vector3(-5.76286f,-3.76117f,1f),
			new Vector3(-5.76286f,-3.76117f,-1f),
			new Vector3(-5.98044f,-3.85547f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(-3.3582f,-10f,-1f),
			new Vector3(0.392432f,-10f,-1f),
			new Vector3(-4.29742f,-4.13704f,1f),
			new Vector3(0.392432f,-10f,1f),
			new Vector3(-3.3582f,-10f,1f),
			new Vector3(-3.89868f,-4.13711f,1f),
			new Vector3(0.674601f,-8.62726f,-1f),
			new Vector3(0.674601f,-8.62726f,1f),
			new Vector3(-3.89868f,-4.13711f,-1f),
			new Vector3(-4.29742f,-4.13704f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(5.09223f,-10f,-1f),
			new Vector3(10f,-10f,-1f),
			new Vector3(4.54339f,-6.7361f,-1f),
			new Vector3(10f,-6.8623f,1f),
			new Vector3(5.09223f,-10f,1f),
			new Vector3(10f,-10f,1f),
			new Vector3(4.54339f,-6.7361f,1f),
			new Vector3(10f,-6.8623f,-1f),
			new Vector3(7.65906f,-5.72942f,-1f),
			new Vector3(7.65906f,-5.72942f,1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(0.392432f,-10f,1f),
			new Vector3(5.09223f,-10f,-1f),
			new Vector3(4.54339f,-6.7361f,1f),
			new Vector3(4.54339f,-6.7361f,-1f),
			new Vector3(0.392432f,-10f,-1f),
			new Vector3(5.09223f,-10f,1f),
			new Vector3(3.79361f,-6.58422f,-1f),
			new Vector3(3.79361f,-6.58422f,1f),
			new Vector3(0.674601f,-8.62726f,1f),
			new Vector3(0.674601f,-8.62726f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(0.674601f,-8.62726f,-1f),
			new Vector3(3.79361f,-6.58422f,-1f),
			new Vector3(1.13447f,-1.63668f,1f),
			new Vector3(-3.89868f,-4.13711f,1f),
			new Vector3(0.674601f,-8.62726f,1f),
			new Vector3(3.79361f,-6.58422f,1f),
			new Vector3(1.13447f,-1.63668f,-1f),
			new Vector3(-3.89868f,-4.13711f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(4.54339f,-6.7361f,1f),
			new Vector3(7.65906f,-5.72942f,-1f),
			new Vector3(1.13447f,-1.63668f,1f),
			new Vector3(7.1848f,-2.47067f,-1f),
			new Vector3(3.79361f,-6.58422f,1f),
			new Vector3(7.65906f,-5.72942f,1f),
			new Vector3(1.13447f,-1.63668f,-1f),
			new Vector3(7.1848f,-2.47067f,1f),
			new Vector3(3.79361f,-6.58422f,-1f),
			new Vector3(4.54339f,-6.7361f,-1f),
			new Vector3(1.17045f,-1.57031f,1f),
			new Vector3(1.17045f,-1.57031f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(1.17045f,-1.57031f,-1f),
			new Vector3(7.38704f,-1.96041f,1f),
			new Vector3(1.62592f,1.21872f,1f),
			new Vector3(1.17045f,-1.57031f,1f),
			new Vector3(4.19136f,2.27818f,1f),
			new Vector3(7.1848f,-2.47067f,1f),
			new Vector3(1.62592f,1.21872f,-1f),
			new Vector3(4.19136f,2.27818f,-1f),
			new Vector3(7.1848f,-2.47067f,-1f),
			new Vector3(7.38704f,-1.96041f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(7.1848f,-2.47067f,-1f),
			new Vector3(10f,-6.8623f,-1f),
			new Vector3(10f,0.382188f,-1f),
			new Vector3(10f,0.382188f,1f),
			new Vector3(7.38704f,-1.96041f,1f),
			new Vector3(10f,-6.8623f,1f),
			new Vector3(7.38704f,-1.96041f,-1f),
			new Vector3(7.65906f,-5.72942f,-1f),
			new Vector3(7.65906f,-5.72942f,1f),
			new Vector3(7.1848f,-2.47067f,1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(-10f,-3.31584f,-1f),
			new Vector3(-5.76286f,-3.76117f,1f),
			new Vector3(-10f,2.04241f,1f),
			new Vector3(-5.61674f,0.928311f,-1f),
			new Vector3(-10f,-3.31584f,1f),
			new Vector3(-5.98044f,-3.85547f,1f),
			new Vector3(-9.30719f,2.07222f,1f),
			new Vector3(-5.61674f,0.928311f,1f),
			new Vector3(-9.30719f,2.07222f,-1f),
			new Vector3(-10f,2.04241f,-1f),
			new Vector3(-5.98044f,-3.85547f,-1f),
			new Vector3(-5.76286f,-3.76117f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(-9.30719f,2.07222f,-1f),
			new Vector3(-5.61674f,0.928311f,-1f),
			new Vector3(-5.66215f,8.63591f,1f),
			new Vector3(-4.8279f,2.63814f,-1f),
			new Vector3(-9.30719f,2.07222f,1f),
			new Vector3(-5.61674f,0.928311f,1f),
			new Vector3(-4.91934f,3.5441f,1f),
			new Vector3(-4.8279f,2.63814f,1f),
			new Vector3(-4.91934f,3.5441f,-1f),
			new Vector3(-5.66215f,8.63591f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(-3.89868f,-4.13711f,1f),
			new Vector3(-5.61674f,0.928311f,-1f),
			new Vector3(-5.76286f,-3.76117f,-1f),
			new Vector3(1.49754f,1.35368f,-1f),
			new Vector3(1.17045f,-1.57031f,-1f),
			new Vector3(1.13447f,-1.63668f,-1f),
			new Vector3(-4.8279f,2.63814f,-1f),
			new Vector3(1.49754f,1.35368f,1f),
			new Vector3(-5.61674f,0.928311f,1f),
			new Vector3(-4.8279f,2.63814f,1f),
			new Vector3(1.62592f,1.21872f,-1f),
			new Vector3(1.62592f,1.21872f,1f),
			new Vector3(-4.29742f,-4.13704f,-1f),
			new Vector3(-3.89868f,-4.13711f,-1f),
			new Vector3(-4.29742f,-4.13704f,1f),
			new Vector3(-5.76286f,-3.76117f,1f),
			new Vector3(1.17045f,-1.57031f,1f),
			new Vector3(1.13447f,-1.63668f,1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(5.46549f,3.46373f,1f),
			new Vector3(10f,0.382188f,1f),
			new Vector3(5.46549f,3.46373f,-1f),
			new Vector3(10f,2.22725f,-1f),
			new Vector3(10f,0.382188f,-1f),
			new Vector3(7.38704f,-1.96041f,-1f),
			new Vector3(7.38704f,-1.96041f,1f),
			new Vector3(10f,2.22725f,1f),
			new Vector3(4.19136f,2.27818f,-1f),
			new Vector3(4.19136f,2.27818f,1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(-10f,2.04241f,-1f),
			new Vector3(-9.30719f,2.07222f,-1f),
			new Vector3(-10f,10f,-1f),
			new Vector3(-5.7447f,10f,1f),
			new Vector3(-10f,2.04241f,1f),
			new Vector3(-9.30719f,2.07222f,1f),
			new Vector3(-10f,10f,1f),
			new Vector3(-5.66215f,8.63591f,1f),
			new Vector3(-5.66215f,8.63591f,-1f),
			new Vector3(-5.7447f,10f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(0.604238f,4.11363f,1f),
			new Vector3(-4.8279f,2.63814f,1f),
			new Vector3(-4.8279f,2.63814f,-1f),
			new Vector3(0.604238f,4.11363f,-1f),
			new Vector3(1.49754f,1.35368f,1f),
			new Vector3(1.49754f,1.35368f,-1f),
			new Vector3(-4.91934f,3.5441f,1f),
			new Vector3(-4.91934f,3.5441f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(-4.91934f,3.5441f,-1f),
			new Vector3(0.604238f,4.11363f,-1f),
			new Vector3(0.0411408f,6.32277f,1f),
			new Vector3(-4.83227f,10f,1f),
			new Vector3(-5.66215f,8.63591f,-1f),
			new Vector3(0.604238f,4.11363f,1f),
			new Vector3(-5.7447f,10f,1f),
			new Vector3(-4.83227f,10f,-1f),
			new Vector3(0.0411408f,6.32277f,-1f),
			new Vector3(-5.7447f,10f,-1f),
			new Vector3(-4.91934f,3.5441f,1f),
			new Vector3(-5.66215f,8.63591f,1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(1.49754f,1.35368f,-1f),
			new Vector3(5.46549f,3.46373f,1f),
			new Vector3(0.604238f,4.11363f,-1f),
			new Vector3(5.40612f,6.31257f,1f),
			new Vector3(1.49754f,1.35368f,1f),
			new Vector3(1.62592f,1.21872f,-1f),
			new Vector3(0.0411408f,6.32277f,-1f),
			new Vector3(5.40612f,6.31257f,-1f),
			new Vector3(2.76031f,7.005f,-1f),
			new Vector3(2.76031f,7.005f,1f),
			new Vector3(0.604238f,4.11363f,1f),
			new Vector3(0.0411408f,6.32277f,1f),
			new Vector3(4.19136f,2.27818f,-1f),
			new Vector3(5.46549f,3.46373f,-1f),
			new Vector3(4.19136f,2.27818f,1f),
			new Vector3(1.62592f,1.21872f,1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(5.46549f,3.46373f,1f),
			new Vector3(10f,2.22725f,-1f),
			new Vector3(8.15934f,10f,-1f),
			new Vector3(10f,10f,-1f),
			new Vector3(5.40612f,6.31257f,1f),
			new Vector3(10f,2.22725f,1f),
			new Vector3(8.15934f,10f,1f),
			new Vector3(10f,10f,1f),
			new Vector3(5.40612f,6.31257f,-1f),
			new Vector3(5.46549f,3.46373f,-1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(0.0411408f,6.32277f,-1f),
			new Vector3(2.76031f,7.005f,-1f),
			new Vector3(-4.83227f,10f,-1f),
			new Vector3(2.61917f,10f,-1f),
			new Vector3(0.0411408f,6.32277f,1f),
			new Vector3(2.76031f,7.005f,1f),
			new Vector3(-4.83227f,10f,1f),
			new Vector3(2.61917f,10f,1f),

		});

		//ALL VC POINTS:

		vc.Add(new List<Vector3>(){
			new Vector3(2.76031f,7.005f,-1f),
			new Vector3(5.40612f,6.31257f,-1f),
			new Vector3(2.61917f,10f,-1f),
			new Vector3(8.15934f,10f,-1f),
			new Vector3(2.76031f,7.005f,1f),
			new Vector3(5.40612f,6.31257f,1f),
			new Vector3(2.61917f,10f,1f),
			new Vector3(8.15934f,10f,1f),

		});



		var cc = 0;
		foreach (var vv in vc) {
			cc++;
			randomVerts = vv;
			var vertices = new List<Vertex> ();
			var color = new Color (139.0f/255f, 244f/255f, 23f/255f,1);
			if (cc % 2 == 0) {
				color = new Color (255f/255f, 0, 240f/255f);
			}
			if (cc % 3 == 0) {
				color = new Color (0f/255f, 255f/255f, 252f/255f);
			}	//new Color (UnityEngine.Random.Range (0.0f, 1.0f), 
//							UnityEngine.Random.Range (0.0f, 1.0f), 
//							UnityEngine.Random.Range (0.0f, 1.0f));
			foreach (var v in randomVerts) {
			
				var location = new double[dimension];
				location [0] = v.x;
				location [1] = v.y;
				location [2] = v.z;
				vertices.Add (new Vertex (location));
			}

//		for (var i = 0; i < NumberOfVertices; i++)
//		{
//			var location = new double[dimension];
//			for (var j = 0; j < dimension; j++)
//				location[j] = UnityEngine.Random.Range(0,size);
//			
//			var v = new Vertex (location);
//			vertices.Add(v);
//			randomVerts.Add (new Vector3((float)location[0], (float)location[1], 0f));
//		}

			voronoi = VoronoiMesh.Create (vertices);
			var delaunay = Triangulation.CreateDelaunay<Vertex> (vertices);

			verticesList = new List<Vector3> ();
			lines = new List<Vector3[]> ();

			foreach (var e in voronoi.Vertices) {
				//CreateTriangles (e);
				CreateTetrahedron (cc, e,color);
			}
		}
//		foreach (var e in voronoi.Edges) {
//			var l = e;
//
//			lines.Add (new Vector3[]{ 
//				new Vector3((float)e.Source.Vertices[0].Position[0],
//					(float)e.Source.Vertices[0].Position[1],
//					0.0f),
//				new Vector3((float)e.Target.Vertices[0].Position[0],
//					(float)e.Target.Vertices[0].Position[1],
//					0.0f)
//			
//			});
//
//		}
//		var component = GetComponent<MeshFilter> ().mesh;
	}

	void findAdjacentVertices(){
		//voronoi.Edges.Select(x=>x.
	}
	void CreateTriangles(DefaultTriangulationCell<Vertex> e){
		var go = new GameObject(e.ToString());
		go.AddComponent<MeshFilter> ();
		go.AddComponent<MeshRenderer> ();
		Mesh mesh = new Mesh ();
		go.GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = new Vector3[] { new Vector3 ((float)e.Vertices[0].Position[0], (float)e.Vertices[0].Position [1], 0f),
			new Vector3 ((float)e.Vertices [1].Position[0], (float)e.Vertices [1].Position [1], 0f),
			new Vector3 ((float)e.Vertices [2].Position[0], (float)e.Vertices [2].Position [1],0f)
		};
		verticesList.Add(new Vector3 ((float)e.Vertices[0].Position[0], (float)e.Vertices[0].Position [1],  0f));

		//			foreach (Vertex sv in e.Source.Vertices) {
		//				
		//				mesh.vertices =  new Vector3[] {new Vector3(sv.Position[0], 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)};
		//			
		//			}
		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
		mesh.triangles = new int[] {0, 1, 2};
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		go.GetComponent<Renderer> ().material.color = Color.blue;
//		new Color (UnityEngine.Random.Range (0.0f, 1.0f), 
//			UnityEngine.Random.Range (0.0f, 1.0f), 
//			UnityEngine.Random.Range (0.0f, 1.0f));
//		
	}
	void CreateTetrahedron(int cc,DefaultTriangulationCell<Vertex> e, Color color){
		var p0 = new Vector3 ((float)e.Vertices [0].Position [0], (float)e.Vertices [0].Position [1], (float)e.Vertices [0].Position [2]);
		var p1 =new Vector3 ((float)e.Vertices [1].Position[0], (float)e.Vertices [1].Position [1],(float)e.Vertices [1].Position [2]);
		var p2 =new Vector3 ((float)e.Vertices [2].Position[0], (float)e.Vertices [2].Position [1],(float)e.Vertices [2].Position [2]);
		var p3 =new Vector3 ((float)e.Vertices [3].Position[0], (float)e.Vertices [3].Position [1],(float)e.Vertices [3].Position [2]);
		var go = new GameObject(cc.ToString() + "-" + UnityEngine.Random.Range(0,1000).ToString());
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
		;
		go.GetComponent<Renderer> ().material.color = //Color.red;
//		new Color (UnityEngine.Random.Range (0.0f, 1.0f), 
//			UnityEngine.Random.Range (0.0f, 1.0f), 
//			UnityEngine.Random.Range (0.0f, 1.0f),
//			0f);
			color;
	}
	void OnDrawGizmosSelected() {
		
		Gizmos.color = Color.red;
		if (voronoi != null) {
			foreach (var e in voronoi.Vertices) {
				var p0 = new Vector3 ((float)e.Vertices [0].Position [0], (float)e.Vertices [0].Position [1], (float)e.Vertices [0].Position [2]);
				var p1 =new Vector3 ((float)e.Vertices [1].Position[0], (float)e.Vertices [1].Position [1],(float)e.Vertices [1].Position [2]);
				var p2 =new Vector3 ((float)e.Vertices [2].Position[0], (float)e.Vertices [2].Position [1],(float)e.Vertices [2].Position [2]);
				var p3 =new Vector3 ((float)e.Vertices [3].Position[0], (float)e.Vertices [3].Position [1],(float)e.Vertices [3].Position [2]);

				var cc = CreateGround.Circumcentre (p0,p1,p2,p3);
				Gizmos.DrawSphere (cc, 1.0f);
			}
		}
		Gizmos.color = Color.green;
		if (randomVerts != null) {
			foreach (var v in randomVerts) {
				//Gizmos.DrawCube (v, 0.5f);
			}	
		}
		Gizmos.color = Color.blue;
		if (lines != null) {
			foreach (var e in voronoi.Edges) {
				
				//Gizmos.DrawLine (l[0], l[1]);
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}

	public static Vector3 Circumcentre2(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		// Calculate plane for edge AB
		Vector3 planeABNormal = b - a;
		Vector3 planeABPoint = Vector3.Lerp(a, b, 0.5f);

		// Calculate plane for edge AC
		Vector3 planeACNormal = c - a;
		Vector3 planeACPoint = Vector3.Lerp(a, c, 0.5f);

		// Calculate plane for edge BD
		Vector3 planeBDNormal = d - b;
		Vector3 planeBDPoint = Vector3.Lerp(b, d, 0.5f);

		// Calculate plane for edge CD
		Vector3 planeCDNormal = d - c;
		Vector3 planeCDPoint = Vector3.Lerp(c, d, 0.5f);

		// Calculate line that is the plane-plane intersection between AB and AC
		Vector3 linePoint1;
		Vector3 lineDirection1;

		// Taken from: http://wiki.unity3d.com/index.php/3d_Math_functions
		Math3d.PlanePlaneIntersection(out linePoint1, out lineDirection1, planeABNormal, planeABPoint, planeACNormal, planeACPoint);

		Vector3 linePoint2;
		Vector3 lineDirection2;

		Math3d.PlanePlaneIntersection(out linePoint2, out lineDirection2, planeBDNormal, planeBDPoint, planeCDNormal, planeCDPoint);

		// Calculate the point that is the plane-line intersection between the above line and CD
		Vector3 intersection;

		// Floating point inaccuracy often causes these two lines to not intersect, in that case get the two closest points on each line 
		// and average them
		// Taken from: http://wiki.unity3d.com/index.php/3d_Math_functions
		if (!Math3d.LineLineIntersection(out intersection, linePoint1, lineDirection1, linePoint2, lineDirection2))
		{
			Vector3 closestLine1;
			Vector3 closestLine2;

			// Taken from: http://wiki.unity3d.com/index.php/3d_Math_functions
			Math3d.ClosestPointsOnTwoLines(out closestLine1, out closestLine2, linePoint1, lineDirection1, linePoint2, lineDirection2);

			// Intersection is halfway between the closest two points on lines
			intersection = Vector3.Lerp(closestLine2, closestLine2, 0.5f);
		}

		return intersection;
	}

	//Shamelessy taken from : http://math.stackexchange.com/questions/1875452/calculate-circumsphere-of-tetrahedron
	public static Vector3 Circumcentre(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		// Calculate plane for edge AB
		Vector3 planeABNormal = b - a;
		Vector3 planeABPoint = Vector3.Lerp(a, b, 0.5f);

		// Calculate plane for edge AC
		Vector3 planeACNormal = c - a;
		Vector3 planeACPoint = Vector3.Lerp(a, c, 0.5f);

		// Calculate plane for edge BD
		Vector3 planeBDNormal = d - b;
		Vector3 planeBDPoint = Vector3.Lerp(b, d, 0.5f);

		// Calculate plane for edge CD
		Vector3 planeCDNormal = d - c;
		Vector3 planeCDPoint = Vector3.Lerp(c, d, 0.5f);

		// Calculate line that is the plane-plane intersection between AB and AC
		Vector3 linePoint1;
		Vector3 lineDirection1;

		// Taken from: http://wiki.unity3d.com/index.php/3d_Math_functions
		Math3d.PlanePlaneIntersection(out linePoint1, out lineDirection1, planeABNormal, planeABPoint, planeACNormal, planeACPoint);

		Vector3 linePoint2;
		Vector3 lineDirection2;

		Math3d.PlanePlaneIntersection(out linePoint2, out lineDirection2, planeBDNormal, planeBDPoint, planeCDNormal, planeCDPoint);

		// Calculate the point that is the plane-line intersection between the above line and CD
		Vector3 intersection;

		// Floating point inaccuracy often causes these two lines to not intersect, in that case get the two closest points on each line 
		// and average them
		// Taken from: http://wiki.unity3d.com/index.php/3d_Math_functions
		if (!Math3d.LineLineIntersection(out intersection, linePoint1, lineDirection1, linePoint2, lineDirection2))
		{
			Vector3 closestLine1;
			Vector3 closestLine2;

			// Taken from: http://wiki.unity3d.com/index.php/3d_Math_functions
			Math3d.ClosestPointsOnTwoLines(out closestLine1, out closestLine2, linePoint1, lineDirection1, linePoint2, lineDirection2);

			// Intersection is halfway between the closest two points on lines
			intersection = Vector3.Lerp(closestLine2, closestLine2, 0.5f);
		}
		return intersection;
	}

}
