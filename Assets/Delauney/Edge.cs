using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Delauney {

	public class Edge {

        public Point p1, p2;

		public Edge(Point p1, Point p2)
		{
            this.p1 = p1;
            this.p2 = p2;
		}


		public bool HasPoint(Point p)
		{
            return p1.id == p.id || p2.id == p.id;
		}
	}

    public class EdgeEquality : IEqualityComparer<Edge>
	{
		public bool Equals(Edge e1, Edge e2)
		{
            return e1.HasPoint(e2.p1) && e1.HasPoint(e2.p2);
		}

        /// <summary>
        /// Return the same hash code for e { a, b } and e { b, a }
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
		public int GetHashCode(Edge e)
		{
            var res = new { id1 = Math.Min(e.p1.id, e.p2.id), id2 = Math.Max(e.p1.id, e.p2.id) };

            return res.GetHashCode();
		}
	}

}