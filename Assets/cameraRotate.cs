using UnityEngine;
using System.Collections;

public class cameraRotate : MonoBehaviour {


	public Vector3 point = new Vector3(-12.65f, 1.31f, -11.75f);
	public float velocity = 10f;
	float angle = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		angle += velocity * Time.deltaTime;

		GetComponent<Transform> ().RotateAround (point, new Vector3 (0, 1, 0), 1);
	}
}
