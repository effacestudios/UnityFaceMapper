using UnityEngine;
using System.Collections;

public class headRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, 0.3f*Mathf.Sign(Mathf.Sin (Time.realtimeSinceStartup)));
	}
}
