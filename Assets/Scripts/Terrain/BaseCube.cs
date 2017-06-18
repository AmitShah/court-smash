using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MIConvexHull;

public class Baseube : MonoBehaviour {
		

	List<List<Vector3>> vertices;
	bool isDestroyed;

	Vector3 Location;
	GameObject parent;
	GameObject[] childern;
	int health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void BreakCube(Vector3 breakOrigin){
		//Generate Voronoi, add force to each parent based on circumcentre to origin
		//For each face, hold the normal

	}

	void OnHit(){
		//TODO Glow and hit 
		//reduce by some variable amount of health

	}


}
