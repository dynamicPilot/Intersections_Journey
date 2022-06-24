using UnityEngine;

[System.Serializable]
public struct TimePoint
{
    [SerializeField] private int roadStartPointNumber;
    public int RoadStartPointNumber { get => roadStartPointNumber; }

    [SerializeField] private int roadEndPointNumber;
    public int RoadEndPointNumber { get => roadEndPointNumber; }

    [SerializeField] private float hour;
    public float Hour { get => hour; }

    [SerializeField] private bool isDouble;
    public bool IsDouble { get => isDouble; }

    public TimePoint(int newStart, int newEnd, float newHour = 0f, bool newIsDouble = false)
    {
        roadStartPointNumber = newStart;
        roadEndPointNumber = newEnd;
        hour = newHour;
        isDouble = newIsDouble;
    }
}
