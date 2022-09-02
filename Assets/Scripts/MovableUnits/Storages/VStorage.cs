using System.Collections.Generic;

public interface IGetUnitsOnRoad
{
    public UnitsShearers GetUnitsToAddToVehicleToFollow(DIRECTION direction, int roadStartPointNumber, int exceptIndex);
}
public class VStorage: IGetUnitsOnRoad
{
    IDictionary<TYPE, Dictionary<int, List<int>>> freeUnits = new Dictionary<TYPE, Dictionary<int, List<int>>>(); // free cars indexes by TYPE and nameCode   

    IDictionary<int, int[]> activeUnits = new Dictionary<int, int[]>(); // all vehicles in scene with TYPe and nameCode

    List<IDirectionShearer> allUnitsDirection = new List<IDirectionShearer>();
    List<IVelocityShearer> allUnitsVelocity = new List<IVelocityShearer>();

    public int GetFreeUnitIndexOfType(TYPE type, int nameCode)
    {
        if (freeUnits.ContainsKey(type))
        {
            if (freeUnits[type].ContainsKey(nameCode))
            {
                if (freeUnits[type][nameCode].Count == 0)
                {
                    return -1;
                }
                else
                {
                    int index = freeUnits[type][nameCode][0];
                    freeUnits[type][nameCode].RemoveAt(0);
                    return index;
                }
            }
            else
            {
                freeUnits[type][nameCode] = new List<int>();
                return -1;
            }
        }
        else
        {
            freeUnits[type] = new Dictionary<int, List<int>>();
            return -1;
        }
    }

    public int AddUnitAndGetIndex(IDirectionShearer direction, IVelocityShearer velocity)
    {
        allUnitsDirection.Add(direction);
        allUnitsVelocity.Add(velocity);
        return allUnitsDirection.Count - 1;
    }

    public IDirectionShearer ReserveUnit(int unitIndex, TYPE type, int nameCode)
    {
        if (unitIndex < allUnitsDirection.Count)
        {
            activeUnits[unitIndex] = new int[2] { (int)type, nameCode };
            return allUnitsDirection[unitIndex];
        }
        else return null;
    }

    public KeyValuePair<TYPE, IDirectionShearer> GetUnitTypeAndDirection(int unitIndex)
    {
        if (!activeUnits.ContainsKey(unitIndex))
        {
            return new KeyValuePair<TYPE, IDirectionShearer>();
        }

        TYPE type = (TYPE)activeUnits[unitIndex][0];
        return new KeyValuePair<TYPE, IDirectionShearer>(type, allUnitsDirection[unitIndex]);
    }

    public KeyValuePair<TYPE, IDirectionShearer> FreeUnit(int unitIndex)
    {
        if (!activeUnits.ContainsKey(unitIndex))
        {
            return new KeyValuePair<TYPE, IDirectionShearer>();
        }

        TYPE type = (TYPE) activeUnits[unitIndex][0];
        int nameCode = activeUnits[unitIndex][1];

        if (!freeUnits.ContainsKey(type))
        {
            freeUnits[type] = new Dictionary<int, List<int>>();
        }

        if (!freeUnits[type].ContainsKey(nameCode))
        {
            freeUnits[type][nameCode] = new List<int>();
        }

        freeUnits[type][nameCode].Add(unitIndex);

        activeUnits.Remove(unitIndex);

        return new KeyValuePair<TYPE, IDirectionShearer> (type, allUnitsDirection[unitIndex]);
    }

    public UnitsShearers GetUnitsToAddToVehicleToFollow (DIRECTION direction, int roadStartPointNumber, int exceptIndex)
    {
        List<IPositionShearer> _positions = new List<IPositionShearer>();
        List<IVelocityShearer> _velocities = new List<IVelocityShearer>();

        foreach (int unitIndex in activeUnits.Keys)
        {
            if (unitIndex == exceptIndex) continue;

            if (allUnitsDirection[unitIndex].GetRoadStartPoint() == roadStartPointNumber && allUnitsDirection[unitIndex].GetDirection() == direction)
            {
                _positions.Add(((VScanner)allUnitsDirection[unitIndex]).PositionShearer);
                _velocities.Add(allUnitsVelocity[unitIndex]);
            }
        }

        return new UnitsShearers(_positions, _velocities);
    }
}
