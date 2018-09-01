using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FaceContour : FaceElement {
    public LineRenderer lineRenderer;
    public List<ControlPoint> points = new List<ControlPoint>();

    // Update is called once per frame
    void Update() {
        lineRenderer.positionCount = points.Count + 1;
        var cps = points.Select(p => p.gameObject.transform.position).ToList();
        cps.Add(cps.First());
        lineRenderer.SetPositions(cps.ToArray());
    }

    public override List<Point> LockAndReturnPoints()
    {
        foreach (var p in points) p.Lock();
        return points.Select(p => p.Point).ToList();
    }
}
