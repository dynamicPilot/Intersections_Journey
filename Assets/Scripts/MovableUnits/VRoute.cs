using System.Collections.Generic;
using MovableUnits.MediatorAndComponents;
using UnityEngine;

public interface IGetPathPointAndMove
{
    public abstract void GetPathPointToMove(IMovable movable);
}

public interface IStartAndEndPathPoints
{
    public abstract int StartPathPoints();
    public abstract int EndPathPoints();
}

[System.Serializable]
public class VRoute : IGetPathPointAndMove, IStartAndEndPathPoints
{
    [SerializeField] private List<Path> paths = new List<Path>(); // all paths from start to end point
    [SerializeField] private List<Vector2> points = new List<Vector2>(); // current path part points

    [SerializeField] private int pathPartIndex = -1;
    private float t = 0;

    private bool isLine = true;
    private bool isInTurn = false;
    private bool roadEnds = false;
    public RouterComponent routerComponent;

    public delegate void PathsEnded();
    public event PathsEnded OnPathEnded;
    public VRoute(List<Path> _paths, RouterComponent _routerComponent)
    {
        paths = _paths;
        routerComponent = _routerComponent;
        pathPartIndex = -1;
        roadEnds = false;
        NewPathsPart();
    }

    public int StartPathPoints()
    {
        return paths[pathPartIndex].StartPointNumber;
    }

    public int EndPathPoints()
    {
        return paths[pathPartIndex].EndPointNumber;
    }

    public int TargetRoutePoint()
    {
        return paths[^1].EndPointNumber;
    }

    public Path.TURN NextPartTurn()
    {
        if (pathPartIndex + 1 < paths.Count) return paths[pathPartIndex + 1].Turn;
        return Path.TURN.none;
    }

    public void GetPathPointToMove(IMovable movable)
    {
        movable.SetNewPosition(GetPathPoint(movable.GetDistanceToMove(), movable.GetPosition()));
    }

    Vector3 GetPathPoint(float distance, Vector3 position)
    {
        if (distance == 0 || roadEnds) return position;
        t += distance / paths[pathPartIndex].CurveLength;

        // move to endPoint
        if (Mathf.Abs(1f - t) < 0.001f) t = 1f;
        else if (t > 1)
        {
            // move to
            distance -= Vector3.Distance(points[points.Count - 1], position);
            NewPathsPart();           
            return GetPathPoint(distance, position);
        }

        Vector3 newPoint;

        // get new point
        if (isLine)
        {
            newPoint = Vector2.Lerp(points[0], points[1], t);
        }
        else
        {
            newPoint = Curves.GetBezierPoint(t, points);
        }


        if (t >= 1) NewPathsPart();

        return newPoint;
    }

    void NewPathsPart()
    {
        pathPartIndex++;

        if (pathPartIndex >= paths.Count)
        {
            roadEnds = true;
            if (OnPathEnded != null) OnPathEnded.Invoke();
            return;
        }

        // next path
        points = paths[pathPartIndex].CurvePoints;
        isLine = (points.Count == 2);

        ChangeIsInTurn();

        t = 0f;
    }

    void ChangeIsInTurn()
    {
        bool prevInTurn = isInTurn;
        isInTurn = (paths[pathPartIndex].Turn != Path.TURN.none);

        if (isInTurn && !prevInTurn)
        {
            // notify mediator
            routerComponent.DoInTurn(paths[pathPartIndex].Turn);
        }
        else if (prevInTurn && !isInTurn)
        {
            routerComponent.DoEndTurn();
        }
    }
}
