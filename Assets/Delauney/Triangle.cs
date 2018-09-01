using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Delauney {

	public class Triangle {

		public HashSet<Point> points;
		public HashSet<Edge> edges;
        private float radius;
		private Vector2 center;

		public Triangle(Point p1, Point p2, Point p3)
		{
            points = new HashSet<Point>(new PointEquality()) { p1, p2, p3 };
            edges = new HashSet<Edge>(new EdgeEquality()) { new Edge(p1, p2), new Edge(p2, p3), new Edge(p3, p1) };

            ComputeCircle();
		}

        /// <summary>
        /// Calculate the triangle's circle
        /// </summary>
        public void ComputeCircle()
		{
            Vector2 a = points.ElementAt(0).position;
			Vector2 b = points.ElementAt(1).position;
            Vector2 c = points.ElementAt(2).position;

            float D = 2*(a.x*(b.y - c.y) + b.x*(c.y - a.y) + c.x*(a.y - b.y));

			center.x = (
					(a.x*a.x + a.y*a.y)*(b.y - c.y) +
					(b.x*b.x + b.y*b.y)*(c.y - a.y) +
					(c.x*c.x + c.y*c.y)*(a.y - b.y))/D;

			center.y = (
				(a.x*a.x + a.y*a.y)*(c.x - b.x) +
				(b.x*b.x + b.y*b.y)*(a.x - c.x) +
				(c.x*c.x + c.y*c.y)*(b.x - a.x))/D;

			radius = (center - a).magnitude;
		}

		public bool HasPointInCircle(Point p)
		{
            return (p.position - center).magnitude <= radius;
		}

		public Vector2 GetBarycentricCoordinates(Vector2 pos)
		{
            Vector2 v0 = points.ElementAt(0).position - points.ElementAt(2).position;
			Vector2 v1 = points.ElementAt(1).position - points.ElementAt(2).position;
			Vector2 v2 = pos - points.ElementAt(2).position;

			float d00 = Vector2.Dot (v0, v0);
			float d10 = Vector2.Dot (v0, v1);
			float d01 = d10;
			float d11 = Vector2.Dot (v1, v1);

			float d02 = Vector2.Dot (v2, v0);
			float d12 = Vector2.Dot (v2, v1);

			float det = d00 * d11 - d10 * d01;

			float alpha = (d02 * d11 - d01 * d12)/det;
			float beta = (d00 * d12 - d02 * d10) / det;

			return new Vector2 (alpha, beta); 
		}

		public Vector2 getCartesianCoords(Vector2 bar)
		{
			Vector2 a = points.ElementAt(0).position;
			Vector2 b = points.ElementAt(1).position;
			Vector2 c = points.ElementAt(2).position;

			return bar[0]*a + bar[1]*b + (1 - bar[0] - bar[1])*c;
		}

        public static Triangle MakeTriangle(Edge e, Point p)
        {
			return new Triangle (e.p1, e.p2, p);
		}                     

		public bool IsPositionInside(Vector2 p)
		{
			var b = GetBarycentricCoordinates(p);

			if (((b [0] + b [1]) > 1) || (b [0] < 0) || (b [1] < 0))
				return false;
			else 
				return true;
		}

        public bool HasPoint(Point p)
        {
            return points.Contains(p);
        }

        public bool HasAnyPoint(List<Point> points)
        {
            return points.Any(p => HasPoint(p));
        }

        public void Print()
		{
			string s = "Triangle = { center: " + center.ToString() + ", radius: " + radius + ", points: ";

			foreach (var p in this.points) {
				s += p.ToString();
			}

			s += "}";

			Debug.Log (s);
		}

		public List<Point> GetPoints()
		{
            return points.ToList();
		}
	}

	public class TriangleEquality : IEqualityComparer<Triangle>
	{
		public bool Equals(Triangle t1, Triangle t2)
		{
            return t1.Equals(t2);
		}

		public int GetHashCode(Triangle t)
		{
            var orderedPoints = t.points.OrderBy(p => p.id);

            var res = new { h1 = orderedPoints.ElementAt(0).GetHashCode(), h2 = orderedPoints.ElementAt(1).GetHashCode(), h3 = orderedPoints.ElementAt(2).GetHashCode() };

            return res.GetHashCode();
		}
	}
}





