using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public BrickController brickPrefab;
	public int rows = 5;
	public int columns = 10;

	public Text livesText;
	public Text scoreText;
	public Text highScoreText;
	public Text gameOverText;
	public Text winStateText;

	public int lives = 3;
	public int score = 0;
	public int highScore = 30;
	public int brickCount = 0;

	public List<GameObject> brickList;

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
		highScoreText.text = "High Score: " + highScore; 
		CreateBrickObjectList ();
		CreateBricks ();
	}

	void CreateBricks(){
		for (int row = 0; rows < rows; rows++) {
			for(int col = 0; col < columns; col++){
				BrickController brick = Instantiate (brickPrefab) as BrickController;
				brick.transform.position = new Vector3 (columns,rows,0); //Need to fix. Bricks not spawning.
			}
		}
	}

	public static void LostBall(){
		instance.lives--;
		if (instance.lives < 0) {
			instance.gameOverText.text = "You Lose!";
			instance.gameOverText.gameObject.SetActive (true);
		} else {
			instance.livesText.text = "Lives: " + instance.lives;
		}
	}

	public static void BrickBroken (int points){
		instance.score += points;
		instance.scoreText.text = "Score: " + instance.score;
		if (instance.score > instance.highScore) { //Update High Score
			instance.highScore = instance.score;
			instance.highScoreText.text = "High Score: " + instance.highScore;
		}
	}

	public static void AllBricksBroken(){
		if(instance.brickList.Count <= 0){
			Debug.Log ("All bricks broken.");
			instance.winStateText.text = "You Win!";
			instance.winStateText.gameObject.SetActive (true);
		}
	}

	public void CreateBrickObjectList(){
		brickList = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Brick"));
	}
		
}
