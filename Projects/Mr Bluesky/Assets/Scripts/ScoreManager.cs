/// <summary>
/// Keeps track of number of coins remaining on a level and overall score
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {
	[Tooltip("keeps track of player score")]
	public static int score;
	[Tooltip("text that displays score")]
	Text text;
	[Tooltip("coins remaining text")]
	Text coins;
	[Tooltip("number of coins remaining")]
	public static int numberOfCoins;
	[Tooltip("keeps track of coins needed before player can finish level")]
	public int CoinsNeeded;
	[Tooltip("coins left game object")]
	public GameObject CoinsLeft;
	[Tooltip("coin icon")]
	public GameObject CoinsIcon; 

	//intial
	void Start() {
		//manages UI
		text = GameObject.FindGameObjectWithTag("score").GetComponent<Text> (); 
		if (CoinsNeeded == 0) {
			CoinsIcon.SetActive (false);
			CoinsLeft.SetActive (false);
		}
		coins = CoinsLeft.GetComponent<Text> (); 

		numberOfCoins = CoinsNeeded;
		score = 0;
	}
	//updates every frame
	void Update() {
		if (score < 0) {
			score = 0;
		}

		text.text = "" + score;
		if (coins != null)
			coins.text = "" + numberOfCoins;
	
	}

	/// <summary>
	/// Adds to points.
	/// </summary>
	/// <param name="pointsToAdd">Points to add.</param>
	public static void AddPoints (int pointsToAdd) {
		score += pointsToAdd;
	}

	/// <summary>
	/// subtracts one from number of coins remaining
	/// </summary>
	public static void Subtract() {
		if (numberOfCoins > 0) {
			numberOfCoins--;
		}
	}

	/// <summary>
	/// Coins blink if player tries to pass level without collecting all coins
	/// </summary>
	public void BlinkCoins() {
		StartCoroutine (Blink ());
	}

	/// <summary>
	/// Blink coroutine
	/// </summary>
	private IEnumerator Blink() {
		for (int i = 0; i < 5; i++) {
			CoinsIcon.SetActive (false);
			yield return new WaitForSeconds (.1f);

			CoinsIcon.SetActive (true);
			yield return new WaitForSeconds (.1f);
		}
	}
}
