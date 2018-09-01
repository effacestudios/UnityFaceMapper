using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class faceMask : MonoBehaviour {
    public List<Vector2> positions = new List<Vector2> { 
        new Vector2(319, 110),
        new Vector2(398, 141),
        new Vector2(483, 203),
        new Vector2(519, 260),
        new Vector2(542, 338),
        new Vector2(548, 566),
        new Vector2(521, 649),
        new Vector2(480, 677),
        new Vector2(413, 693),
        new Vector2(234, 694),
        new Vector2(161, 679),
        new Vector2(117, 647),
        new Vector2(93, 567),
        new Vector2(97, 340),
        new Vector2(118, 266),
        new Vector2(158, 199),
        new Vector2(257, 131),
        };

    public List<Vector2> GetScreenCoordinates()
	{
		var worldPoints = new List<Vector2> ();
		var texture = GetComponent<Renderer> ().material.mainTexture;
		var width = texture.width;
		var height = texture.height;

		foreach (var point in positions) {

			var newPoint = new Vector3();

			newPoint.x = (float)(point.x/width - 0.5);
			newPoint.y = (float)(point.y / height - 0.5);
            newPoint.z = GetComponent<Transform>().position.z;

			Vector2 finalPoint = Camera.main.WorldToScreenPoint(GetComponent<Transform>().localToWorldMatrix.MultiplyPoint3x4(newPoint));

			worldPoints.Add(finalPoint);

			Debug.Log("Original point: " + point + ", local coord: " + newPoint + ", finalPoint: " + finalPoint);

		}

		return worldPoints;
	}
}
