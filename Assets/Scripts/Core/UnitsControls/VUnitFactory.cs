using MovableUnits.Units;
using System.Collections.Generic;
using UnityEngine;
using IJ.Utilities;

public class VUnitFactory
{
    IDictionary<TYPE, List<Unit>> unitsCatalog = new Dictionary<TYPE, List<Unit>>();
    
    public VUnitFactory(List<Unit> units)
    {
        MakeCatalog(units);
    }

    void MakeCatalog(List<Unit> units)
    {
        foreach (Unit unit in units)
        {
            if (!unitsCatalog.ContainsKey(unit.Type))
            {
                unitsCatalog[unit.Type] = new List<Unit>();
            }
            unitsCatalog[unit.Type].Add(unit);
        }
    }

    protected Unit GetUnitOfType(TYPE type)
    {
        if (unitsCatalog.ContainsKey(type))
        {
            if (unitsCatalog.Count == 0) return null;

            return unitsCatalog[type][Random.Range(0, unitsCatalog[type].Count)];
        }
        else
        {
            return null;
        }
    }

    public virtual VInfo SpawnUnitAndGetInfo(IInstantiatePrefab factoryMethod,TYPE type, List<Path> paths, VStorage storage, bool stopInParking)
    {
        Unit unit = GetUnitOfType(type);

        // get free instance or create new instance
        int indexOfFreeVehicle = storage.GetFreeUnitIndexOfType(unit.Type, unit.VehicleName.GetHashCode());

        if (indexOfFreeVehicle == -1)
        {
            // create new and add to AllVehicles
            Transform temp = factoryMethod.InstantiatePrefab(unit.Prefab);
            indexOfFreeVehicle = storage.AddUnitAndGetIndex(temp.GetComponent<IDirectionShearer>(), temp.GetComponent<IVelocityShearer>());
        }

        IDirectionShearer unitShearer = storage.ReserveUnit(indexOfFreeVehicle, unit.Type, unit.VehicleName.GetHashCode());
        if (unitShearer == null) return null;

        VInfo vInfo = (unitShearer as VScanner).GetComponent<VInfo>();

        if (vInfo.GetComponent<VRoadMemberTag>()) vInfo.GetComponent<VRoadMemberTag>().SetRoadInfo(storage as IGetUnitsOnRoad, indexOfFreeVehicle);

        vInfo.GetComponent<IVUnit>().StartVehicle(paths, indexOfFreeVehicle, stopInParking);

        return vInfo;
    }
}
