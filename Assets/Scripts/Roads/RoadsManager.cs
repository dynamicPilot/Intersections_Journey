using System.Collections.Generic;
using UnityEngine;

public class RoadsManager : MonoBehaviour
{
    [Header("Scripts")]
    //[SerializeField] private WayPoints wayPoints;
    [SerializeField] private LevelControl levelControl;
    [SerializeField] private GameMaster gameMaster;
    [SerializeField] private RoadMarksControl roadMarks;

    [Header("Settings")]
    [SerializeField] private float valueToShowIndicator = 0.7f;

    [Header("Taxi Calculation")]
    [SerializeField] private float aCoeff = -0.25f;
    [SerializeField] private float bCoeff = 0.25f;

    IDictionary<int, int> roads = new Dictionary<int, int>();
    IDictionary<int, PointsPair> startPointsPairs = new Dictionary<int, PointsPair>();
    IDictionary<int, int> startPointsNumbers = new Dictionary<int, int>();
    IDictionary<int, bool> endPointsWithParkingState = new Dictionary<int, bool>();

    [Header("----- Info -----")]
    [SerializeField] List<int> roadStates = new List<int>();
    [SerializeField] List<int> unitsOnRoads = new List<int>();
    [SerializeField] List<int> taxiUnitsOnRoads = new List<int>();
    [SerializeField] List<float> roadTimers = new List<float>();
    //[SerializeField] List<float> maxMinVelocityOnRoads = new List<float>();

    List<float> maxTimeByRoad = new List<float>();
    [SerializeField] List<float> maxAdditionalTimeToWaitByRoad = new List<float>();
    //List<int> maxMinVelocityOnRoadCounters = new List<int>();
    List<int> roadsWithAllertOn = new List<int>();

    bool pauseUpdate = true;

    private void Awake()
    {
        pauseUpdate = true;
    }

    private void FixedUpdate()
    {
        if (pauseUpdate) return;

        // calculate roads timers
        for (int i = 0; i< unitsOnRoads.Count; i++)
        {
            if (unitsOnRoads[i] > 0)
            {
                roadTimers[i] += (unitsOnRoads[i] - taxiUnitsOnRoads[i]) * Time.deltaTime + (taxiUnitsOnRoads[i] + aCoeff + taxiUnitsOnRoads[i] * bCoeff) * Time.deltaTime;

                if (roadTimers[i] >= maxTimeByRoad[i])
                {
                    if (!roadsWithAllertOn.Contains(i))
                    {
                        Logging.Log("RoadsManager: start allert...."); 
                        StartAllert(i);
                    }
                    else if (roadTimers[i] >= maxTimeByRoad[i] + (maxAdditionalTimeToWaitByRoad[i] * unitsOnRoads[i]))
                    {
                        Logging.Log("RoadsManager: ROAD IS FULL --> END GAME");
                        gameMaster.EndLevel(false, roadMarks.GetIndicatorPosition(startPointsNumbers[i]).x, roadMarks.GetIndicatorPosition(startPointsNumbers[i]).y);
                    }

                    roadMarks.UpdateIndicatorAllertValue(startPointsNumbers[i], (roadTimers[i] - maxTimeByRoad[i]) / (maxAdditionalTimeToWaitByRoad[i] * unitsOnRoads[i]));
                }
                else
                {
                    // if allert is on but need to be off
                    if (roadsWithAllertOn.Contains(i))
                    {
                        StopAllert(i, true);
                    }

                    if (roadTimers[i] / maxTimeByRoad[i] >= valueToShowIndicator)
                    {
                        // update indicator
                        roadMarks.UpdateIndicatorValue(startPointsNumbers[i], roadTimers[i] / maxTimeByRoad[i]);
                    }
                    else if (roadMarks.IsIndicatorOn(startPointsNumbers[i]))
                    {
                        roadMarks.StopIndicator(startPointsNumbers[i]);
                    }
                }
            }
            else if (unitsOnRoads[i] == 0 && roadTimers[i] != 0)
            {
                roadTimers[i] = 0;
                if (roadsWithAllertOn.Contains(i)) StopAllert(i);
                roadMarks.StopIndicator(startPointsNumbers[i]);
            }

            //if (roadTimers[i] == 0)
            //{
            //    Logging.Log("RoadsManager: STOP INDICATOR");
                
            //}
        }        
    }


    public void SetRoads(PointsPair[] newPointNumbersPairs, int[] endPointsWithParking)
    {
        for (int i = 0; i < newPointNumbersPairs.Length; i++)
        {
            roads[newPointNumbersPairs[i].StartPointNumber] = i;
            startPointsPairs[newPointNumbersPairs[i].StartPointNumber] = newPointNumbersPairs[i];
            startPointsNumbers[i] = newPointNumbersPairs[i].StartPointNumber;

            unitsOnRoads.Add(0);
            taxiUnitsOnRoads.Add(0);
            //maxMinVelocityOnRoads.Add(0);
            roadStates.Add(0);
            roadTimers.Add(0);
            //maxMinVelocityOnRoadCounters.Add(0);
        }

        if (endPointsWithParking.Length > 0)
        {
            foreach(int point in endPointsWithParking)
            {
                endPointsWithParkingState[point] = true;
            }
        }

        //SetMaxTimers();
    }

    public void SetMaxTimers(List<float> newMaxTimeByRoad, List<float> newMaxAdditionalTimeToWaitByRoad)
    {
        maxTimeByRoad = newMaxTimeByRoad;
        maxAdditionalTimeToWaitByRoad = newMaxAdditionalTimeToWaitByRoad;
        pauseUpdate = false;
    }

    public bool CheckStartPoint(int startPointNumber)
    {
        if (roads.Count == 0)
        {
            Logging.Log("RoadsManager: no roads!");
            return false;
        }
        else if (!roads.ContainsKey(startPointNumber))
        {
            Logging.Log("RoadsManager: no start point " + startPointNumber);
            return false;
        }

        return roadStates[roads[startPointNumber]] == 0;

    }

    public int GetStartPoint()
    {
        if (roads.Count == 0)
        {
            Logging.Log("RoadsManager: no roads!");
            return -1;
        }

        //Logging.Log("RoadsManager: start searching for start point");

        List<int> freeRoads = new List<int>();

        for (int i = 0; i < roadStates.Count; i++)
        {
            if (roadStates[i] == 0)
            {
                if (startPointsNumbers.ContainsKey(i)) freeRoads.Add(startPointsNumbers[i]);
            }
        }

        if (freeRoads.Count > 0)
        {
            return freeRoads[Random.Range(0, freeRoads.Count)];
        }
        else
        {
            return -1;
        }
    }

    public int GetEndPoint(int startPointNumber)
    {
        int randomEndPoint = startPointsPairs[startPointNumber].GetRandomEndPointNumber();
        while (!CheckEndPointParkingState(randomEndPoint))
        {
            randomEndPoint = startPointsPairs[startPointNumber].GetRandomEndPointNumber();
        }
        SetNewEndPointWithParkingState(randomEndPoint, false);
        return randomEndPoint;
    }

    public bool CheckForEndPoint(int pointNumber, int startPointNumber)
    {
        return startPointsPairs[startPointNumber].IsEndPoint(pointNumber);
    }

    bool CheckEndPointParkingState(int endPointNumber)
    {
        if (endPointsWithParkingState.ContainsKey(endPointNumber)) return endPointsWithParkingState[endPointNumber];
        else return true;
    }

    public bool IsStopInParkingEndPoint(int endPointNumber)
    {
        return endPointsWithParkingState.ContainsKey(endPointNumber);
    }

    public void SetNewEndPointWithParkingState(int endPointNumber, bool newState)
    {
        if (endPointsWithParkingState.ContainsKey(endPointNumber))
        {
            endPointsWithParkingState[endPointNumber] = newState;
        }
    }

    public List<int> GetAllEndPointsForStartPoint(int startPointNumber)
    {
        return startPointsPairs[startPointNumber].GetAllEndPoints();
    }

    void StartAllert(int roadIndex)
    {
        roadsWithAllertOn.Add(roadIndex);
        roadMarks.StartAllert(startPointsNumbers[roadIndex]);

    }

    void StopAllert(int roadIndex, bool needAnimateStop = false)
    {
        if (needAnimateStop) roadMarks.StopAllert(startPointsNumbers[roadIndex]);
        roadsWithAllertOn.Remove(roadIndex);       
    }

    public void WhenAddVehicleOnRoad(int startPointNumber, VehicleUnit.TYPE type)
    {
        int index = roads[startPointNumber];
        roadStates[index] += 1;
        if (type != VehicleUnit.TYPE.train) unitsOnRoads[index] += 1;
        
        if (type == VehicleUnit.TYPE.taxi)
        {
            taxiUnitsOnRoads[index] += 1;
        }

        Logging.Log("RoadsManager: add vehicle to road " + roads[startPointNumber]);
    }

    public void WhenRoadTriggerExit(int startPointNumber)
    {
        int directionNumber = roads[startPointNumber];
        Logging.Log("RoadsManager: WhenRoadTriggerExit: trigger start point number is " + startPointNumber);
        if (directionNumber < roadStates.Count)
            roadStates[directionNumber] -= 1;        
    }

    public void WhenRemoveVehicleFromRoad(int startPointNumber, float totalTimeOnRoad, VehicleUnit.TYPE type)
    {
        if (!roads.ContainsKey(startPointNumber)) return;
        else if (type == VehicleUnit.TYPE.train) return;

        int directionNumber = roads[startPointNumber];
        // Add vehicle to units on road
        unitsOnRoads[directionNumber] -= 1;
        roadTimers[directionNumber] -= totalTimeOnRoad;

        if (type == VehicleUnit.TYPE.taxi)
        {
            roadTimers[directionNumber] -= totalTimeOnRoad * (aCoeff + bCoeff * taxiUnitsOnRoads[directionNumber]); // PAY ATTENTION! Possibly it's need some change later!
            taxiUnitsOnRoads[directionNumber] -= 1;
        }

        //if (maxMinVelocityOnRoads[directionNumber] == maxVehicleVelocity)
        //{
        //    maxMinVelocityOnRoadCounters[directionNumber] -= 1;

        //    // reset velocity
        //    if (maxMinVelocityOnRoadCounters[directionNumber] == 0)
        //    {
        //        maxMinVelocityOnRoads[directionNumber] = 0;
        //    }
        //}
    }

    public void WhenRemoveVehicleFromRoadByStartPointNumber(int startPointNumber, float totalTimeOnRoad, VehicleUnit.TYPE type)
    {
        if (roads.ContainsKey(startPointNumber)) WhenRemoveVehicleFromRoad(startPointNumber, totalTimeOnRoad, type);
    }
}
