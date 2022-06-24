using System.Collections.Generic;

public class DirectionInfo
{
    private DIRECTION _direction = DIRECTION.none;

    private List<IPositionShearer> _positionsToChangeStateWhenDirectionNone = new List<IPositionShearer>();
    private List<IVelocityShearer> _velocitiesToChangeStateWhenDirectionNone = new List<IVelocityShearer>();
    private List<bool> _unitsStateWhenDirectionNone = new List<bool>();

    private List<IPositionShearer> _unitsPositionToAddBeforeDirectionChange = new List<IPositionShearer>();

    public DirectionInfo (DIRECTION direction, List<IPositionShearer> unitsPositionToAdd)
    {
        _direction = direction;
        _unitsPositionToAddBeforeDirectionChange = unitsPositionToAdd;
    }

    public void AddUnit(IPositionShearer unitPosition, IVelocityShearer unitVelocity, bool state)
    {
        _positionsToChangeStateWhenDirectionNone.Add(unitPosition);
        _velocitiesToChangeStateWhenDirectionNone.Add(unitVelocity);
        _unitsStateWhenDirectionNone.Add(state);
    }

    public void MakeEndUpdate(List<IPositionShearer> positions, List<IVelocityShearer> velocities)
    {
        ScannerUtilities.RemoveUnits(_unitsPositionToAddBeforeDirectionChange, ref positions, ref velocities);

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
       
        ScannerUtilities.AddUnits(toAddPositions, toAddVelocities, ref positions, ref velocities);
    }

}
