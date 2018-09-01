using UnityEngine;
using System.Collections;

public class ImageTransform : MonoBehaviour {

	float zoomSpeed = 4;
	float rotateAngle = 0.0f;
	float dragSpeed = 4;
	float rotateSpeed = 2.0f;
	float minZoom = 0.3f;
	float minAngle = 0.05f;

	private bool done = false;

	void Start () {
	
	}

	public void doneTransform (){
		done = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (! GetComponent<buttonLoadImage> ().imageLoaded () || done)
			return;
        
            // Two finger touch
            if (Input.touchCount == 2) {

                // Get current touches
                var touch0 = Input.GetTouch(0);
                var touch1 = Input.GetTouch(1);

                // Get previous touches
                Vector2 lastTouch0 = touch0.position  - touch0.deltaPosition;
                Vector2 lastTouch1 = touch1.position  - touch1.deltaPosition;

                // Distance between current touches
                float touchDelta = (touch1.position - touch0.position).magnitude;

                // Distance between previous touches
                float lastTouchDelta = (lastTouch0 - lastTouch1).magnitude;

                // Difference bewteen current and last finger distance
                float delta = touchDelta - lastTouchDelta;
            
        

        var transform = GetComponent<Transform>();

			var aspect = transform.localScale.x/transform.localScale.y;

			//  Scale the quad
			if ((transform.localScale.x + delta*zoomSpeed) > minZoom)
				transform.localScale = new Vector3(  transform.localScale.x + delta*zoomSpeed, 
			    	                                (transform.localScale.x + delta*zoomSpeed)/aspect,
			        	                           	 transform.localScale.z);

			// Difference vectors for each touch
			Vector2 a = lastTouch1 - lastTouch0;
			Vector2 b = touch1.position - touch0.position;

			// Cross vector between them
			var cross = Vector3.Cross( new Vector3(a.x, a.y, 0), new Vector3(b.x, b.y, 0));

			// Direction of cross vector
			float dir = Vector3.Dot( cross.normalized, Vector3.forward);

			// The angle between the vectors
			float angle = Mathf.Asin( cross.magnitude/(a.magnitude*b.magnitude));

			// Rotate around Z axis
			if (angle > minAngle)
				transform.RotateAround(transform.position, new Vector3(0, 0, 1), rotateSpeed*Mathf.Rad2Deg*angle*dir);
				

		}

		// For 1 touch
		if (Input.touchCount == 1) {

			// Get touch
			var touch = Input.GetTouch(0);

			// Get delta
			Vector2 delta = dragSpeed*touch.deltaPosition;

			var transform = GetComponent<Transform>();

			// Translate by delta
			transform.position += new Vector3(delta.x, delta.y, 0);

		}

        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            var transform = GetComponent<Transform>();
            // Translate by delta
            transform.position += new Vector3(0, 40f, 0);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            var transform = GetComponent<Transform>();
            // Translate by delta
            transform.position += new Vector3(0, -40f, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            var transform = GetComponent<Transform>();
            // Translate by delta
            transform.position += new Vector3(-40f, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            var transform = GetComponent<Transform>();
            // Translate by delta
            transform.position += new Vector3(40f, 0, 0);
        }
    }


	public void StretchSide (float delta)
	{

		var transform = GetComponent<Transform> ();

		transform.localScale += new Vector3 (delta, 0, 0);

	}

	public void StretchUp (float delta)
	{
		var transform = GetComponent<Transform> ();
		
		transform.localScale += new Vector3 (0, delta, 0);
	}

}
