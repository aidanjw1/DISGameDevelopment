/// <summary>
/// Play background music.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject bgmusic = GameObject.FindGameObjectWithTag ("BackgroundMusic");
		bgmusic.GetComponent<AudioSource> ().Play ();
	}
}
