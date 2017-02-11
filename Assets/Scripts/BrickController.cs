using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour {
	public int points = 10;

	void Awake(){

	}

	void Start(){
		//GameManager.instance.BrickCount++;

	}

	void OnEnable(){
		//GameManager.instance.brickCount++;

	}

	void OnCollisionExit(Collision collision){
		gameObject.SetActive (false);
		GameManager.BrickBroken (points);
		GameManager.instance.brickList.Remove(gameObject);
		GameManager.AllBricksBroken();
	}
}
