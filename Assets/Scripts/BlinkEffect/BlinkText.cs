using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkText : MonoBehaviour {

	public float BlinkFrequency;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var color =GetComponent<TextMesh> ().color;

		this.GetComponent<TextMesh>().color= new Color( color.r, color.g, color.b, 0.1f + .9f*(0.5f+Mathf.Sin(Time.time*BlinkFrequency)));
	}
}
