using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    public List<Vector2> points = new List<Vector2> ();
    public int pointsCount = 0;

    float pointsMinDistance = 0.1f;
    float circleColliderRadius;

    public void AddPoint ( Vector2 newPoint ) {
        if (pointsCount >= 1 && Vector2.Distance (newPoint, GetLastPoint ()) < pointsMinDistance)
            return;

        points.Add ( newPoint );
        pointsCount++;

        //Add circle collider to the Point
        CircleCollider2D circleCollider = this.gameObject.AddComponent <CircleCollider2D> ( );
        circleCollider.offset = newPoint;
        circleCollider.radius = circleColliderRadius;
        
        //Line Renderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition ( pointsCount-1, newPoint );

        //Edge Collider
        if ( pointsCount>1 )
            edgeCollider.points = points.ToArray ( );
    }

    public Vector2 GetLastPoint ( ) {
        return ( Vector2 )lineRenderer.GetPosition ( pointsCount - 1 );
    }

    public void SetLineColor ( Gradient colorGradient ) {
        lineRenderer.colorGradient = colorGradient;
    }

    public void SetPointsMinDistance ( float distance ) {
        pointsMinDistance = distance;
    }

    public void SetLineWidth ( float width ) {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        circleColliderRadius = width / 2f;
        edgeCollider.edgeRadius = circleColliderRadius;
    }
}
