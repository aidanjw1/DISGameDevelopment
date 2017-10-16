using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameObject gameOverScreen;

	// Use this for initialization
	void Start () {
		GameObject.FindObjectOfType<Draw> ().enabled = false;
	}

	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}

	public void MainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}
}
