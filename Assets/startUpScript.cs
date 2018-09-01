using UnityEngine;
using System.Collections;

public class startUpScript : MonoBehaviour {

	public float screenWidth;
	public float screenHeight;
	
	public float panelHeight = 0.3f;

	public bool invertOritentation = false;

	// Use this for initialization
	void Start () {

		// Fix screen orientation
		//Screen.orientation = ScreenOrientation.Portrait;

		// Orientation width/height fix
		if (Screen.height < Screen.width) {
			screenWidth = Screen.height;
			screenHeight = Screen.width;
			invertOritentation = true;;
		} else {
			screenWidth = Screen.width;
			screenHeight = Screen.height;
		}	

		// Set camera dimensions to match the screen size
		GetComponent<Camera> ().orthographicSize = screenHeight/2;
		GetComponent<Camera> ().aspect = screenWidth / screenHeight;


		// Get UI Panel rectangle
		var panelRect = GameObject.Find ("Canvas/Panel").GetComponent<RectTransform>();

		// Set Panel size
		panelRect.sizeDelta = new Vector2 (screenWidth, screenHeight * panelHeight);

	}
	
	// Update is called once per frame
	void Update () {

	}
}
