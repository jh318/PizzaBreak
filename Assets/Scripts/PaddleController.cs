using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

	public static PaddleController instance;
	public float speed = 2.0f;
	public float tilt = 3;

	new Renderer renderer;
	public AudioSource sound;
	//public AudioClip[] powerUpSFXs;
	public AudioClip extraLargeSFX;
	public AudioClip extraCheeseSFX;
	public AudioClip anchoviesSFX;
	public AudioClip peppersSFX;

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
				Destroy (pc.gameObject);
				sound.clip = extraCheeseSFX;
				sound.Play ();
				break;
			case PowerupController.PowerupType.SpeedBall:
				Debug.Log ("SPEEEEEEED");
				PaddleController.instance.speed = 15;
				Destroy (pc.gameObject);
				sound.clip = peppersSFX;
				sound.Play ();
				break;
			case PowerupController.PowerupType.SlowBall:
				Debug.Log ("SLOOOOOOOOW");
				PaddleController.instance.speed = 5;
				Destroy (pc.gameObject);
				sound.clip = anchoviesSFX;
				sound.Play ();
				break;
			case PowerupController.PowerupType.BigPaddle:
				PaddleController.instance.transform.localScale = new Vector3 (3.0f, 1.0f, 1.0f);
				sound.clip = extraLargeSFX;
				sound.Play ();
				Destroy (pc.gameObject);
				StartCoroutine (ResetPaddleSize());
				break;
			default:
				Debug.LogWarning ("DEFAULT");
				GameManager.instance.lives++;
				Destroy (pc.gameObject);
				break;
			}
		}
	}

	IEnumerator ResetPaddleSize(){
		yield return new WaitForSeconds (3);

		gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		Debug.Log ("Reset");

	}
		
}
