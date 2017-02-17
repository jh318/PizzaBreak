using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	public float speed = 2.0f;
	public ParticleSystem hitParticlesPrefab;
	public ParticleSystem paddleParticles;
	List<ParticleSystem> particlePool = new List<ParticleSystem> ();

	Rigidbody body;
	AudioSource sound;

	void Start () {
		body = GetComponent<Rigidbody> ();
		sound = GetComponent<AudioSource> ();
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
		ParticleSystem hitParticles = null;
		for (int i = 0; i < particlePool.Count; i++) {
			ParticleSystem p = particlePool [i];
			if (p.isStopped) {
				hitParticles = p;
				Debug.Log ("reusing from my pool");
				break;
			}
		}
		if (hitParticles == null) {
			hitParticles = Instantiate(hitParticlesPrefab) as ParticleSystem;
			particlePool.Add (hitParticles);
		}
		sound.pitch = Random.Range (0.9f, 1.1f);
		sound.volume = Random.Range (0.8f, 1f);
		sound.Play ();
	}
}