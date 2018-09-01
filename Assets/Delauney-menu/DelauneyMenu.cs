using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;


using Delauney;

[CustomEditor(typeof(DelauneyApp))]
public class DelauneyMenu : Editor {

	/*public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();

		DelauneyApp delauney = (DelauneyApp)target;
        
		if (GUILayout.Button("Test"))
		{
			var points = new List<Vector2>();

			points.Add(new Vector2(60, 60));
			points.Add(new Vector2(10, 10));
			points.Add(new Vector2(80, 26));
			points.Add(new Vector2(2, 48));
			points.Add (new Vector2(20, 84));

			Triangulate t = new Triangulate(points, 100, 100);

			t.PrintTriangles();
			t.DrawTriangles();

			var tri = new Triangle(new Vector2(0, 0),
			                     new Vector2(2,2),
			                     new Vector2(1,0));

			tri.Print();

			if (tri.IsPointSide(new Vector2(0.5f, 0.5f)))
				Debug.Log("Point inside");
			else
				Debug.Log("Point outside");
		}
	}*/
}


#endif