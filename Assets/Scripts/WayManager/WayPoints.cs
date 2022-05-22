using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    // Hold all point in the crossroads and pathes between them to create graph and routes
    [Header("Containers")]
    [SerializeField] private WayPointsContainer[] containers;
    [SerializeField] private Transform additionalWayPointsContainer;

    [Header("Points Corrections")]
    [SerializeField] private Vector3[] corrections;

    [Header("Points -- FOR INFO __")]
    [SerializeField] private List<Vector2> points;
    public List<Vector2> Points { get => points; }

    [Header("Pathes")]
    [SerializeField] private Path[] paths;
    public Path[] Paths { get => paths; }

    private void Awake()
    {
        points = new List<Vector2>();
        ReadPoints();
        //CalculatePaths();
    }

    void ReadPoints()
    {
        points.Clear();

        foreach(WayPointsContainer container in containers)
        {
            points.AddRange(container.ReadPoints());
        }

        //foreach (Transform point in wayPointsContainer.GetComponentsInChildren<Transform>())
        //{
        //    if (point.position.y == 0 && point.position.x == 0) continue;

        //    points.Add(point.position);

        //    // add symmetric point
        //    if (isSymmetricAgainstOx && point.position.y != 0) points.Add(new Vector2(point.position.x, -point.position.y));
        //    if (isSymmetricAgainstOy && point.position.x != 0) points.Add(new Vector2(- point.position.x, point.position.y));

        //    if (isSymmetricAgainstOx && isSymmetricAgainstOy && point.position.y != 0 && point.position.x != 0) points.Add(new Vector2(-point.position.x, -point.position.y));
        //}

        if (additionalWayPointsContainer != null)
        {
            foreach (Transform point in additionalWayPointsContainer.GetComponentsInChildren<Transform>())
            {
                if (point.position.y == 0 && point.position.x == 0) continue;
                points.Add(point.position);
            }
        }

        // make corrections
        foreach(Vector3 pointToCorrect in corrections)
        {
            int pointNumberToCorrect = (int)pointToCorrect.z;
            if (pointNumberToCorrect< points.Count)
            {
                points[pointNumberToCorrect] = new Vector2(pointToCorrect.x, pointToCorrect.y); //new Vector2(points[pointNumberToCorrect].x + pointToCorrect.x, points[pointNumberToCorrect].y + pointToCorrect.y);

            }
        }

        Logging.Log("WayPoints: end to read points, total number is " + points.Count);
    }

    public void MakeCorrectionAccordingToLevel(Vector3[] levelCorrections)
    {
        foreach (Vector3 pointToCorrect in levelCorrections)
        {
            MakePointCorrections((int)pointToCorrect.z, new Vector2(pointToCorrect.x, pointToCorrect.y));
        }
    }

    public void MakePointCorrections(int pointNumber, Vector2 newPosition)
    {
        if (pointNumber < points.Count)
        {
            Logging.Log("WayPoints: make correction for point " + pointNumber + " to position " + newPosition.x + " " + newPosition.y);
            points[pointNumber] = newPosition;
        }
    }

    public void CalculatePaths()
    {
        foreach (Path path in paths)
        {
            path.FillCurvePoints(points[path.StartPointNumber], points[path.EndPointNumber]);
        }

        Logging.Log("WayPoints: end to calculate paths");
    }


    public List<Path> GetPathsForRoute(List<int> route)
    {
        List<Path> pathsForRoute = new List<Path>();
        IDictionary<int, Path> pathesByRoutePoints = new Dictionary<int, Path>();

        
        Logging.Log("WayPoints: start form paires....");

        // form point pairs
        for (int i = 0; i < route.Count - 1; i++)
        {
            pathesByRoutePoints.Add(route[i] * 100 + route[i + 1], null);
        }

        //Logging.Log("WayPoints: end form paires, total number is " + pathesByRoutePoints.Count + " route count is " + route.Count);

        // search for paths
        foreach (Path path in paths)
        {
            int pathPair = path.StartPointNumber * 100 + path.EndPointNumber;

            if (pathesByRoutePoints.ContainsKey(pathPair))
            {
                if (pathesByRoutePoints[pathPair] != null) Logging.Log("WayPoints: HAVE ONE MORE PAIR! ");

                pathesByRoutePoints[pathPair] = path;
            }

        }

        foreach (int pair in pathesByRoutePoints.Keys)
        {
            if (pathesByRoutePoints[pair] != null) pathsForRoute.Add(pathesByRoutePoints[pair]);
            else Logging.Log("WayPoints: HAVE NO PATH FOR ROUTE PART! ");
        }

        Logging.Log("WayPoints: end form paires....");
        return pathsForRoute;
    }
}
