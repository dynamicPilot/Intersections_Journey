using UnityEngine;

[System.Serializable]
public struct StartPointPack
{
    [SerializeField] private int startPointNumber;
    public int StartPointNumber { get { return startPointNumber; } }

    [SerializeField] private Transform roadTrigger;
    public Transform RoadTrigger { get { return roadTrigger; } }
}

[System.Serializable]
public class StartPointPackCorrection
{
    [SerializeField] private int packIndex;
    public int PackIndex { get => packIndex; }

    [Header("New Positions")]
    [SerializeField] private Vector2 newStartPointPosition;
    public Vector2 NewStartPointPosition { get => newStartPointPosition;  }

    [SerializeField] private Vector2 newRoadTriggerPosition;

    public void SetRoadTriggerPosition(Transform roadTrigger)
    {
        roadTrigger.position = newRoadTriggerPosition;
    }
}
