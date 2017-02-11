using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public float speed = 2.0f;

	Rigidbody body;

	void Start () {
		body = GetComponent<Rigidbody> ();
		PreLaunch ();
	}

	void PreLaunch(){
		body.velocity = Vector3.zero;
		transform.SetParent(PaddleController.instance.transform);
		transform.localPosition = Vector3.up;
	}

	void Launch () {
		transform.SetParent (null);
		body.velocity = Vector3.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent == null){
		Vector3 v = body.velocity;
		if (Mathf.Abs (v.x) > 2 * Mathf.Abs (v.y)) {
			v.x *= 0.9f;
		}
		v = v.normalized * speed;
		body.velocity = v;
		DeathCheck ();
		}
		else{
			if (Input.GetButton("Jump")){
				Launch ();
			}
		}
	}
		

		void DeathCheck(){
		Vector3 view = Camera.main.WorldToViewportPoint (transform.position);
		if (view.y < 0) { 
			GameManager.LostBall ();
			if (GameManager.instance.lives >= 0) {
				PreLaunch ();
			} else {
				gameObject.SetActive (false);
			}
			PreLaunch ();
		}
	}

	void OnCollisionEnter(Collision c){
		ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController> ();
		shake.Shake ();
	}


}
