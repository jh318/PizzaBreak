using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueController : MonoBehaviour {

	public string[] dialogueLines;
	Text dialogueLine;

	void Start(){
		dialogueLine = GetComponentInChildren<Text> ();
		StartCoroutine ("DialogueCoroutine");
	}

	IEnumerator DialogueCoroutine () {
		for (int i = 0; i < dialogueLines.Length; i++) {
		
			dialogueLine.text = dialogueLines [i];
			yield return new WaitForSeconds(3);
		}

		// load new scene
	}

}