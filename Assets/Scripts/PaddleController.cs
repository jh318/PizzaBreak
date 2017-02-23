using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

	public static PaddleController instance;
	public float speed = 2.0f;
	public float tilt = 3;

	new Renderer renderer;

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

	void OnTriggerEnter(Collider c){
		PowerupController pc = c.gameObject.GetComponent<PowerupController> ();
		if (pc != null) {
			switch (pc.powerupType) {
			case PowerupController.PowerupType.ExtraLife:
				Debug.Log ("You Powered up!!!!!!!!!!!!!!!!!!");
				GameManager.instance.lives++;
				break;
			case PowerupController.PowerupType.SpeedBall:
				Debug.Log ("SPEEEEEEED");
				PaddleController.instance.speed = 20;
				break;
			case PowerupController.PowerupType.SlowBall:
				Debug.Log ("SLOOOOOOOOW");
				PaddleController.instance.speed = 1;
				break;
			default:
				Debug.LogWarning ("DEFAULT");
				GameManager.instance.lives++;
				break;
			}
		}

		/**
		if (c.transform.parent.gameObject.tag == "PowerUp") {
			Debug.Log ("You Powered up!!!!!!!!!!!!!!!!!!");
			GameManager.instance.lives++;
			GameManager.instance.livesText.text = "Lives: " + GameManager.instance.lives;  

		} else if (c.transform.parent.gameObject.tag == "PowerUpSpeedBall") {
			Debug.Log ("SPEEEEEEED");
			PaddleController.instance.speed = 20;


		} else if (c.transform.parent.gameObject.tag == "PowerUpSlowBall") {
			Debug.Log ("SLOOOOOOOOW");
			PaddleController.instance.speed = 1;
		}
		**/
	}

		
}
