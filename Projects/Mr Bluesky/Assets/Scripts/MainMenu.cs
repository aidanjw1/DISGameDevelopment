using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string startLevelName;
	// Use this for initialization
	void Start () {
		GameManager.gameState = GameManager.GameState.MainMenu;
	}
		
	public void StartGame() {
		GameManager.gameState = GameManager.GameState.Running;
		Time.timeScale = 1;
		SceneManager.LoadScene ("TutorialLevel");
	}

	public void Quit() {
		GameManager.Quit ();
	}

	public void Credits() {
		SceneManager.LoadScene ("Credits");
	}
}
