/// <summary>
/// GUI pop up for check point reached
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPopUp : MonoBehaviour {
	private GUIStyle guiStyle = new GUIStyle();
	public string name; //text to be displayed
	private bool showName = false;

	void OnTriggerEnter2D()
	{
		showName = true;
	}
	void OnTriggerExit2D()
	{
		showName =false;
	}

	/// <summary>
	/// Raises the GU event.
	/// </summary>
	void OnGUI()
	{
		guiStyle.fontSize = 35;
		guiStyle.normal.textColor = Color.green;
		if (showName)
			GUI.Label(new Rect(200, 10, 100, 20), name, guiStyle);
	}
		
}
