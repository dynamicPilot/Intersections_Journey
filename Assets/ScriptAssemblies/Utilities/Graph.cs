using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    // weighted, directed graph
    public readonly IDictionary<int, Vertex> graph;

    public Graph()
    {
        graph = new Dictionary<int, Vertex>();
    }

    public void AddPoint(int newNumber, Vector2 newCoordinates)
    {
        graph.Add(newNumber, new Vertex(newNumber, newCoordinates));
    }

    public void AddVertex(Vertex newVertex)
    {
        if (!graph.Keys.Contains(newVertex.number)) graph.Add(newVertex.number, newVertex);
    }

    public void AddEdge(Vertex startVertex, Vertex endVertex, float newWeight)
    {
        startVertex.AddNeighbor(endVertex, newWeight);
    }

    public void AddEdgeByPointNumbers(int startVertexNumber, int endVertexNumber, float newWeight)
    {
        graph[startVertexNumber].AddNeighbor(graph[endVertexNumber], newWeight);
    }
}
