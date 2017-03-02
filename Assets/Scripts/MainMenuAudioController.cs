using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioController : MonoBehaviour {
	public AudioClip[] audioClips;
	public AudioSource sound;

	void Start () {
		sound.clip = audioClips[(Random.Range(0,audioClips.Length))];
		sound.Play();
	}
}
