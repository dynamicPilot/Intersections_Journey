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

    [SerializeField] private TrafficLight trafficLightToFollow;
    private List<IPositionShearer> _unitsPosition = new List<IPositionShearer>();
    private List<IVelocityShearer> _unitsVelocity = new List<IVelocityShearer>();

    private IPositionShearer _positionShearer;
    private IDirectionShearer directionShearer;
    private IRoadInfoShearer _roadInfo;

    private bool _directionControl;
    private DirectionInfo _directionInfo;

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
        ScannerUtilities.RemoveUnitsWithAnotherDirection(direction, ref _unitsPosition, ref _unitsVelocity);

        _directionControl = directionControl;

        if (_directionControl)
        {            
            // get all units with the same direction from manager and add them to unitsPosition, unitsVelocities
            UnitsShearers _unitsShearers = _roadInfo.GetUnitsToAddToVehicleToFollow(directionShearer);
            ScannerUtilities.AddUnits(_unitsShearers.Positions, _unitsShearers.Velocities, ref _unitsPosition, ref _unitsVelocity);

            // add to directionInfo
            _directionInfo = new DirectionInfo(direction, _unitsShearers.Positions);
        }
    }

    void ChangeDirectionControlToNone(DIRECTION direction, bool directionControl)
    {        
        if (_directionControl && _directionInfo != null)
        {
            _directionInfo.MakeEndUpdate(_unitsPosition, _unitsVelocity);
            _directionInfo = null;
        }
        _directionControl = directionControl;
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
    }

    public void RemoveUnit(IPositionShearer unitPosition, IVelocityShearer unitVelocity)
    {
        if (_unitsPosition.Contains(unitPosition))
        {
            if (_directionControl)
            {
                _directionInfo.AddUnit(unitPosition, unitVelocity, false);
            }
            else
            {
                ScannerUtilities.RemoveUnitAtIndex(_unitsPosition.IndexOf(unitPosition), ref _unitsPosition, ref _unitsVelocity);
            }
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

            if (newDistance == -100f) continue;
            else if (newDistance > _maxDistanceToDetectUnit)
            {
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

        ScannerUtilities.RemoveUnitsAtIndexes(removeDueToDistance, ref _unitsPosition, ref _unitsVelocity);

        if (distanceToVehicleWithZeroVelocity == 1000f) distanceToVehicleWithZeroVelocity = -100f;

        return new float[4] { toUnitMinVelocity, toUnitMinDistance, 0f, distanceToVehicleWithZeroVelocity };
    }
}