using System.Collections.Generic;
using UnityEngine;

public class RoadsManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private RoadMarksControl roadMarks;

    [Header("Settings")]
    [SerializeField] private float valueToShowIndicator = 0.7f;

    [Header("Taxi Calculation")]
    [SerializeField] private float aCoeff = -0.25f;
    [SerializeField] private float bCoeff = 0.25f;

    [SerializeField] private RoadsInfo roadsInfo;
    [SerializeField] private RoadsTimers roadsTimers;
    [SerializeField] private RoadIndicators roadIndicators;
    [SerializeField] private RoadTimer timer;
    bool pauseUpdate = true;

    public delegate void GameOver(Vector3 position, bool endByTimer);
    public event GameOver OnGameOver;

    private void Awake()
    {
        pauseUpdate = true;
    }

    private void FixedUpdate()
    {
        if (pauseUpdate) return;

        timer = new RoadTimer(Time.fixedDeltaTime, aCoeff, bCoeff, valueToShowIndicator);
        roadsInfo.UpdateTimer(timer);
        roadsTimers.UpdateTimer(timer);
        roadIndicators.UpdateMarks(timer);      
    }

    private void OnDestroy()
    {
        roadsTimers.OnTimerIsOver -= GameOverState;
    }

    public void SetRoads(PointsPair[] pointNumbersPairs, int[] endPointsWithParking)
    {
        roadsInfo = new RoadsInfo(pointNumbersPairs, endPointsWithParking);
        roadIndicators = new RoadIndicators(roadMarks,valueToShowIndicator, pointNumbersPairs.Length, roadsInfo);
    }

    public void SetMaxTimers(List<float> _maxTimeByRoad, List<float> _maxAdditionalTimeToWaitByRoad, int roadsCount)
    {
        roadsTimers = new RoadsTimers(roadsCount, _maxTimeByRoad, _maxAdditionalTimeToWaitByRoad, aCoeff, bCoeff);
        roadsTimers.OnTimerIsOver += GameOverState;
        pauseUpdate = false;
    }

    public bool CheckStartPoint(int startPointNumber)
    {
        return roadsInfo.CheckStartPoint(startPointNumber);

    }

    //public int GetStartPoint()
    //{
    //    return roadsInfo.GetStartPoint();
    //}

    public int GetEndPoint(int startPointNumber)
    {
        return roadsInfo.GetEndPoint(startPointNumber);
    }

    public bool CheckForEndPoint(int pointNumber, int startPointNumber)
    {
        return roadsInfo.CheckForEndPoint(pointNumber, startPointNumber);
    }

    public bool IsStopInParkingEndPoint(int endPointNumber)
    {
        return roadsInfo.EndPointIsParking(endPointNumber);
    }

    public void FreeParking(int endPointNumber)
    {
        roadsInfo.SetNewEndPointWithParkingState(endPointNumber, false);
    }

    public List<int> GetAllEndPointsForStartPoint(int startPointNumber)
    {
        return roadsInfo.GetAllEndPointsForStartPoint(startPointNumber);
    }

    public void RoadTriggersOnEnter(int startPointNumber)
    {
        roadsInfo.RoadTriggersOnEnter(startPointNumber);
    }

    public void AddVehicleOnRoad(int startPointNumber, TYPE type)
    {
        roadsInfo.AddUnitOnRoad(startPointNumber, type);
    }

    public void RemoveVehicleFromRoad(int startPointNumber, float totalTimeOnRoad, TYPE type)
    {
        roadsInfo.RemoveUnitFromRoad(startPointNumber, totalTimeOnRoad, type, roadsTimers);

    }

    public void GameOverState(int roadIndex)
    {
        pauseUpdate = true;
        if (OnGameOver != null) OnGameOver.Invoke(roadIndicators.GetMarkPosition(roadIndex), false);        
    }
}
