using System.Collections.Generic;
using UnityEngine;

public interface IGetDistanceInfo
{
    public abstract List<float> GetInfo();
}

public interface IHoldScannerUnitsInfo
{
    public abstract void AddUnit(IPositionShearer unitPosition, IVelocityShearer unitVelocity);
    public abstract void RemoveUnit(IPositionShearer unitPosition, IVelocityShearer unitVelocity);
}

[System.Serializable]
public class VScannerUnitsInfo: IGetDistanceInfo, IHoldScannerUnitsInfo
{   
    private float _maxDistanceToDetectUnit = 10f;
    private float _minDistanceToKeep = 3f;
    //[SerializeField] private TrafficLight trafficLightToFollow;
    [SerializeField] private List<IPositionShearer> _unitsPosition = new List<IPositionShearer>();
    [SerializeField] private List<IVelocityShearer> _unitsVelocity = new List<IVelocityShearer>();

    private IPositionShearer _positionShearer;
    private IDirectionShearer directionShearer;
    private IRoadInfoShearer _roadInfo;

    [SerializeField] private bool _directionControl;
    [SerializeField] private DirectionInfo _directionInfo;
    [SerializeField] private bool _needLog = false;

    [SerializeField] private List<GameObject> _units = new List<GameObject>();

    public VScannerUnitsInfo(IPositionShearer positionShearer, float _maxDistance, IDirectionShearer _directionShearer,IRoadInfoShearer roadInfo)
    {
        _maxDistanceToDetectUnit = _maxDistance;
        _positionShearer = positionShearer;

        if (positionShearer == null)
        {
            Logging.Log("Position shearer is null");
        }

        directionShearer = _directionShearer;
        directionShearer.OnDirectionControlChangeFromNone += ChangeDirectionControlFromNone;
        directionShearer.OnDirectionControlChangeToNone += ChangeDirectionControlToNone;

        _roadInfo = roadInfo;
    }

    public void Destroy()
    {
        try
        {
            directionShearer.OnDirectionControlChangeFromNone -= ChangeDirectionControlFromNone;
            directionShearer.OnDirectionControlChangeToNone -= ChangeDirectionControlToNone;
        }
        catch { }
    }

    void ChangeDirectionControlFromNone(DIRECTION direction, bool directionControl)
    {
        // remove with another direction
        ScannerUtilities.RemoveUnitsWithAnotherDirection(direction, _unitsPosition, _unitsVelocity);

        _directionControl = directionControl;

        if (_directionControl)
        {            
            // get all units with the same direction from manager and add them to unitsPosition, unitsVelocities
            UnitsShearers _unitsShearers = _roadInfo.GetUnitsToAddToVehicleToFollow(directionShearer);
            ScannerUtilities.AddUnits(_unitsShearers.Positions, _unitsShearers.Velocities, _unitsPosition, _unitsVelocity);

            // add to directionInfo
          
            _directionInfo = new DirectionInfo(direction, _unitsShearers.Positions);
        }

        UpdateUnits();
    }

    void ChangeDirectionControlToNone(DIRECTION direction, bool directionControl)
    {        
        if (_directionControl && _directionInfo != null)
        {
            Logging.Log("Change direction and direction control");
            _directionInfo.MakeEndUpdate(_unitsPosition, _unitsVelocity, GetInfo(), _minDistanceToKeep);
            _directionInfo = null;
        }
        _directionControl = directionControl;

        UpdateUnits();
    }

    public List<float> GetInfo()
    {
        List<float> distance = new List<float>();
        Vector3 position = _positionShearer.GetPosition();
        float gap = _positionShearer.GetGap();

        distance.AddRange(GetDistanceToVehicles(position, gap));
        
        return distance;
    }
    public void AddUnit(IPositionShearer unitPosition, IVelocityShearer unitVelocity)
    { 
        if (!_unitsPosition.Contains(unitPosition))
        {
            _unitsPosition.Add(unitPosition);
            _unitsVelocity.Add(unitVelocity);
        }

        if (_directionControl)
        {
            _directionInfo.AddUnit(unitPosition, unitVelocity, true);
        }

        UpdateUnits();
    }

    public void RemoveUnit(IPositionShearer unitPosition, IVelocityShearer unitVelocity)
    {
        if (_unitsPosition.Contains(unitPosition))
        {
            if (_directionControl)
            {
                Logging.Log("Mark as removed due to directionControl");
                _directionInfo.AddUnit(unitPosition, unitVelocity, false);
                if (unitVelocity.GetInCrashAndNonActive())
                {
                    Logging.Log("Remove due to crash");
                    ScannerUtilities.RemoveUnitAtIndex(_unitsPosition.IndexOf(unitPosition), _unitsPosition, _unitsVelocity);
                }
            }
            else
            {
                ScannerUtilities.RemoveUnitAtIndex(_unitsPosition.IndexOf(unitPosition), _unitsPosition, _unitsVelocity);
            }
            UpdateUnits();
        }
    }

    float[] GetDistanceToVehicles(Vector3 position, float gap)
    {
        if (_unitsPosition.Count == 0) return new float[4] { -100f, -100f, -100f, -100f};

        float toUnitMinDistance = 1000f;
        float toUnitMinVelocity = 0f;
        float distanceToVehicleWithZeroVelocity = 1000f;

        List<int> removeDueToDistance = new List<int>();

        for (int i = 0; i < _unitsPosition.Count; i++)
        {
            float newDistance = ScannerUtilities.DistanceToSingleUnitObject(position, _positionShearer.GetSetToOriginVelocityVector(), _unitsPosition[i], gap);

            if (_positionShearer.GetDirectionShearer().GetDirection() != DIRECTION.none)
            {
                if ((_unitsPosition[i].GetDirectionShearer().GetDirection() != _positionShearer.GetDirectionShearer().GetDirection()) && newDistance > _minDistanceToKeep)
                {
                    newDistance = _maxDistanceToDetectUnit + 1;
                    if (_directionControl) _directionInfo.AddUnit(_unitsPosition[i], _unitsVelocity[i], false);
                }
            }

            //newDistance = ScannerUtilities.DistanceToSingleUnitObject(position, _positionShearer.GetSetToOriginVelocityVector(), _unitsPosition[i], gap);

            if (_needLog) Logging.Log("Distance to unit " + _unitsPosition[i].GetName() + " is " + newDistance);

            if (newDistance == -100f) continue;
            else if (newDistance > _maxDistanceToDetectUnit)
            {
                Logging.Log("....remove due to distance " + newDistance + " index is " + i);
                removeDueToDistance.Add(i);
                continue;
            }

            float velocity = _unitsVelocity[i].GetVelocity();

            if (velocity < 0.01f)
            {
                distanceToVehicleWithZeroVelocity = Mathf.Min(distanceToVehicleWithZeroVelocity, newDistance);
            }
            else
            {
                if (newDistance < toUnitMinDistance)
                {
                    toUnitMinDistance = newDistance;
                    toUnitMinVelocity = velocity;
                }
            }           
        }

        ScannerUtilities.RemoveUnitsAtIndexes(removeDueToDistance, _unitsPosition, _unitsVelocity);

        if (distanceToVehicleWithZeroVelocity == 1000f) distanceToVehicleWithZeroVelocity = -100f;
        if (toUnitMinDistance == 1000f) toUnitMinDistance = -100f;

        UpdateUnits();
        return new float[4] { toUnitMinVelocity, toUnitMinDistance, 0f, distanceToVehicleWithZeroVelocity };
    }

    private void UpdateUnits()
    {
        _units.Clear();
        for (int i = 0; i < _unitsPosition.Count; i++) _units.Add(_unitsPosition[i].GetName());
    }
}