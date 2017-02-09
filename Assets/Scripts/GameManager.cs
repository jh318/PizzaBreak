using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

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
	public static void LostBall(){
		instance.lives--;
		Debug.Log ("ball off screen");
	}

}
