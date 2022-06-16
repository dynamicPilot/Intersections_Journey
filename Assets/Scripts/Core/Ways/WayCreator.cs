using IJ.Core.Ways;
using IJ.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class WayCreator : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private WayPoints wayPoints;
    [SerializeField] private RoadsManager roadsManager;
    
    Graph graph;

    private void BuildGraph()
    {        
        graph = new Graph();

        // set vertexes
        for (int i = 0; i< wayPoints.Points.Count; i++)
        {
            graph.AddPoint(i, wayPoints.Points[i]);
        }

        // add edges
        foreach(Path path in wayPoints.Paths)
        {
            graph.AddEdgeByPointNumbers(path.StartPointNumber, path.EndPointNumber, path.Weight);
        }
    }

    public List<int> SearchForRoute(int startPoint, int endPoint)
    {
        if (graph == null) BuildGraph();

        if (startPoint < 0 || endPoint < 0) return null;

        //Logging.Log("WayCreator: start search for road from " + startPoint + " to " + endPoint);
        GraphSearch graphSearch = new GraphSearch();
        return graphSearch.SearchForRoute(graph, startPoint, endPoint);
    }

    public List<Path> CreatePathForVehicleWithMustHavePoints (int startPointNumber, List<int> mustHavePoints)
    {
        // Check start point
        if (!roadsManager.CheckStartPoint(startPointNumber))
        {
            Logging.Log("WayCreator: start point is busy");
            return null;
        }

        // Check last point for end point
        int endPointNumber = -1;

        if (roadsManager.CheckForEndPoint(mustHavePoints[mustHavePoints.Count - 1], startPointNumber))
        {
            endPointNumber = mustHavePoints[mustHavePoints.Count - 1];
            return CreatePaths(startPointNumber, endPointNumber);
        }

        if (mustHavePoints.Count < 2)
        {
            Logging.Log("WayCreator: something wrong with must have points for start point " + startPointNumber);
            return null;
        }
        // create suitable path by parts

        List<int> route = new List<int>();

        int currentStartPointNumber = startPointNumber;
        int currentEndPointNumber;

        for (int i = 0; i < mustHavePoints.Count; i += 2)
        {
            currentEndPointNumber = mustHavePoints[i];

            if (currentStartPointNumber == currentEndPointNumber)
            {
                currentEndPointNumber = mustHavePoints[i + 1];
            }

            List<int> currentRoute = GetRoute(currentStartPointNumber, currentEndPointNumber);

            if (currentRoute == null)
            {
                return null;
            }
            else
            {
                // have correct route part
                route.AddRange(currentRoute);

                if (!route.Contains(mustHavePoints[i + 1]))
                    route.Add(mustHavePoints[i + 1]);

                currentStartPointNumber = mustHavePoints[i + 1];
            }
        }

        // create route from the last must have point to the end point
        // get all end points
        foreach(int point in roadsManager.GetAllEndPointsForStartPoint(startPointNumber))
        {
            List<int> currentRoute = GetRoute(currentStartPointNumber, point);

            if (currentRoute != null)
            {
                if (currentRoute.Count > 0)
                {
                    route.AddRange(currentRoute);
                    endPointNumber = point;
                    break;
                }
            }
        }

        if (endPointNumber < 0) return null;

        return GetPath(route);
    }

    public List<Path> CreatePathsForVehicle(int startPointNumber)
    {
        // Check start point
        if (!roadsManager.CheckStartPoint(startPointNumber))
        {
            Logging.Log("WayCreator: start point is busy");
            return null;
        }

        // Get route
        int endPointNumber = roadsManager.GetEndPoint(startPointNumber);

        return CreatePaths(startPointNumber, endPointNumber);
    }

    List<Path> CreatePaths(int startPointNumber, int endPointNumber)
    {
        return GetPath(GetRoute(startPointNumber, endPointNumber));
    }

    List<Path> GetPath(List<int> route)
    {
        if (route == null)
        {
            return null;
        }
        else if (route.Count == 0)
        {
            return null;
        }

        // Get paths
        List<Path> paths = wayPoints.GetPathsForRoute(route);

        if (paths == null)
        {
            Logging.Log("WayCreator: null paths");
            return null;
        }
        else if (paths.Count < 1)
        {
            Logging.Log("WayCreator: empty paths");
            return null;
        }

        return paths;
    }

    List<int> GetRoute(int startPointNumber, int endPointNumber)
    {
        if (startPointNumber < 0 || endPointNumber < 0)
        {
            Logging.Log("WayCreator: start or end points are null");
            return null;
        }

        List<int> route = SearchForRoute(startPointNumber, endPointNumber);

        if (route == null)
        {
            Logging.Log("WayCreator: null route");
            return null;
        }
        else if (route.Count < 2)
        {
            Logging.Log("WayCreator: wrong route from " + startPointNumber + " to " + endPointNumber);
            return null;
        }

        return route;

    }

}
