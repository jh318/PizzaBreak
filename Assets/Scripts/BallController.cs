using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public float speed = 2.0f;

	Rigidbody body;

	void Start () {
		body = GetComponent<Rigidbody> ();
		Launch ();
	}

	void Launch () {
		transform.position = PaddleController.instance.transform.position + Vector3.up;
		body.velocity = Vector3.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 view = Camera.main.WorldToViewportPoint (transform.position);
		if (view.y < 0) { 
			GameManager.LostBall ();
			Launch ();
		}

	}


}
