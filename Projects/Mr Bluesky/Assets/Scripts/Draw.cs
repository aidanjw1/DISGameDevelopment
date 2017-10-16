/// <summary> 
/// Responsible for spawning rocks that the player can walk on
/// as platforms, as well as updating the slider which keeps track of the 
/// fraction of rocks that have been spawned out of the limit.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

	[Tooltip("Prefab for the rock that will be spawned")]
	public GameObject RockPrefab;
	[Tooltip("Prefab for the player character")]
	public GameObject player;
	[Tooltip("How many rock prefabs can be instantiated at a time")]
	public int numRocksLimit;
	[Tooltip("Number of seconds before each rock prefab disappears")]
	public float rockDeathTime;
	[Tooltip("Slider to show how fraction of rock limit has been spawned")]
	[SerializeField] RectTransform stamSlider;

	private Vector2 mouseLastPos;
	private bool clicked;
	public int numRocks;

	// Use this for initialization
	void Start () {
		clicked = false;
		numRocks = 0;
		Rock.rockTime = rockDeathTime;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		UpdateStamina (numRocksLimit, numRocks);
		if (Input.GetMouseButton (0)) {

			// Check to see if this is the first update since the mouse was clicked, in 
			// which case there will be no last position for the mouse, so we want to set
			// mouseLastPos to the current position of the mouse
			if (!clicked) {
				mouseLastPos = mousePos;
				clicked = true;
			}

			// Stop spawning rocks if above the rocks limit
			if (numRocks < numRocksLimit) {
				RockPrefab.transform.position = new Vector3 (mousePos.x, mousePos.y, 1);
				Instantiate (RockPrefab);
				numRocks++;
				DrawCirlesBetweenPoints (mouseLastPos, mousePos);
				mouseLastPos = mousePos;
			}
				
		} else {
			clicked = false;
		}
	}
		
	/// <summary>
	/// Recursively draw rocks between two 2D Vector points. Put a rock between the
	/// two points given, then call the method again to draw rocks between the old
	/// points and the new middle.
	/// </summary>
	/// <param name="position_0">Position of the first point</param>
	/// <param name="position_1">Position of the seconds point</param>
	private void DrawCirlesBetweenPoints(Vector2 position_0, Vector2 position_1) {
		Vector2 between = position_1 - position_0;

		// base case to stop recursion
		if (between.magnitude < .5) {
			return;
		}
			
		Vector2 newPos = (between / 2) + position_0;
		RockPrefab.transform.position = newPos;
		Instantiate (RockPrefab);
		numRocks++;
		DrawCirlesBetweenPoints (position_0, newPos);
		DrawCirlesBetweenPoints (newPos, position_1);
	}

	/// <summary>
	/// Updates the stamina slider for rocks used out of limit.
	/// </summary>
	/// <param name="max">Rock spawn limit.</param>
	/// <param name="current">Current rocks used.</param>
	void UpdateStamina (int max, int current) {
		if (stamSlider == null)
			return;
		Vector3 scale = stamSlider.transform.localScale;
		scale.x = 0;
		float relativeScale = (float)current/(float)max;
		scale = stamSlider.transform.localScale;
		scale.x = relativeScale;
		stamSlider.transform.localScale = scale;
	}

	public void loseRock() {
		numRocks--;
	}
}
