using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
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




	unsafe static   void run()
	{
		IntPtr items;
		int count =0;

		IntPtr cellPtr = IntPtr.Zero;
		int cellCount;
		Vector3[] randomeVerts = new Vector3[20];
		for(int k =0; k < 20; k++){
			randomeVerts[k] = new Vector3(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f)); 
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

		for (var j = 0; j < cells.Length; j+=3){
			Debug.Log(String.Format("{0},{1},{2}",cells[j],cells[j+1],cells[j+2]));
		}
//		int[] c = new int[cellCount+1];
//		Marshal.Copy(cells, c, 0, cellCount);
////			for(int i=0; i <count; i++){
////				Debug.Log(items[i]);
////			}
//		for (int i=0; i < cellCount; i++){
//			Debug.Log(c[cellCount]);
//		}
//
//
//		Debug.Log ("run");
	}
//
//		
//	}



	void Awake(){
		var result = val();
	


		//Debug.Log(Marshal.PtrToStringAuto(d));

		Debug.Log (rnd());
		run ();
	}
}
