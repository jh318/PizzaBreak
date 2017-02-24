using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public float speed = 2.0f;
	public ParticleSystem hitParticlesPrefab;
	public ParticleSystem paddleParticles;
	List<ParticleSystem> particlePool = new List<ParticleSystem> ();

	Rigidbody body;
	public AudioSource sound;
	public AudioClip paddleHitSfx;
	public AudioClip brickHitSfx;
	public AudioClip wallHitSfx;

	private int pizzaHitCount = 8;

	void Start () {
		sound = GetComponent<AudioSource> ();
		body = GetComponent<Rigidbody> ();
		PreLaunch ();
	}

	void PreLaunch(){
		body.velocity = Vector3.zero;
		transform.SetParent(PaddleController.instance.transform);
		transform.localPosition = Vector3.up;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).gameObject.SetActive (true);
			pizzaHitCount = 8;
		}
	}

	void Launch () {
		transform.SetParent (null);
		body.velocity = Vector3.up * speed;
	}
	
	void Update () {
		if(transform.parent == null){
			Vector3 v = body.velocity;
			if (Mathf.Abs (v.x) > Mathf.Abs (v.y)) {
				v.x *= 0.9f;
			}
			v = v.normalized * speed;
			body.velocity = v;
			transform.up = v; //procedural ball thing
			transform.localScale = new Vector3(0.9f,1.1f,1f);
			DeathCheck ();
		}
		else{
			transform.localScale = Vector3.one;
			if (Input.GetButton("Jump")){
				Launch ();
			}
		}
	}
		
	void OnCollisionEnter(Collision c){
		CameraShake ();
		CollisionParticlePlayer (c);
		SoundPlayer ();
	}

	void CollisionParticlePlayer(Collision c){
		ParticleSystem hitParticles = null;
		for (int i = 0; i < particlePool.Count; i++) { //Particle Pool
			ParticleSystem p = particlePool [i];
			if (p.isStopped) {
				hitParticles = p;
				break;
			}
		}
		if (hitParticles == null) {
			hitParticles = Instantiate(hitParticlesPrefab) as ParticleSystem;
			particlePool.Add (hitParticles);
		}
		if (c.gameObject.tag == "Player") { //Collision with paddle
			paddleParticles.transform.position = transform.position;
			paddleParticles.Play ();
			//Debug.Log ("Hit the paddle");
			sound.clip = paddleHitSfx;
			sound.Play();
		} 
		else if(c.gameObject.tag == "Brick"){
			hitParticles.transform.position = transform.position;
			hitParticles.transform.up = body.velocity;
			hitParticles.Play ();
			//Debug.Log ("Hit a brick");
			sound.clip = brickHitSfx;
			sound.Play();
		}
		else {
			hitParticles.transform.position = transform.position;
			hitParticles.transform.up = body.velocity;
			hitParticles.Play ();
			//Debug.Log ("Hit the wall");
			sound.clip = wallHitSfx;
			sound.Play();
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

	void CameraShake(){
		ShakeController shake = Camera.main.gameObject.GetComponent<ShakeController> ();
		shake.Shake ();
	}

	void SoundPlayer(){
		sound.pitch = Random.Range (0.9f, 1.1f);
		sound.volume = Random.Range (0.8f, 1f);
		sound.Play ();
	}

	void RestorePizza(){
		if (transform.childCount == 0) {
			
		}
	}

	public int GetPizzaHitCount(){ return pizzaHitCount; }
	public void SetPizzaHitCount(int hitCount){ pizzaHitCount += hitCount;}
}

	