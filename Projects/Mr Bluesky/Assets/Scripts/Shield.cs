/// <summary>
/// Manages shield mechanics
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
	[Tooltip("Initializes shield prefab")]
	public GameObject rectPrefab;
	[Tooltip("location of shield start")]
	Vector3 shieldStart;
	[Tooltip("location of shield end")]
	Vector3 shieldStop;
	[Tooltip("GameObject at the position where the laser starts")]
	public GameObject laserProjection;
	[Tooltip("LineRenderer of the laser graphics")]
	public LineRenderer lineRenderer;
	[Tooltip("bool that checks to see if out of range text can be displayed")]
	bool shieldText = false;



	// Use this for initialization 
	void Start () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.widthMultiplier = 0.2f;
	}

	// Update is called once per frame
	void Update () {
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		lineRenderer.enabled = false;

		//sets shield start position where mouse is right clicked
		if (Input.GetMouseButtonDown (1) && ShieldInRange (mouseWorldPos)) {
			shieldStart = mouseWorldPos;
			shieldText = false;
		} else if (Input.GetMouseButtonDown (1) && ShieldInRange (mouseWorldPos) == false) {
			shieldText = true;
			Invoke("ToggleLabel", 2);
		}

		//draws shield render while right click is held down
		if (Input.GetMouseButton (1) && ShieldInRange (shieldStart)) {
			lineRenderer.enabled = true;

		} else {
			//intitializes shield once right click is released
			if (Input.GetMouseButtonUp(1) && ShieldInRange(shieldStart)) {
					DrawPlatform (shieldStart, shieldStop);	
			}
			shieldStart = mouseWorldPos;

		}

		//updates shield projection
		UpdateProjection (shieldStart, mouseWorldPos);

	}

	void ToggleLabel() {
		shieldText = !shieldText;
	}

	/// <summary>
	/// Checks to see if shield start position is within range of player
	/// </summary>
	/// <returns><c>true</c>, if in range was shielded, <c>false</c> otherwise.</returns>
	/// <param name="shieldStart">Shield start.</param>
	bool ShieldInRange(Vector2 shieldStart) {
		Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().transform.position;
		Vector2 direction = shieldStart - playerPosition;
		float distance = direction.magnitude;

		if (distance < 13) {
			return true;
		} else {
			return false;
		}
	}

	/// <summary>
	/// Draws the shield.
	/// </summary>
	/// <param name="start">Start.</param>
	/// <param name="stop">Stop.</param>
	void DrawPlatform(Vector2 start, Vector2 stop) {
		GameObject shield = GameObject.FindGameObjectWithTag("shield");
		if (shield != null) {
			Destroy (shield);
		} 

		//find center of shield object
		float mouseY = start.y - stop.y;
		float mouseX = start.x - stop.x;
		Vector2 midVector = (start + stop) / 2;

		//find angle of vector
		float angle = Mathf.Atan2 (mouseY, mouseX);
		float angle2 = angle * Mathf.Rad2Deg;

		//set position and roation of shield
		rectPrefab.transform.rotation = Quaternion.AngleAxis(angle2, Vector3.forward);
		rectPrefab.transform.position = new Vector3 (midVector.x, midVector.y, 0);

		Instantiate (rectPrefab);
	}


	/// <summary>
	/// Updates the shield projection.
	/// </summary>
	/// <param name="shieldStart">Shield start.</param>
	/// <param name="mouseWorldPos">Mouse world position.</param>
	void UpdateProjection(Vector3 shieldStart, Vector3 mouseWorldPos){
		
		mouseWorldPos.z = 0;
		shieldStart.z = 0;
	
		Vector2 direction = mouseWorldPos - shieldStart;
		float distance = direction.magnitude;
		if (distance < 7) {
			lineRenderer.SetPosition (0, shieldStart);
			lineRenderer.SetPosition (1, mouseWorldPos);
		} 
		shieldStop = lineRenderer.GetPosition (1);
	}

	/// <summary>
	/// Destroyes the shield.
	/// </summary>
	void DeleteShield() {
		Destroy (gameObject);
	}


	/// <summary>
	/// Raises the GU event.
	/// </summary>
	void OnGUI() {
		if (shieldText) {
			GUIStyle guiStyle = new GUIStyle();
			guiStyle.fontSize = 35;
			guiStyle.normal.textColor = Color.red;
			GUI.Label(new Rect(200, 10, 100, 20), "Shield out of range!", guiStyle);
		}
	}
}

