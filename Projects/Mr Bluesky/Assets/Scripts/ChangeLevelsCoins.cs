/// <summary>
/// Checks to see that all coins and enemies have been defeated before allowing player to beat level
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeLevelsCoins : MonoBehaviour {
	[Tooltip("Level name")]
	public string levelName;

	/// <summary>
	/// Raises the collision enter2 d event. Blinks UI elements if not all have been collected. Allows player to move on to next level
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter2D (Collision2D other){
		Text coins = GameObject.FindGameObjectWithTag("coins").GetComponent<Text> ();
		Text enemies = GameObject.FindGameObjectWithTag ("enemyCount").GetComponent<Text> ();

		if ((coins.text.Equals ("0")) && enemies.text.Equals ("0")) {
			SceneManager.LoadScene (levelName);
		} else if (enemies.text.Equals ("0")) {
			GameObject.FindObjectOfType<ScoreManager> ().BlinkCoins ();
		} else if (coins.text.Equals ("0")) {
			GameObject.FindObjectOfType<EnemyCount> ().BlinkEnemies ();
		} else {
			GameObject.FindObjectOfType<EnemyCount> ().BlinkEnemies ();
			GameObject.FindObjectOfType<ScoreManager> ().BlinkCoins ();
		}
	}
}
