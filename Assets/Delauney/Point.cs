using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point  {
    public readonly uint id;
    public Vector2 position;

    public float x
    {
        get
        {
            return position.x;
        }
        set
        {
            position.x = value;
        }
    }

    public float y
    {
        get
        {
            return position.y;
        }
        set
        {
            position.y = value;
        }
    }

    public Point(uint id)
    {
        this.id = id;
    }

    public Point(uint id, float x, float y)
    {
        this.id = id;
        this.position.x = x;
        this.position.y = y;
    }

    public override string ToString()
    {
        return position.ToString();
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}

public class PointEquality : IEqualityComparer<Point>
{
    public bool Equals(Point a, Point b)
    {
        return a.id == b.id;
    }

    public int GetHashCode(Point obj)
    {
        return obj.GetHashCode();
    }
}