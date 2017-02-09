using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour {

	
	void OnCollisionExit(Collision collision){
		gameObject.SetActive (false);
	}
}
