using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DialogueController : MonoBehaviour {

	public string[] dialogueLines;
	public Text dialogueLine;
	public string scene;

	void Start(){
		StartCoroutine ("DialogueCoroutine");
	}

	IEnumerator DialogueCoroutine () {
		Debug.Log ("here");
		for (int i = 0; i < dialogueLines.Length; i++) {
		
			dialogueLine.text = dialogueLines [i];
			Debug.Log ("assignment went fine");
			yield return new WaitForSeconds(3);
			Debug.Log ("finished wait for 3 sec");
		}

		SceneManager.LoadScene (scene);
	}

}