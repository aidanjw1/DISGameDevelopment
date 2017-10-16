/// <summary>
/// Manages the number of enemies
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour {
	
	Text enemyCount;
	[Tooltip("Total number of enemies on a level")]
	public int totalEnemies;
	[Tooltip("current number of enemies")]
	public static int enemies; 
	[Tooltip("turret UI icon")]
	public GameObject TurretIcon;

	// Use this for initialization
	void Start () {
		enemyCount = this.gameObject.GetComponent<Text>();
		enemies = totalEnemies;
	}
	
	// Update is called once per frame
	void Update () {
		enemyCount.text = "" + enemies; 
	}

	/// <summary>
	/// Decreases nunber of enemies in UI
	/// </summary>
	public static void KilledEnemy() {
		if (enemies > 0) {
			enemies--;

		}
	}

	/// <summary>
	/// Enemy turret UI icon blinks if player tries to win level before killing all enemies
	/// </summary>
	public void BlinkEnemies() {
		StartCoroutine (Blink ());
	}


	/// <summary>
	/// Blink coroutine
	/// </summary>
	private IEnumerator Blink() {
		for (int i = 0; i < 5; i++) {
			TurretIcon.SetActive (false);
			yield return new WaitForSeconds (.1f);
			TurretIcon.SetActive (true);
			yield return new WaitForSeconds (.1f);
		}
	}
}
