using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Delauney {
	public class Triangulation {

		public HashSet<Triangle> triangles;
		public List<Point> points;
		public float width;
		public float height;
		public Triangle superTriangle;

		public Triangulation(List<Point> input, float w, float h)
		{
			points = input;

			triangles = new HashSet<Triangle>(new TriangleEquality());

			this.width = w;
			this.height = h;

			// Add super triangle 
			CreateSuperTriangle();

			// Process each vertex
			foreach (Point p in points) {

				// Create edge buffer
				Dictionary<Edge, int> edge_buffer = new Dictionary<Edge, int>(new EdgeEquality());

				// Check each triangle to see if point is in circle
				foreach (Triangle triangle in triangles)
				{
					// If so, store the edges in buffer, and remove the triangles from the list
					if (triangle.HasPointInCircle(p))
					{
						// Store edge as keys, with number of occurences as values
						foreach (Edge triangle_edge in triangle.edges)
						{
							if (edge_buffer.ContainsKey(triangle_edge))
								edge_buffer[triangle_edge] += 1;
							else
								edge_buffer[triangle_edge] = 1;
						}

						// Remove the triangle from list
						//triangles.Remove(triangle);

					}
				}

				// Remove triangles from list
				triangles.RemoveWhere( t => t.HasPointInCircle(p));

				var edge_remove  = new List<Edge>();

				// Remove doubled edges from buffer
				foreach (KeyValuePair<Edge, int> e in edge_buffer)
				{
					if (e.Value > 1)
						//edge_buffer.Remove(e.Key);
						edge_remove.Add(e.Key);
				}

				foreach (Edge e in edge_remove)
					edge_buffer.Remove(e);



				// For each remaining edge, add a new triangle that connects to the vertex
				foreach (Edge e in edge_buffer.Keys)
				{
					triangles.Add(Triangle.MakeTriangle(e, p));
				}

				//Debug.Log ("Vertex " + counter);
				//printTriangles();
				//counter++;
			}

			// Remove any triangle with supre triangle vertices
			triangles.RemoveWhere( t=> t.HasAnyPoint( superTriangle.GetPoints()) );

			PrintTriangles();
		}

		public void CreateSuperTriangle()
		{
			float factor = 3;
			Triangle superTri = new Triangle (
				new Point(int.MaxValue, -factor*width,0),
				new Point(int.MaxValue-1, 0, factor*width),
				new Point(int.MaxValue-2, factor * width, 0));

			triangles.Add (superTri);
			superTriangle = superTri;
		}

		public void PrintTriangles()
		{
			Debug.Log ("Triangles: ");

			foreach (Triangle t in triangles)
				t.Print ();
		}
	}
}
