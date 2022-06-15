using System.Collections.Generic;

public interface IConvertToPointNumber
{
    public int ConvertToPointNumber(int roadIndex);
}

[System.Serializable]
public class RoadsInfo: IConvertToPointNumber
{
    IDictionary<int, int> roads = new Dictionary<int, int>();
    IDictionary<int, PointsPair> startPointsPairs = new Dictionary<int, PointsPair>();
    IDictionary<int, int> startPointsNumbers = new Dictionary<int, int>();
    IDictionary<int, bool> endPointsWithParkingState = new Dictionary<int, bool>();

    List<int> roadStates = new List<int>();
    public List<int> unitsOnRoads = new List<int>();
    List<int> taxiUnitsOnRoads = new List<int>();    

    public RoadsInfo (PointsPair[] _pointNumbersPairs, int[] endPointsWithParking)
    {
        for (int i = 0; i < _pointNumbersPairs.Length; i++)
        {
            roads[_pointNumbersPairs[i].StartPointNumber] = i;
            startPointsPairs[_pointNumbersPairs[i].StartPointNumber] = _pointNumbersPairs[i];
            startPointsNumbers[i] = _pointNumbersPairs[i].StartPointNumber;

            unitsOnRoads.Add(0);
            taxiUnitsOnRoads.Add(0);
            roadStates.Add(0);
        }

        if (endPointsWithParking.Length > 0)
        {
            foreach (int point in endPointsWithParking)
            {
                endPointsWithParkingState[point] = true;
            }
        }
    }

    public void RoadTriggersOnEnter(int startPointNumber)
    {
        int roadIndex = roads[startPointNumber];

        if (roadIndex < roadStates.Count)
            roadStates[roadIndex] -= 1;
    }
    public void AddUnitOnRoad(int startPointNumber, TYPE type)
    {        
        if (type == TYPE.train) return;

        int roadIndex = roads[startPointNumber];
        roadStates[roadIndex] += 1;
        unitsOnRoads[roadIndex] += 1;

        if (type == TYPE.taxi)
        {
            taxiUnitsOnRoads[roadIndex] += 1;
        }
    }

    public void RemoveUnitFromRoad(int startPointNumber, float totalTimeOnRoad, TYPE type, RoadsTimers timers)
    {
        if (!roads.ContainsKey(startPointNumber)) return;
        else if (type == TYPE.train) return;

        int roadIndex = roads[startPointNumber];
        unitsOnRoads[roadIndex] -= 1;

        timers.RemoveUnitFromRoad(roadIndex, totalTimeOnRoad, type, taxiUnitsOnRoads[roadIndex]);

        if (type == TYPE.taxi)
        {         
            taxiUnitsOnRoads[roadIndex] -= 1;
        }

        // change parking state

    }

    public int ConvertToPointNumber(int roadIndex)
    {
        if (roadIndex < startPointsNumbers.Count) return startPointsNumbers[roadIndex];
        else return -1;
    }

    public void UpdateTimer(RoadTimer timer)
    {
        timer.UpdateUnits(unitsOnRoads, taxiUnitsOnRoads);
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

    //public int GetStartPoint()
    //{
    //    if (roads.Count == 0)
    //    {
    //        Logging.Log("RoadsManager: no roads!");
    //        return -1;
    //    }
    //    List<int> freeRoads = new List<int>();

    //    for (int i = 0; i < roadStates.Count; i++)
    //    {
    //        if (roadStates[i] == 0)
    //        {
    //            if (startPointsNumbers.ContainsKey(i)) freeRoads.Add(startPointsNumbers[i]);
    //        }
    //    }

    //    if (freeRoads.Count > 0)
    //    {
    //        return freeRoads[Random.Range(0, freeRoads.Count)];
    //    }
    //    else
    //    {
    //        return -1;
    //    }
    //}

    public List<int> GetAllEndPointsForStartPoint(int startPointNumber)
    {
        return startPointsPairs[startPointNumber].GetAllEndPoints();
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

    public bool EndPointIsParking(int endPointNumber)
    {
        return endPointsWithParkingState.ContainsKey(endPointNumber);
    }

    bool CheckEndPointParkingState(int endPointNumber)
    {
        if (EndPointIsParking(endPointNumber)) return endPointsWithParkingState[endPointNumber];
        else return true;
    }

    public void SetNewEndPointWithParkingState(int endPointNumber, bool newState)
    {
        if (EndPointIsParking(endPointNumber))
        {
            endPointsWithParkingState[endPointNumber] = newState;
        }
    }

}
