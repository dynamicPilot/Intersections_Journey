using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class GraphSearch
{
    private Dictionary<Vertex, Vertex> cameFrom = new Dictionary<Vertex, Vertex>();
    private Dictionary<Vertex, float> costSoFar = new Dictionary<Vertex, float>();
    private int count = 100;
    static float Heuristic (Vertex a, Vertex b)
    {
        return Mathf.Abs(a.coordinates.x - b.coordinates.x) + Mathf.Abs(a.coordinates.y - b.coordinates.y);
    }

    void AStarSearch(Graph graph, Vertex startPoint, Vertex endPoint)
    {
        // A* graph search algorithm
        count = 100;
        var pointsToBeVisited = new SimplePriorityQueue<Vertex>();
        pointsToBeVisited.Enqueue(startPoint, 0f);

        cameFrom[startPoint] = startPoint;
        costSoFar[startPoint] = 0;

        //Logging.Log("AStarSearch: FIND ROUTE FROM: " + startPoint.number + " TO " + endPoint.number + " count " + count + " " + pointsToBeVisited.Count);
        while (pointsToBeVisited.Count > 0 && count > 0)
        {
            count--;
            Vertex currentPoint = pointsToBeVisited.Dequeue();

            //Logging.Log("AStarSearch: currentPoint" + currentPoint.number);
            if (currentPoint == endPoint)
            {
                break;
            }


            foreach (Vertex neighbor in currentPoint.Neighbors.Keys)
            {
                float newCost = costSoFar[currentPoint] + currentPoint.Neighbors[neighbor];

                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                {
                    costSoFar[neighbor] = newCost;
                    float priority = newCost + Heuristic(neighbor, endPoint);
                    pointsToBeVisited.Enqueue(neighbor, priority);
                    cameFrom[neighbor] = currentPoint;
                }
            }
        }
    }

    public List<int> SearchForRoute(Graph graph, int startPointNumber, int endPointNumber)
    {
        AStarSearch(graph, graph.graph[startPointNumber], graph.graph[endPointNumber]);

        if (count <= 0)
        {
            Logging.Log("AStarSearch: CAN NOT FIND ROUTE FROM: " + startPointNumber + " TO " + endPointNumber);
            return null;
        }

        // form the route
        List<int> route = new List<int>();

        Vertex currentPoint = graph.graph[endPointNumber];
        route.Add(endPointNumber);

        while(currentPoint.number != startPointNumber)
        {
            if (cameFrom.ContainsKey(currentPoint))
            {
                route.Add(cameFrom[currentPoint].number);
                currentPoint = cameFrom[currentPoint];
            }
            else
            {
                Logging.Log("GraphSearch: no point!");
                route.Clear();
                break;
            }
        }

        route.Reverse();
        return route;
    }
}
