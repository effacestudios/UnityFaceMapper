using UnityEngine;
using System.Collections;

public class quadFullScreen : MonoBehaviour {

	Transform quadTransform;
	startUpScript startUp;
    //public imageGetPoints getPoints;  //AVI
    public bool maintainAspectRatio = false;

	// Use this for initialization
	void Start () {

		// Get the quads Transform
		quadTransform = GetComponent<Transform> ();

		startUp = Camera.main.GetComponent<startUpScript> ();

		var cameraHeight = Camera.main.orthographicSize * 2;
		var cameraWidth = 2 * (Camera.main.orthographicSize * Camera.main.aspect);

		var newY = (startUp.panelHeight * cameraHeight)/2;

		// Position the quad at the center
		quadTransform.position = new Vector3 (0, newY, quadTransform.position.z);

		// Scale to fill part of the screen
		quadTransform.localScale = new Vector3 (cameraWidth, cameraHeight*(1-startUp.panelHeight), 1.0f);


		if (maintainAspectRatio) {

			var texWidth = GetComponent<Renderer>().material.mainTexture.width;
			var texHeight = GetComponent<Renderer>().material.mainTexture.height;

			float aspect = ((float) texWidth)  / texHeight;

			var scaleHeight = cameraHeight*(1-startUp.panelHeight);
			var scaleWidth = aspect * scaleHeight;

			quadTransform.localScale = new Vector3 (scaleWidth, scaleHeight, 1.0f);

		}

		Debug.Log ("start" + cameraWidth);
        //getPoints.Start();
      //  getPoints = imageGetPoints.Instantiate.Start;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
