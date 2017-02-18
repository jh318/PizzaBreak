﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public BrickController brickPrefab;
	public GameObject powerUp;
	public ParticleSystem lifeParticle;
	public ParticleSystem scoreParticle;


	public int rows = 5;
	public int columns = 10;
	[Range(0,1)] public float edgePadding = 0.1f;
	[Range(0,1)] public float bottomPadding = 0.4f;
	List<BrickController> brickList = new List<BrickController> ();

	public Text livesText;
	public Text scoreText;
	public Text highScoreText;
	public Text gameOverText;
	public Text winStateText;

	public int lives = 3;
	public int score = 0;
	public int highScore = 30;
	public int brickCount = 0;

	public AudioSource sound;
	public AudioClip winSfx;
	public AudioClip loseSfx;
	public AudioClip lifeLostSfx;
	//public AudioClip gameMusic;

	void Awake (){
		if (instance == null) {
			instance = this;
		} 
		else {
			Destroy (gameObject);
		}

		Debug.Log ("My score: " + score);
		Debug.Log ("Manager score: " + instance.score);
	}

	void Start (){
		TallyScoreAndDisplay ();
		CreateBricks ();
		sound = GetComponent<AudioSource>();
	}

	void CreateBricks(){
		Vector3 bottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (edgePadding, bottomPadding, 0));
		Vector3 topRight = Camera.main.ViewportToWorldPoint (new Vector3 (1 - edgePadding, 1 - edgePadding, 0));
		bottomLeft.z = 0;
		float w = (topRight.x - bottomLeft.x) / (float)columns;
		float h = (topRight.y - bottomLeft.y) / (float)rows;

		for (int row = 0; row < rows; row++) {
			for(int col = 0; col < columns; col++){
				BrickController brick = Instantiate (brickPrefab) as BrickController;
				brick.transform.position = bottomLeft + new Vector3 ((col + 0.5f) * w, (row + 0.5f) * h,0);
				brickList.Add(brick);
			}
		}
	}

	public static void LostBall(){
		instance.lives--;
		if (instance.lives < 0) {
			instance.gameOverText.text = "You Lose!";
			instance.gameOverText.gameObject.SetActive (true);
			instance.sound.clip = instance.loseSfx;
			instance.sound.Play ();
		} else {
			instance.sound.clip = instance.lifeLostSfx;
			instance.sound.Play ();
			instance.livesText.text = "Lives: " + instance.lives;
			ParticleSystem death = instance.lifeParticle;
			death.Play();
		}
	}

	public static void BrickBroken (int points){
		instance.score += points;
		ParticleSystem p = instance.scoreParticle;
		p.Play ();
		instance.scoreText.text = "Score: " + instance.score;
		if (instance.score > instance.highScore) { //Update High Score
			instance.highScore = instance.score;
			instance.highScoreText.text = "High Score: " + instance.highScore;
		}
		if (Random.value < 0.5) {
			instance.DropPowerUp ();
		}

		AllBricksBroken ();

	}

	public static void AllBricksBroken() {
		bool hasWon = true;
		for (int i = 0; i < instance.brickList.Count; i++) {
			BrickController brick = instance.brickList [i];
			if (brick.gameObject.activeSelf) {
				hasWon = false;
				break;
			}
		}
		if (hasWon) {
			instance.winStateText.text = "You Win!";
			instance.winStateText.gameObject.SetActive (true);
			instance.sound.clip = instance.winSfx;
			instance.sound.Play ();
		}
	}

	void TallyScoreAndDisplay(){
		livesText.text = "Lives: " + lives;
		scoreText.text = "Scores: " + score;
		highScoreText.text = "High Score: " + highScore; 
	}

	void DropPowerUp(){
		GameObject powerP = Instantiate (powerUp);
		powerP.transform.position = GameObject.FindGameObjectWithTag ("Ball").transform.position;
	}
}
