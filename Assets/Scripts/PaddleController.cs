using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

	public float speed = 2.0f;
	public float tilt = 3;

	void Update () {
		float playerInputX = Input.GetAxis ("Horizontal") * speed * Time.deltaTime; 
		transform.position = transform.position + new Vector3(playerInputX, 0, 0); // current position + Vector3.right * playerInputX
		transform.localEulerAngles = Vector3.back * tilt * playerInputX;
	}
}
