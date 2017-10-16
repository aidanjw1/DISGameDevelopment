/// <summary>
/// Manages player lives and changes heart UI
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {
	[Tooltip("keeps track of lives left")]
	private int livesLeft;
	[Tooltip("list of heart objects")]
	public GameObject[] Hearts;
	[Tooltip("blinks when heart is lost")]
	public int NumBlinks;
	[Tooltip("blink time")]
	public float BlinkTime;
	[Tooltip("game over screen")]
	public GameObject gameOverScreen;

	// Use this for initialization
	void Start () {
		livesLeft = Hearts.Length;
	}

	/// <summary>
	/// Updates the hearts.
	/// </summary>
	public void updateHearts() {
		livesLeft--;
		StartCoroutine (HeartBlink (Hearts [livesLeft]));
		if (livesLeft <= 0) {
			GameOver();
		}
	}

	/// <summary>
	/// Pulls up game over UI
	/// </summary>
	private void GameOver() {
		gameOverScreen.SetActive (true);
		Time.timeScale = 0;
	}

	/// <summary>
	/// Resets lives after game over
	/// </summary>
	public void ResetLives() {
		livesLeft = Hearts.Length;
	}

	/// <summary>
	/// Hearts blink when they are lost
	/// </summary>
	/// <returns>The blink.</returns>
	/// <param name="heart">Heart.</param>
	private IEnumerator HeartBlink(GameObject heart) {
		for (int i = 0; i < NumBlinks; i++) {
			heart.SetActive (false);
			yield return new WaitForSeconds (BlinkTime);

			heart.SetActive (true);
			yield return new WaitForSeconds (BlinkTime);
		}
		heart.SetActive (false);
	}
}
