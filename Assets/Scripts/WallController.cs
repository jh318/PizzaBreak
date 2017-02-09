using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

	public float viewX = 0;

	// Use this for initialization
	void Start () {
		Vector3 v = Camera.main.WorldToViewportPoint(transform.position);
		v.x = viewX;
		transform.position = Camera.main.ViewportToWorldPoint(v);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
