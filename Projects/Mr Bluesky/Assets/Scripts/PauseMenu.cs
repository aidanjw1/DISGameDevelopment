/// <summary>
/// Manages pause menu
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	[Tooltip("pause menu")]
	public GameObject pauseMenuCanvas;
	[Tooltip("heart canvas")]
	public GameObject heartCanvas;
	[Tooltip("Draw manager object")]
	public GameObject drawmanager;

	// Use this for initialization
	void Start () {
		GameManager.gameState = GameManager.GameState.Running;
		pauseMenuCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameManager.gameState == GameManager.GameState.Running) {
				Pause ();
			}
			else if (GameManager.gameState == GameManager.GameState.Paused) {
				Resume ();
			}
		}
	}

	/// <summary>
	/// Triggers pause menu
	/// </summary>
	public void Pause() {
		pauseMenuCanvas.SetActive (true);
		drawmanager.SetActive (false);
		Time.timeScale = 0;

	}
		
	/// <summary>
	/// Resumes game.
	/// </summary>
	public void Resume() {
		pauseMenuCanvas.SetActive (false);
		drawmanager.SetActive (true);
		Time.timeScale = 1;
	}

	/// <summary>
	/// Returns to main menu.
	/// </summary>
	public void ReturnToMainMenu() {
		GameManager.LoadScene ("MainMenu");
		heartCanvas.SetActive (false);
	}
}
