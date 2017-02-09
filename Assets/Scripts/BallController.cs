using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public float speed = 2.0f;

	// Use this for initialization
	void Start () {
		Rigidbody body = GetComponent<Rigidbody> ();
		body.velocity = Vector3.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
			
	}
}
