using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public Text livesText;
	public Text scoreText;
	public Text highScoreText;

	public int lives = 3;
	public int score = 0;

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
		livesText.text = "Lives: " + lives;
		scoreText.text = "Scores: " + score;
	}
	public static void LostBall(){
		instance.lives--;
		instance.livesText.text = "Lives: " + instance.lives;
	}

	public static void BrickBroken (int points){
		instance.score += points;
		instance.scoreText.text = "Score: " + instance.score;
	}

}
