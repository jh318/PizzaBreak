using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour {
	public int points = 10;

	void OnCollisionExit(Collision collision){
		gameObject.SetActive (false);
		GameManager.BrickBroken (points);

	}
}
