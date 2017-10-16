using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

	// Use this for initialization
	public void Quit()
	{
		SceneManager.LoadScene ("MainMenu");
	}
	public void PlayAgain()
	{
		SceneManager.LoadScene ("LevelOne");
	}
}
