using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DirectionInfo
{
    [SerializeField] private DIRECTION _direction = DIRECTION.none;

    [SerializeField] private List<IPositionShearer> _positionsToChangeStateWhenDirectionNone = new List<IPositionShearer>();
    [SerializeField] private List<IVelocityShearer> _velocitiesToChangeStateWhenDirectionNone = new List<IVelocityShearer>();
    [SerializeField] private List<bool> _unitsStateWhenDirectionNone = new List<bool>();

    [SerializeField] private List<IPositionShearer> _unitsPositionToAddBeforeDirectionChange = new List<IPositionShearer>();

    public DirectionInfo(DIRECTION direction, List<IPositionShearer> unitsPositionToAdd)
    {
        _direction = direction;
        _unitsPositionToAddBeforeDirectionChange = unitsPositionToAdd;
    }

    public void ClearInfo()
    {
        _direction = DIRECTION.none;
    }

    public void AddUnit(IPositionShearer unitPosition, IVelocityShearer unitVelocity, bool state)
    {
        _positionsToChangeStateWhenDirectionNone.Add(unitPosition);
        _velocitiesToChangeStateWhenDirectionNone.Add(unitVelocity);
        _unitsStateWhenDirectionNone.Add(state);
    }

    public void MakeEndUpdate(List<IPositionShearer> positions, List<IVelocityShearer> velocities, List<float> distances, float minDistanceToKeep)
    {
        Logging.Log("Make and update for direction control");
        List<IPositionShearer> toRemove = _unitsPositionToAddBeforeDirectionChange;

        for (int i = 0; i < positions.Count; i++)
        {
            if (_unitsPositionToAddBeforeDirectionChange.Contains(positions[i]) && distances[i] <= minDistanceToKeep)
            {
                toRemove.Remove(positions[i]);
            }
        }
        ScannerUtilities.RemoveUnits(toRemove, positions, velocities);

        List<IPositionShearer> toAddPositions = new List<IPositionShearer>();
        List<IVelocityShearer> toAddVelocities = new List<IVelocityShearer>();

        for (int i = 0; i < _unitsStateWhenDirectionNone.Count; i++)
        {
            if (_unitsStateWhenDirectionNone[i])
            {
                toAddPositions.Add(_positionsToChangeStateWhenDirectionNone[i]);
                toAddVelocities.Add(_velocitiesToChangeStateWhenDirectionNone[i]);
            }
            else if (toAddPositions.Contains(_positionsToChangeStateWhenDirectionNone[i]))
            {
                toAddPositions.Remove(_positionsToChangeStateWhenDirectionNone[i]);
                toAddVelocities.Remove(_velocitiesToChangeStateWhenDirectionNone[i]);
            }
        }
       
        ScannerUtilities.AddUnits(toAddPositions, toAddVelocities, positions, velocities);
    }

}
