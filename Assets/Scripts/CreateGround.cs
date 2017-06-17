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



//		//BAD BLOCK
//		//SHOULD BE BAD
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.16764f,-0.0700706f,-0.5f),
//			new Vector3(-0.0464072f,-0.0647357f,-0.5f),
//			new Vector3(-0.144536f,0.237344f,-0.5f),
//			new Vector3(-0.149231f,0.240476f,-0.5f),
//
//			new Vector3(-0.16764f,-0.0700706f,0.5f),
//			new Vector3(-0.0464072f,-0.0647357f,0.5f),
//			new Vector3(-0.144536f,0.237344f,0.5f),
//			new Vector3(-0.149231f,0.240476f,0.5f),
//
//		});
//
//		//Fails on completely parallel triangles
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.348162f,-0.0982212f,-0.5f),
//			new Vector3(-0.0326806f,-0.398012f,0.5f),
//			new Vector3(-0.348162f,-0.0982212f,0.5f),
//			//new Vector3(-0.0326806f,-0.398012f,-0.2f),//Introduced a way point
//			new Vector3(-0.0326806f,-0.398012f,-0.5f),
//			new Vector3(-0.232884f,-0.0832066f,-0.5f),
//			new Vector3(-0.232884f,-0.0832066f,0.5f),
//
//		});

//		//TEST #2
//		vc.Add(new List<Vector3>(){
//			new Vector3(-1f,-1f,-0.5f),
//			new Vector3(0.536051f,-1f,-0.5f),
//			new Vector3(-0.191468f,-0.375987f,-0.5f),
//			new Vector3(0.0555073f,-0.372317f,-0.5f),
//			new Vector3(-1f,-1f,0.5f),
//			new Vector3(0.536051f,-1f,0.5f),
//			new Vector3(0.0173284f,-0.361066f,0.5f),
//			new Vector3(0.0555073f,-0.372317f,0.5f),
//			new Vector3(0.0173284f,-0.361066f,-0.5f),
//			new Vector3(-1f,-0.795192f,-0.5f),
//			new Vector3(-1f,-0.795192f,0.5f),
//			new Vector3(-0.191468f,-0.375987f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.102548f,-0.119624f,-0.5f),
//			new Vector3(-0.0611588f,-0.0380439f,-0.5f),
//			new Vector3(0.00741258f,-0.118741f,0.5f),
//			new Vector3(0.00741258f,-0.118741f,-0.5f),
//			new Vector3(-0.102548f,-0.119624f,0.5f),
//			new Vector3(-0.006146f,-0.0269761f,-0.5f),
//			new Vector3(-0.0611588f,-0.0380439f,0.5f),
//			new Vector3(-0.006146f,-0.0269761f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.191468f,-0.375987f,0.5f),
//			new Vector3(-0.191468f,-0.375987f,-0.5f),
//			new Vector3(-0.0793624f,-0.200597f,-0.5f),
//			new Vector3(-0.0340186f,-0.214759f,0.5f),
//			new Vector3(0.0173284f,-0.361066f,0.5f),
//			new Vector3(-0.0340186f,-0.214759f,-0.5f),
//			new Vector3(-0.0793624f,-0.200597f,0.5f),
//			new Vector3(0.0173284f,-0.361066f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-1f,-0.795192f,-0.5f),
//			new Vector3(-0.0793624f,-0.200597f,-0.5f),
//			new Vector3(-0.107104f,-0.12395f,-0.5f),
//			new Vector3(-0.0793624f,-0.200597f,0.5f),
//			new Vector3(-1f,-0.795192f,0.5f),
//			new Vector3(-0.191468f,-0.375987f,0.5f),
//			new Vector3(-1f,-0.781501f,0.5f),
//			new Vector3(-1f,-0.781501f,-0.5f),
//			new Vector3(-0.191468f,-0.375987f,-0.5f),
//			new Vector3(-0.107104f,-0.12395f,0.5f),
//			new Vector3(-0.257323f,-0.0957589f,0.5f),
//			new Vector3(-0.257323f,-0.0957589f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.00741258f,-0.118741f,0.5f),
//			new Vector3(-0.0793624f,-0.200597f,-0.5f),
//			new Vector3(-0.107104f,-0.12395f,0.5f),
//			new Vector3(0.123349f,-0.160472f,0.5f),
//			new Vector3(-0.0340186f,-0.214759f,0.5f),
//			new Vector3(-0.0340186f,-0.214759f,-0.5f),
//			new Vector3(-0.0793624f,-0.200597f,0.5f),
//			new Vector3(-0.102548f,-0.119624f,0.5f),
//			new Vector3(-0.102548f,-0.119624f,-0.5f),
//			new Vector3(-0.107104f,-0.12395f,-0.5f),
//			new Vector3(0.12004f,-0.17124f,0.5f),
//			new Vector3(0.12004f,-0.17124f,-0.5f),
//			new Vector3(0.123349f,-0.160472f,-0.5f),
//			new Vector3(0.00741258f,-0.118741f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.257323f,-0.0957589f,0.5f),
//			new Vector3(-0.102548f,-0.119624f,0.5f),
//			new Vector3(-0.171954f,0.0668147f,0.5f),
//			new Vector3(-0.117652f,0.0686361f,-0.5f),
//			new Vector3(-0.257323f,-0.0957589f,-0.5f),
//			new Vector3(-0.107104f,-0.12395f,0.5f),
//			new Vector3(-0.124183f,0.0732046f,0.5f),
//			new Vector3(-0.0611588f,-0.0380439f,-0.5f),
//			new Vector3(-0.107104f,-0.12395f,-0.5f),
//			new Vector3(-0.102548f,-0.119624f,-0.5f),
//			new Vector3(-0.117652f,0.0686361f,0.5f),
//			new Vector3(-0.0611588f,-0.0380439f,0.5f),
//			new Vector3(-0.124183f,0.0732046f,-0.5f),
//			new Vector3(-0.171954f,0.0668147f,-0.5f),
//			new Vector3(-0.254036f,-0.0717289f,0.5f),
//			new Vector3(-0.254036f,-0.0717289f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.536051f,-1f,0.5f),
//			new Vector3(0.855617f,-1f,-0.5f),
//			new Vector3(0.121255f,-0.180823f,-0.5f),
//			new Vector3(0.121255f,-0.180823f,0.5f),
//			new Vector3(0.0555073f,-0.372317f,0.5f),
//			new Vector3(0.855617f,-1f,0.5f),
//			new Vector3(0.0555073f,-0.372317f,-0.5f),
//			new Vector3(0.536051f,-1f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.267402f,0.130015f,-0.5f),
//			new Vector3(1f,-0.194706f,0.5f),
//			new Vector3(0.165052f,-0.118485f,0.5f),
//			new Vector3(0.846305f,0.532869f,-0.5f),
//			new Vector3(1f,-0.194706f,-0.5f),
//			new Vector3(0.165052f,-0.118485f,-0.5f),
//			new Vector3(0.267402f,0.130015f,0.5f),
//			new Vector3(1f,0.59539f,-0.5f),
//			new Vector3(0.846305f,0.532869f,0.5f),
//			new Vector3(1f,0.59539f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.855617f,-1f,-0.5f),
//			new Vector3(1f,-1f,-0.5f),
//			new Vector3(0.121255f,-0.180823f,0.5f),
//			new Vector3(1f,-0.194706f,-0.5f),
//			new Vector3(0.855617f,-1f,0.5f),
//			new Vector3(1f,-1f,0.5f),
//			new Vector3(0.147274f,-0.12381f,-0.5f),
//			new Vector3(1f,-0.194706f,0.5f),
//			new Vector3(0.12004f,-0.17124f,0.5f),
//			new Vector3(0.121255f,-0.180823f,-0.5f),
//			new Vector3(0.123349f,-0.160472f,0.5f),
//			new Vector3(0.165052f,-0.118485f,-0.5f),
//			new Vector3(0.123349f,-0.160472f,-0.5f),
//			new Vector3(0.12004f,-0.17124f,-0.5f),
//			new Vector3(0.147274f,-0.12381f,0.5f),
//			new Vector3(0.165052f,-0.118485f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.12004f,-0.17124f,-0.5f),
//			new Vector3(-0.0340186f,-0.214759f,-0.5f),
//			new Vector3(0.12004f,-0.17124f,0.5f),
//			new Vector3(0.121255f,-0.180823f,0.5f),
//			new Vector3(0.0555073f,-0.372317f,-0.5f),
//			new Vector3(0.0555073f,-0.372317f,0.5f),
//			new Vector3(-0.0340186f,-0.214759f,0.5f),
//			new Vector3(0.121255f,-0.180823f,-0.5f),
//			new Vector3(0.0173284f,-0.361066f,0.5f),
//			new Vector3(0.0173284f,-0.361066f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.00741258f,-0.118741f,-0.5f),
//			new Vector3(0.123349f,-0.160472f,0.5f),
//			new Vector3(0.0657027f,0.00496402f,-0.5f),
//			new Vector3(0.0657027f,0.00496402f,0.5f),
//			new Vector3(0.00741258f,-0.118741f,0.5f),
//			new Vector3(0.123349f,-0.160472f,-0.5f),
//			new Vector3(-0.006146f,-0.0269761f,-0.5f),
//			new Vector3(-0.006146f,-0.0269761f,0.5f),
//			new Vector3(0.147274f,-0.12381f,-0.5f),
//			new Vector3(0.147274f,-0.12381f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-1f,-0.781501f,0.5f),
//			new Vector3(-0.660971f,0.601722f,-0.5f),
//			new Vector3(-1f,0.909721f,-0.5f),
//			new Vector3(-0.752045f,0.71827f,0.5f),
//			new Vector3(-0.257323f,-0.0957589f,-0.5f),
//			new Vector3(-0.254036f,-0.0717289f,-0.5f),
//			new Vector3(-1f,0.909721f,0.5f),
//			new Vector3(-0.752045f,0.71827f,-0.5f),
//			new Vector3(-1f,-0.781501f,-0.5f),
//			new Vector3(-0.660971f,0.601722f,0.5f),
//			new Vector3(-0.254036f,-0.0717289f,0.5f),
//			new Vector3(-0.257323f,-0.0957589f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.124183f,0.0732046f,0.5f),
//			new Vector3(0.0399654f,0.0694558f,0.5f),
//			new Vector3(-0.0225365f,0.3006f,-0.5f),
//			new Vector3(0.0399654f,0.0694558f,-0.5f),
//			new Vector3(-0.124183f,0.0732046f,-0.5f),
//			new Vector3(-0.0225365f,0.3006f,0.5f),
//			new Vector3(-0.117652f,0.0686361f,-0.5f),
//			new Vector3(-0.117652f,0.0686361f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.0680041f,0.0421237f,-0.5f),
//			new Vector3(-0.006146f,-0.0269761f,0.5f),
//			new Vector3(-0.117652f,0.0686361f,-0.5f),
//			new Vector3(0.0399654f,0.0694558f,0.5f),
//			new Vector3(0.0399654f,0.0694558f,-0.5f),
//			new Vector3(0.0680041f,0.0421237f,0.5f),
//			new Vector3(-0.0611588f,-0.0380439f,-0.5f),
//			new Vector3(0.0657027f,0.00496402f,0.5f),
//			new Vector3(-0.0611588f,-0.0380439f,0.5f),
//			new Vector3(-0.117652f,0.0686361f,0.5f),
//			new Vector3(-0.006146f,-0.0269761f,-0.5f),
//			new Vector3(0.0657027f,0.00496402f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.660971f,0.601722f,-0.5f),
//			new Vector3(-0.660971f,0.601722f,0.5f),
//			new Vector3(-0.254036f,-0.0717289f,-0.5f),
//			new Vector3(-0.171954f,0.0668147f,-0.5f),
//			new Vector3(-0.171954f,0.0668147f,0.5f),
//			new Vector3(-0.254036f,-0.0717289f,0.5f),
//			new Vector3(-0.254036f,-0.0717289f,0f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.171954f,0.0668147f,0.5f),
//			new Vector3(-0.124183f,0.0732046f,-0.5f),
//			new Vector3(-0.752045f,0.71827f,-0.5f),
//			new Vector3(-0.0219509f,0.323106f,0.5f),
//			new Vector3(-0.660971f,0.601722f,0.5f),
//			new Vector3(-0.124183f,0.0732046f,0.5f),
//			new Vector3(-0.752045f,0.71827f,0.5f),
//			new Vector3(-0.0219509f,0.323106f,-0.5f),
//			new Vector3(-0.660971f,0.601722f,-0.5f),
//			new Vector3(-0.171954f,0.0668147f,-0.5f),
//			new Vector3(-0.0225365f,0.3006f,-0.5f),
//			new Vector3(-0.0225365f,0.3006f,0.5f),
//			new Vector3(-0.0216151f,0.309922f,-0.5f),
//			new Vector3(-0.0216151f,0.309922f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.0219509f,0.323106f,0.5f),
//			new Vector3(0.151151f,0.0961385f,-0.5f),
//			new Vector3(-0.0219509f,0.323106f,-0.5f),
//			new Vector3(0.846305f,0.532869f,-0.5f),
//			new Vector3(0.267402f,0.130015f,-0.5f),
//			new Vector3(0.846305f,0.532869f,0.5f),
//			new Vector3(0.267402f,0.130015f,0.5f),
//			new Vector3(0.151151f,0.0961385f,0.5f),
//			new Vector3(-0.0216151f,0.309922f,-0.5f),
//			new Vector3(-0.0216151f,0.309922f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.151151f,0.0961385f,-0.5f),
//			new Vector3(0.0680041f,0.0421237f,0.5f),
//			new Vector3(-0.0225365f,0.3006f,-0.5f),
//			new Vector3(0.0399654f,0.0694558f,0.5f),
//			new Vector3(0.151151f,0.0961385f,0.5f),
//			new Vector3(-0.0216151f,0.309922f,-0.5f),
//			new Vector3(0.0399654f,0.0694558f,-0.5f),
//			new Vector3(0.0680041f,0.0421237f,-0.5f),
//			new Vector3(-0.0225365f,0.3006f,0.5f),
//			new Vector3(-0.0216151f,0.309922f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.0680041f,0.0421237f,-0.5f),
//			new Vector3(0.0657027f,0.00496402f,-0.5f),
//			new Vector3(0.151151f,0.0961385f,-0.5f),
//			new Vector3(0.267402f,0.130015f,-0.5f),
//			new Vector3(0.147274f,-0.12381f,-0.5f),
//			new Vector3(0.147274f,-0.12381f,0.5f),
//			new Vector3(0.151151f,0.0961385f,0.5f),
//			new Vector3(0.267402f,0.130015f,0.5f),
//			new Vector3(0.165052f,-0.118485f,-0.5f),
//			new Vector3(0.165052f,-0.118485f,0.5f),
//			new Vector3(0.0657027f,0.00496402f,0.5f),
//			new Vector3(0.0680041f,0.0421237f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-1f,0.909721f,0.5f),
//			new Vector3(1f,0.59539f,0.5f),
//			new Vector3(-1f,1f,-0.5f),
//			new Vector3(1f,1f,-0.5f),
//			new Vector3(-1f,0.909721f,-0.5f),
//			new Vector3(0.846305f,0.532869f,0.5f),
//			new Vector3(-1f,1f,0.5f),
//			new Vector3(1f,1f,0.5f),
//			new Vector3(-0.0219509f,0.323106f,0.5f),
//			new Vector3(-0.0219509f,0.323106f,-0.5f),
//			new Vector3(-0.752045f,0.71827f,0.5f),
//			new Vector3(-0.752045f,0.71827f,-0.5f),
//			new Vector3(0.846305f,0.532869f,-0.5f),
//			new Vector3(1f,0.59539f,-0.5f),
//
//		});



//		//TEST#3
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-1f,-0.960555f,0.5f),
//			new Vector3(-1f,-0.960555f,-0.5f),
//			new Vector3(-1f,0.55324f,-0.5f),
//			new Vector3(-0.426258f,0.124114f,0.5f),
//			new Vector3(-0.304973f,-0.213889f,0.5f),
//			new Vector3(-0.479205f,0.176951f,0.5f),
//			new Vector3(-1f,0.55324f,0.5f),
//			new Vector3(-0.304973f,-0.213889f,-0.5f),
//			new Vector3(-0.426258f,0.124114f,-0.5f),
//			new Vector3(-0.479205f,0.176951f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.186329f,-0.243634f,0.5f),
//			new Vector3(-0.186329f,-0.243634f,-0.5f),
//			new Vector3(0.0540037f,-0.225309f,-0.5f),
//			new Vector3(0.0540037f,-0.225309f,0.5f),
//			new Vector3(-0.217051f,-0.197398f,0.5f),
//			new Vector3(-0.217051f,-0.197398f,-0.5f),
//			new Vector3(-0.151959f,-0.0789192f,0.5f),
//			new Vector3(-0.104137f,-0.077778f,0.5f),
//			new Vector3(-0.104137f,-0.077778f,-0.5f),
//			new Vector3(-0.151959f,-0.0789192f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.304973f,-0.213889f,-0.5f),
//			new Vector3(-0.217051f,-0.197398f,-0.5f),
//			new Vector3(-0.426258f,0.124114f,-0.5f),
//			new Vector3(-0.151959f,-0.0789192f,0.5f),
//			new Vector3(-0.304973f,-0.213889f,0.5f),
//			new Vector3(-0.217051f,-0.197398f,0.5f),
//			new Vector3(-0.426258f,0.124114f,0.5f),
//			new Vector3(-0.151959f,-0.0789192f,-0.5f),
//			new Vector3(-0.254596f,0.0110088f,-0.5f),
//			new Vector3(-0.254596f,0.0110088f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-1f,-1f,-0.5f),
//			new Vector3(-0.304973f,-0.213889f,-0.5f),
//			new Vector3(-0.186329f,-0.243634f,-0.5f),
//			new Vector3(-0.217051f,-0.197398f,0.5f),
//			new Vector3(-1f,-1f,0.5f),
//			new Vector3(-0.0660014f,-1f,-0.5f),
//			new Vector3(-0.217051f,-0.197398f,-0.5f),
//			new Vector3(-0.304973f,-0.213889f,0.5f),
//			new Vector3(-0.186329f,-0.243634f,0.5f),
//			new Vector3(-0.0660014f,-1f,0.5f),
//			new Vector3(-1f,-0.960555f,-0.5f),
//			new Vector3(-1f,-0.960555f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.0660014f,-1f,-0.5f),
//			new Vector3(0.298754f,-0.441025f,-0.5f),
//			new Vector3(-0.186329f,-0.243634f,-0.5f),
//			new Vector3(0.0854744f,-0.245537f,0.5f),
//			new Vector3(-0.0660014f,-1f,0.5f),
//			new Vector3(0.607584f,-1f,-0.5f),
//			new Vector3(-0.186329f,-0.243634f,0.5f),
//			new Vector3(0.0854744f,-0.245537f,-0.5f),
//			new Vector3(0.0540037f,-0.225309f,-0.5f),
//			new Vector3(0.0540037f,-0.225309f,0.5f),
//			new Vector3(0.298754f,-0.441025f,0.5f),
//			new Vector3(0.607584f,-1f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.00424733f,0.0677186f,-0.5f),
//			new Vector3(0.0854744f,-0.245537f,0.5f),
//			new Vector3(-0.104137f,-0.077778f,-0.5f),
//			new Vector3(0.0421851f,0.0652789f,-0.5f),
//			new Vector3(-0.104137f,-0.077778f,0.5f),
//			new Vector3(0.0540037f,-0.225309f,0.5f),
//			new Vector3(-0.00424733f,0.0677186f,0.5f),
//			new Vector3(0.0421851f,0.0652789f,0.5f),
//			new Vector3(0.0540037f,-0.225309f,-0.5f),
//			new Vector3(0.0854744f,-0.245537f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.22418f,0.03625f,0.5f),
//			new Vector3(0.298754f,-0.441025f,-0.5f),
//			new Vector3(0.0421851f,0.0652789f,-0.5f),
//			new Vector3(0.0854744f,-0.245537f,-0.5f),
//			new Vector3(0.298754f,-0.441025f,0.5f),
//			new Vector3(0.22418f,0.03625f,-0.5f),
//			new Vector3(0.0421851f,0.0652789f,0.5f),
//			new Vector3(0.0854744f,-0.245537f,0.5f),
//			new Vector3(0.174432f,0.0976613f,-0.5f),
//			new Vector3(0.174432f,0.0976613f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.0692106f,0.1823f,-0.5f),
//			new Vector3(-0.0473917f,0.169589f,-0.5f),
//			new Vector3(-0.150062f,0.303337f,-0.5f),
//			new Vector3(0.0137812f,0.261091f,0.5f),
//			new Vector3(-0.0692106f,0.1823f,0.5f),
//			new Vector3(-0.00990032f,0.297795f,-0.5f),
//			new Vector3(-0.150062f,0.303337f,0.5f),
//			new Vector3(-0.0296384f,0.314197f,-0.5f),
//			new Vector3(-0.0473917f,0.169589f,0.5f),
//			new Vector3(0.0137812f,0.261091f,-0.5f),
//			new Vector3(-0.0296384f,0.314197f,0.5f),
//			new Vector3(-0.00990032f,0.297795f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.0828026f,0.091434f,0.5f),
//			new Vector3(-0.426258f,0.124114f,0.5f),
//			new Vector3(-0.254596f,0.0110088f,0.5f),
//			new Vector3(-0.254596f,0.0110088f,-0.5f),
//			new Vector3(-0.0828026f,0.091434f,-0.5f),
//			new Vector3(-0.201087f,0.146032f,0.5f),
//			new Vector3(-0.201087f,0.146032f,-0.5f),
//			new Vector3(-0.426258f,0.124114f,-0.5f),
//			new Vector3(-0.479205f,0.176951f,0.5f),
//			new Vector3(-0.479205f,0.176951f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.479205f,0.176951f,-0.5f),
//			new Vector3(-0.0692106f,0.1823f,0.5f),
//			new Vector3(-1f,0.582126f,-0.5f),
//			new Vector3(-0.150062f,0.303337f,-0.5f),
//			new Vector3(-1f,0.55324f,-0.5f),
//			new Vector3(-0.201087f,0.146032f,0.5f),
//			new Vector3(-1f,0.582126f,0.5f),
//			new Vector3(-0.150062f,0.303337f,0.5f),
//			new Vector3(-0.201087f,0.146032f,-0.5f),
//			new Vector3(-0.0692106f,0.1823f,-0.5f),
//			new Vector3(-0.479205f,0.176951f,0.5f),
//			new Vector3(-1f,0.55324f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.201087f,0.146032f,0.5f),
//			new Vector3(-0.0692106f,0.1823f,0.5f),
//			new Vector3(-0.0473917f,0.169589f,0.5f),
//			new Vector3(-0.0179331f,0.0835178f,0.5f),
//			new Vector3(-0.0828026f,0.091434f,0.5f),
//			new Vector3(-0.0828026f,0.091434f,0f),
//			new Vector3(-0.201087f,0.146032f,-0.5f),
//			new Vector3(-0.0692106f,0.1823f,-0.5f),
//			new Vector3(-0.0828026f,0.091434f,-0.5f),
//			new Vector3(-0.0179331f,0.0835178f,-0.5f),
//			new Vector3(-0.0473917f,0.169589f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.254596f,0.0110088f,0.5f),
//			new Vector3(-0.151959f,-0.0789192f,0.5f),
//			new Vector3(-0.104137f,-0.077778f,0.5f),
//			new Vector3(-0.0828026f,0.091434f,-0.5f),
//			new Vector3(-0.0179331f,0.0835178f,-0.5f),
//			new Vector3(-0.0828026f,0.091434f,0.5f),
//			new Vector3(-0.0179331f,0.0835178f,0.5f),
//			new Vector3(-0.104137f,-0.077778f,-0.5f),
//			new Vector3(-0.254596f,0.0110088f,-0.5f),
//			new Vector3(-0.151959f,-0.0789192f,-0.5f),
//			new Vector3(-0.011291f,0.0763045f,0.5f),
//			new Vector3(-0.011291f,0.0763045f,-0.5f),
//			new Vector3(-0.00424733f,0.0677186f,-0.5f),
//			new Vector3(-0.00424733f,0.0677186f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.011291f,0.0763045f,0.5f),
//			new Vector3(-0.011291f,0.0763045f,-0.5f),
//			new Vector3(0.189929f,0.226259f,0.5f),
//			new Vector3(0.189929f,0.226259f,-0.5f),
//			new Vector3(0.174432f,0.0976613f,0.5f),
//			new Vector3(0.0421851f,0.0652789f,0.5f),
//			new Vector3(0.0536377f,0.249979f,-0.5f),
//			new Vector3(0.0536377f,0.249979f,0.5f),
//			new Vector3(-0.00424733f,0.0677186f,0.5f),
//			new Vector3(-0.00424733f,0.0677186f,-0.5f),
//			new Vector3(0.0421851f,0.0652789f,-0.5f),
//			new Vector3(0.174432f,0.0976613f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.22418f,0.03625f,-0.5f),
//			new Vector3(1f,0.27234f,-0.5f),
//			new Vector3(1f,0.792096f,-0.5f),
//			new Vector3(1f,0.792096f,0.5f),
//			new Vector3(0.174432f,0.0976613f,-0.5f),
//			new Vector3(1f,0.27234f,0.5f),
//			new Vector3(0.271967f,0.357577f,-0.5f),
//			new Vector3(0.271967f,0.357577f,0.5f),
//			new Vector3(0.189929f,0.226259f,-0.5f),
//			new Vector3(0.189929f,0.226259f,0.5f),
//			new Vector3(0.22418f,0.03625f,0.5f),
//			new Vector3(0.174432f,0.0976613f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.0473917f,0.169589f,0.5f),
//			new Vector3(0.0536377f,0.249979f,0.5f),
//			new Vector3(0.0137812f,0.261091f,0.5f),
//			new Vector3(-0.0179331f,0.0835178f,0.5f),
//			new Vector3(-0.011291f,0.0763045f,0.5f),
//			//new Vector3(-0.011291f,0.0763045f,0f),
//			new Vector3(-0.0473917f,0.169589f,-0.5f),
//			new Vector3(0.0536377f,0.249979f,-0.5f),
//			new Vector3(0.0137812f,0.261091f,-0.5f),
//			new Vector3(-0.0179331f,0.0835178f,-0.5f),
//			new Vector3(-0.011291f,0.0763045f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.0536377f,0.249979f,-0.5f),
//			new Vector3(0.189929f,0.226259f,-0.5f),
//			new Vector3(0.140247f,0.396627f,0.5f),
//			new Vector3(0.271967f,0.357577f,0.5f),
//			new Vector3(0.0137812f,0.261091f,0.5f),
//			new Vector3(0.189929f,0.226259f,0.5f),
//			new Vector3(0.0536377f,0.249979f,0.5f),
//			new Vector3(0.0137812f,0.261091f,-0.5f),
//			new Vector3(0.271967f,0.357577f,-0.5f),
//			new Vector3(0.140247f,0.396627f,-0.5f),
//			new Vector3(-0.00990032f,0.297795f,0.5f),
//			new Vector3(-0.00990032f,0.297795f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.607584f,-1f,0.5f),
//			new Vector3(1f,-1f,-0.5f),
//			new Vector3(0.22418f,0.03625f,-0.5f),
//			new Vector3(1f,0.27234f,-0.5f),
//			new Vector3(0.298754f,-0.441025f,0.5f),
//			new Vector3(1f,-1f,0.5f),
//			new Vector3(0.22418f,0.03625f,0.5f),
//			new Vector3(1f,0.27234f,0.5f),
//			new Vector3(0.298754f,-0.441025f,-0.5f),
//			new Vector3(0.607584f,-1f,-0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.150062f,0.303337f,-0.5f),
//			new Vector3(-0.0296384f,0.314197f,-0.5f),
//			new Vector3(-1f,1f,-0.5f),
//			new Vector3(-0.360437f,1f,0.5f),
//			new Vector3(-1f,0.582126f,-0.5f),
//			new Vector3(-0.0296384f,0.314197f,0.5f),
//			new Vector3(-1f,1f,0.5f),
//			new Vector3(0.00799953f,0.465615f,0.5f),
//			new Vector3(0.00799953f,0.465615f,-0.5f),
//			new Vector3(-0.360437f,1f,-0.5f),
//			new Vector3(-0.150062f,0.303337f,0.5f),
//			new Vector3(-1f,0.582126f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.140247f,0.396627f,0.5f),
//			new Vector3(0.140247f,0.396627f,-0.5f),
//			new Vector3(0.00799953f,0.465615f,-0.5f),
//			new Vector3(0.00799953f,0.465615f,0.5f),
//			new Vector3(-0.0296384f,0.314197f,-0.5f),
//			new Vector3(-0.0296384f,0.314197f,0.5f),
//			new Vector3(-0.00990032f,0.297795f,-0.5f),
//			new Vector3(-0.00990032f,0.297795f,0.5f),
//
//		});
//
//		vc.Add(new List<Vector3>(){
//			new Vector3(0.00799953f,0.465615f,-0.5f),
//			new Vector3(1f,0.792096f,0.5f),
//			new Vector3(-0.360437f,1f,-0.5f),
//			new Vector3(1f,1f,-0.5f),
//			new Vector3(0.00799953f,0.465615f,0.5f),
//			new Vector3(0.140247f,0.396627f,-0.5f),
//			new Vector3(-0.360437f,1f,0.5f),
//			new Vector3(1f,1f,0.5f),
//			new Vector3(0.271967f,0.357577f,-0.5f),
//			new Vector3(1f,0.792096f,-0.5f),
//			new Vector3(0.271967f,0.357577f,0.5f),
//			new Vector3(0.140247f,0.396627f,0.5f),
//
//		});


//		vc.Add(new List<Vector3>(){
//			new Vector3(-0.806506f,0.343989f,-0.5f),
//			new Vector3(0.0101658f,0.420679f,0.5f),
//			new Vector3(0.0101658f,0.420679f,-0.5f),
//			new Vector3(-0.14343f,0.205019f,0.5f),
//			new Vector3(-0.14343f,0.205019f,-0.5f),
//			//new Vector3(-0.806506f,0.343989f,0f),
//			new Vector3(-0.806506f,0.343989f,0.5f),
//
//		});



		vc.Add(new List<Vector3>(){
			new Vector3(-1f,-1f,-0.5f),
			new Vector3(-0.0448597f,-0.598239f,-0.5f),
			new Vector3(-1f,-0.362044f,-0.5f),
			new Vector3(-0.385575f,-0.221627f,-0.5f),
			new Vector3(-1f,-1f,0.5f),
			new Vector3(-0.385575f,-0.221627f,0.5f),
			new Vector3(-1f,-0.362044f,0.5f),
			new Vector3(-0.103467f,-1f,-0.5f),
			new Vector3(-0.18347f,-0.420897f,-0.5f),
			new Vector3(-0.18347f,-0.420897f,0.5f),
			new Vector3(-0.0448597f,-0.598239f,0.5f),
			new Vector3(-0.103467f,-1f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.0448597f,-0.598239f,-0.5f),
			new Vector3(1f,-1f,-0.5f),
			new Vector3(-0.103467f,-1f,0.5f),
			new Vector3(0.518295f,-0.459347f,-0.5f),
			new Vector3(-0.103467f,-1f,-0.5f),
			new Vector3(1f,-1f,0.5f),
			new Vector3(0.0743321f,-0.34692f,-0.5f),
			new Vector3(1f,-0.709246f,-0.5f),
			new Vector3(0.518295f,-0.459347f,0.5f),
			new Vector3(1f,-0.709246f,0.5f),
			new Vector3(-0.0448597f,-0.598239f,0.5f),
			new Vector3(0.0743321f,-0.34692f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(0.0743321f,-0.34692f,0.5f),
			new Vector3(0.518295f,-0.459347f,-0.5f),
			new Vector3(0.222638f,-0.250217f,-0.5f),
			new Vector3(0.356161f,-0.286853f,-0.5f),
			new Vector3(0.079184f,-0.323635f,0.5f),
			new Vector3(0.518295f,-0.459347f,0.5f),
			new Vector3(0.222638f,-0.250217f,0.5f),
			new Vector3(0.356161f,-0.286853f,0.5f),
			new Vector3(0.079184f,-0.323635f,-0.5f),
			new Vector3(0.0743321f,-0.34692f,-0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.385575f,-0.221627f,-0.5f),
			new Vector3(-1f,0.144961f,0.5f),
			new Vector3(-0.312959f,-0.137179f,-0.5f),
			new Vector3(-0.684528f,0.136085f,0.5f),
			new Vector3(-1f,-0.362044f,-0.5f),
			new Vector3(-0.312959f,-0.137179f,0.5f),
			new Vector3(-0.684528f,0.136085f,-0.5f),
			new Vector3(-1f,0.144961f,-0.5f),
			new Vector3(-0.385575f,-0.221627f,0.5f),
			new Vector3(-1f,-0.362044f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.684528f,0.136085f,-0.5f),
			new Vector3(-0.312959f,-0.137179f,0.5f),
			new Vector3(-0.283868f,0.0376008f,0.5f),
			new Vector3(-0.312959f,-0.137179f,-0.5f),
			new Vector3(-0.343069f,0.143474f,0.5f),
			new Vector3(-0.312229f,-0.136512f,0.5f),
			new Vector3(-0.684528f,0.136085f,0.5f),
			new Vector3(-0.312229f,-0.136512f,-0.5f),
			new Vector3(-0.283868f,0.0376008f,-0.5f),
			new Vector3(-0.343069f,0.143474f,-0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.312229f,-0.136512f,0.5f),
			new Vector3(-0.385575f,-0.221627f,-0.5f),
			new Vector3(-0.312229f,-0.136512f,-0.5f),
			new Vector3(-0.216008f,-0.160551f,-0.5f),
			new Vector3(-0.18347f,-0.420897f,0.5f),
			new Vector3(-0.18347f,-0.420897f,-0.5f),
			new Vector3(-0.312959f,-0.137179f,-0.5f),
			new Vector3(-0.216008f,-0.160551f,0.5f),
			new Vector3(-0.312959f,-0.137179f,0.5f),
			new Vector3(-0.385575f,-0.221627f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.312229f,-0.136512f,-0.5f),
			new Vector3(-0.216008f,-0.160551f,-0.5f),
			new Vector3(0.0139972f,-0.15204f,-0.5f),
			new Vector3(0.0153618f,-0.0473141f,-0.5f),
			new Vector3(-0.312229f,-0.136512f,0.5f),
			new Vector3(-0.216008f,-0.160551f,0.5f),
			new Vector3(-0.283868f,0.0376008f,-0.5f),
			new Vector3(-0.283868f,0.0376008f,0.5f),
			new Vector3(-0.0237972f,-0.020674f,-0.5f),
			new Vector3(-0.0237972f,-0.020674f,0.5f),
			new Vector3(0.0153618f,-0.0473141f,0.5f),
			new Vector3(0.0139972f,-0.15204f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.0448597f,-0.598239f,0.5f),
			new Vector3(0.079184f,-0.323635f,-0.5f),
			new Vector3(-0.216008f,-0.160551f,-0.5f),
			new Vector3(0.0139972f,-0.15204f,-0.5f),
			new Vector3(-0.18347f,-0.420897f,0.5f),
			new Vector3(0.0743321f,-0.34692f,-0.5f),
			new Vector3(-0.216008f,-0.160551f,0.5f),
			new Vector3(0.0139972f,-0.15204f,0.5f),
			new Vector3(-0.18347f,-0.420897f,-0.5f),
			new Vector3(-0.0448597f,-0.598239f,-0.5f),
			new Vector3(0.079184f,-0.323635f,0.5f),
			new Vector3(0.0743321f,-0.34692f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(0.335603f,-0.119844f,-0.5f),
			new Vector3(0.0139972f,-0.15204f,-0.5f),
			new Vector3(0.079184f,-0.323635f,0.5f),
			new Vector3(0.307884f,0.0428469f,-0.5f),
			new Vector3(0.222638f,-0.250217f,0.5f),
			new Vector3(0.0139972f,-0.15204f,0.5f),
			new Vector3(0.335603f,-0.119844f,0.5f),
			new Vector3(0.307884f,0.0428469f,0.5f),
			new Vector3(0.0277375f,-0.0346146f,-0.5f),
			new Vector3(0.0153618f,-0.0473141f,-0.5f),
			new Vector3(0.079184f,-0.323635f,-0.5f),
			new Vector3(0.222638f,-0.250217f,-0.5f),
			new Vector3(0.0153618f,-0.0473141f,0.5f),
			new Vector3(0.0277375f,-0.0346146f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(0.335603f,-0.119844f,0.5f),
			new Vector3(1f,-0.206532f,-0.5f),
			new Vector3(0.307884f,0.0428469f,-0.5f),
			new Vector3(1f,1f,-0.5f),
			new Vector3(0.335603f,-0.119844f,-0.5f),
			new Vector3(1f,-0.206532f,0.5f),
			new Vector3(0.683389f,0.692466f,0.5f),
			new Vector3(1f,1f,0.5f),
			new Vector3(0.544173f,-0.176223f,-0.5f),
			new Vector3(0.544173f,-0.176223f,0.5f),
			new Vector3(0.307884f,0.0428469f,0.5f),
			new Vector3(0.906816f,1f,0.5f),
			new Vector3(0.906816f,1f,-0.5f),
			new Vector3(0.683389f,0.692466f,-0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(0.356161f,-0.286853f,-0.5f),
			new Vector3(1f,-0.709246f,0.5f),
			new Vector3(0.544173f,-0.176223f,-0.5f),
			new Vector3(1f,-0.206532f,-0.5f),
			new Vector3(0.356161f,-0.286853f,0.5f),
			new Vector3(0.518295f,-0.459347f,0.5f),
			new Vector3(0.544173f,-0.176223f,0.5f),
			new Vector3(1f,-0.206532f,0.5f),
			new Vector3(0.518295f,-0.459347f,-0.5f),
			new Vector3(1f,-0.709246f,-0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(0.335603f,-0.119844f,-0.5f),
			new Vector3(0.222638f,-0.250217f,-0.5f),
			new Vector3(0.335603f,-0.119844f,0.5f),
			new Vector3(0.544173f,-0.176223f,-0.5f),
			new Vector3(0.356161f,-0.286853f,-0.5f),
			new Vector3(0.544173f,-0.176223f,0.5f),
			new Vector3(0.356161f,-0.286853f,0.5f),
			new Vector3(0.222638f,-0.250217f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-1f,0.144961f,0.5f),
			new Vector3(-0.300478f,0.206166f,0.5f),
			new Vector3(-1f,1f,-0.5f),
			new Vector3(-0.738741f,1f,-0.5f),
			new Vector3(-1f,0.144961f,-0.5f),
			new Vector3(-0.332252f,0.156636f,-0.5f),
			new Vector3(-1f,1f,0.5f),
			new Vector3(-0.738741f,1f,0.5f),
			new Vector3(-0.302425f,0.202496f,-0.5f),
			new Vector3(-0.300478f,0.206166f,-0.5f),
			new Vector3(-0.302425f,0.202496f,0.5f),
			new Vector3(-0.332252f,0.156636f,0.5f),
			new Vector3(-0.343069f,0.143474f,0.5f),
			new Vector3(-0.343069f,0.143474f,-0.5f),
			new Vector3(-0.684528f,0.136085f,0.5f),
			new Vector3(-0.684528f,0.136085f,-0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.283868f,0.0376008f,0.5f),
			new Vector3(-0.0237972f,-0.020674f,-0.5f),
			new Vector3(-0.332252f,0.156636f,-0.5f),
			new Vector3(-0.0815209f,0.114455f,-0.5f),
			new Vector3(-0.343069f,0.143474f,0.5f),
			new Vector3(-0.0237972f,-0.020674f,0.5f),
			new Vector3(-0.332252f,0.156636f,0.5f),
			new Vector3(-0.0815209f,0.114455f,0.5f),
			new Vector3(-0.343069f,0.143474f,-0.5f),
			new Vector3(-0.283868f,0.0376008f,-0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.0986741f,0.419068f,0.5f),
			new Vector3(-0.0840575f,0.147685f,-0.5f),
			new Vector3(-0.0986741f,0.419068f,-0.5f),
			new Vector3(-0.0585627f,0.273246f,-0.5f),
			new Vector3(-0.302425f,0.202496f,-0.5f),
			new Vector3(-0.0840575f,0.147685f,0.5f),
			new Vector3(-0.302425f,0.202496f,0.5f),
			new Vector3(-0.0585627f,0.273246f,0.5f),
			new Vector3(-0.300478f,0.206166f,-0.5f),
			new Vector3(-0.300478f,0.206166f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.0840575f,0.147685f,0.5f),
			new Vector3(-0.0840575f,0.147685f,-0.5f),
			new Vector3(-0.0585627f,0.273246f,0.5f),
			new Vector3(0.0277375f,-0.0346146f,0.5f),
			new Vector3(-0.0237972f,-0.020674f,-0.5f),
			new Vector3(0.0675767f,0.128497f,0.5f),
			new Vector3(-0.0585627f,0.273246f,-0.5f),
			new Vector3(0.0675767f,0.128497f,-0.5f),
			new Vector3(-0.0815209f,0.114455f,0.5f),
			new Vector3(-0.0815209f,0.114455f,-0.5f),
			new Vector3(0.0153618f,-0.0473141f,0.5f),
			new Vector3(-0.0237972f,-0.020674f,0.5f),
			new Vector3(0.0153618f,-0.0473141f,-0.5f),
			new Vector3(0.0277375f,-0.0346146f,-0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.332252f,0.156636f,-0.5f),
			new Vector3(-0.302425f,0.202496f,-0.5f),
			new Vector3(-0.0815209f,0.114455f,0.5f),
			new Vector3(-0.302425f,0.202496f,0.5f),
			new Vector3(-0.332252f,0.156636f,0.5f),
			new Vector3(-0.0815209f,0.114455f,-0.5f),
			new Vector3(-0.0840575f,0.147685f,-0.5f),
			new Vector3(-0.0840575f,0.147685f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(0.683389f,0.692466f,0.5f),
			new Vector3(0.0277375f,-0.0346146f,-0.5f),
			new Vector3(0.0675767f,0.128497f,-0.5f),
			new Vector3(0.683389f,0.692466f,-0.5f),
			new Vector3(0.307884f,0.0428469f,0.5f),
			new Vector3(0.307884f,0.0428469f,-0.5f),
			new Vector3(0.0277375f,-0.0346146f,0.5f),
			new Vector3(0.0675767f,0.128497f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(0.0675767f,0.128497f,-0.5f),
			new Vector3(0.683389f,0.692466f,-0.5f),
			new Vector3(-0.0585627f,0.273246f,0.5f),
			new Vector3(0.906816f,1f,-0.5f),
			new Vector3(0.0675767f,0.128497f,0.5f),
			new Vector3(0.683389f,0.692466f,0.5f),
			new Vector3(0.114596f,1f,-0.5f),
			new Vector3(0.906816f,1f,0.5f),
			new Vector3(-0.0986741f,0.419068f,-0.5f),
			new Vector3(-0.0585627f,0.273246f,-0.5f),
			new Vector3(-0.0986741f,0.419068f,0.5f),
			new Vector3(0.114596f,1f,0.5f),

		});

		vc.Add(new List<Vector3>(){
			new Vector3(-0.738741f,1f,0.5f),
			new Vector3(0.114596f,1f,-0.5f),
			new Vector3(-0.738741f,1f,-0.5f),
			new Vector3(-0.300478f,0.206166f,-0.5f),
			new Vector3(-0.300478f,0.206166f,0.5f),
			new Vector3(-0.0986741f,0.419068f,-0.5f),
			new Vector3(0.114596f,1f,0.5f),
			new Vector3(-0.0986741f,0.419068f,0.5f),

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
			//add a random control point in the center?
//
//			vv.Add( Vector3.Scale( Vector3.Normalize (new Vector3 (randomVerts [1].x, randomVerts [1].y, randomVerts [1].z)
//				- new Vector3 (randomVerts [0].x, randomVerts [0].y, randomVerts [0].z)), new Vector3(0.1f,0.1f,0.1f)) 
//				+ new Vector3 (randomVerts [0].x, randomVerts [0].y, randomVerts [0].z));

			//control point seems to have to be in z-depth?

			//vv.Add(new Vector3(randomVerts[1].x, randomVerts[1].y, 0f);
			Debug.Log(String.Format("{0} - {1} - {2}", cc, vv.Count(), randomVerts.Count));
			foreach (var v in vv) {
				
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
			//var delaunay = Triangulation.CreateDelaunay<Vertex> (vertices);
			//var c = Triangulation.CreateDelaunay<Vertex, Tetrahedron>(vertices).Cells;
			//verticesList = new List<Vector3> ();
			//lines = new List<Vector3[]> ();


			var convexHull = ConvexHull.Create<Vertex>(vertices);
			foreach (var f in convexHull.Faces) {
				
				_CreateTriangle(f.ToString (), new Vector3[]{ 
					_CreateVector3FromPosition(f.Vertices[0]),
					_CreateVector3FromPosition(f.Vertices[1]),
					_CreateVector3FromPosition(f.Vertices[2])
				});


			}

//			foreach (var e in voronoi.Vertices) {
//				//CreateTriangles (e);
//				CreateTetrahedron (cc, e,color);
//			}
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

	Vector3 _CreateVector3FromPosition(Vertex p){
		return new Vector3 ((float)p.Position [0], (float)p.Position [1], (float)p.Position [2]);
	}

	static void _CreateTriangle(String e, Vector3[] vertices){
						var go = new GameObject();
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
						go.GetComponent<Renderer> ().material.color = Color.green;
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

		//			foreach (Vertex sv in e.Source.Vertices) {
		//				
		//				mesh.vertices =  new Vector3[] {new Vector3(sv.Position[0], 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)};
		//			
		//			}
		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
		mesh.triangles = new int[] { 0, 1, 2 };

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		go.GetComponent<Renderer> ().material.color = Color.blue;
//		new Color (UnityEngine.Random.Range (0.0f, 1.0f), 
//			UnityEngine.Random.Range (0.0f, 1.0f), 
//			UnityEngine.Random.Range (0.0f, 1.0f));
//		
	}

	static Vector3 SurfaceNormalForTriangle(Vector3 v1, Vector3 v2, Vector3 v3){
		Vector3 a = v2 - v1;
		Vector3 b = v3 - v1;
		return Vector3.Cross (a, b);

	}
	void CreateTetrahedron(int cc,DefaultTriangulationCell<Vertex> e, Color color){
		//Debug.Log (e.Vertices.Count());
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
			2,1,0,
			5,4,3,
			8,7,6,
			11,10,9
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
