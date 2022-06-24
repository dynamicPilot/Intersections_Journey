using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    // graph vertex
    // contains point coordinates, neighbor vertexes and edges weights
    public readonly int number;
    public readonly Vector2 coordinates;
    private IDictionary<Vertex, float> neighbors;
    public IDictionary<Vertex, float> Neighbors { get => neighbors; }

    public Vertex(int newNumber, Vector2 newCoordinates)
    {
        number = newNumber;
        coordinates = newCoordinates;
        neighbors = new Dictionary<Vertex, float>();
    }

    public void AddNeighbor(Vertex nVertex, float nWeight = 0)
    {
        neighbors.Add(nVertex, nWeight);
    }

}
