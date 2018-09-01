using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EllipseContour : FaceElement {
    public LineRenderer lineRenderer;
    public int segments;
    public ControlPoint center, leftPoint, rightPoint, topPoint, bottomPoint;

    private List<ControlPoint> points;

    void Start()
    {
        leftPoint.Lock();
        rightPoint.LockOnY();
        topPoint.Lock();
        bottomPoint.LockOnX();

        points = new List<ControlPoint> { center, leftPoint, rightPoint, topPoint, bottomPoint };

        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
    }

    // Update is called once per frame
    void Update()
    {
        // stretch the axis according to the contorl points
        leftPoint.transform.position = (transform.position - rightPoint.transform.position) + transform.position;
        topPoint.transform.position = (transform.position - bottomPoint.transform.position) + transform.position;

        // recenter the transform at the middle of the control points
        var centerMovement = center.transform.position - transform.position;
        transform.position += centerMovement;
        center.transform.position -= centerMovement;


        float xradius = (rightPoint.transform.position - transform.position).magnitude;
        float yradius = (bottomPoint.transform.position - transform.position).magnitude;

        float x;
        float y;
        float z = 0f;
        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }

    public override List<Point> LockAndReturnPoints()
    {
        foreach (var p in points) p.Lock();
        return points.Select(p => p.Point).ToList();
    }

    public void Rotate(float rotation)
    {
        transform.Rotate(Vector3.forward, rotation);
    }
}
