using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

	public static PaddleController instance;
	public float speed = 2.0f;
	public float tilt = 3;

	Renderer renderer;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	void Start(){
		renderer = GetComponentInChildren<Renderer>();		
	}
	void Update () {
		float playerInputX = Input.GetAxis ("Horizontal") * speed * Time.deltaTime; 
		transform.position = transform.position + new Vector3(playerInputX, 0, 0); // current position + Vector3.right * playerInputX
		transform.localEulerAngles = Vector3.back * tilt * playerInputX;
		ClampToScreen (renderer.bounds.extents.x);
		ClampToScreen (-renderer.bounds.extents.x);

	}

	void ClampToScreen(float xOffset){
		Vector3 v = Camera.main.WorldToViewportPoint (transform.position + Vector3.right * xOffset);
		v.x = Mathf.Clamp01 (v.x);
		transform.position = Camera.main.ViewportToWorldPoint (v) - Vector3.right * xOffset;
	}
		
}
