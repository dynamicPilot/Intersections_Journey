using System.Collections.Generic;

public class Crash
{
    List<int> vehiclesIndexesInCrash = new List<int>();

    public void AddVehicleToCrash(int vehicleIndexesToAdd)
    {
        if (!vehiclesIndexesInCrash.Contains(vehicleIndexesToAdd)) vehiclesIndexesInCrash.Add(vehicleIndexesToAdd);
    }

    //public void RemoveVehicle(int vehicleIndexToRemove)
    //{
    //    vehiclesIndexesInCrash.Remove(vehicleIndexToRemove);
    //}

    public bool IsVehicleInCrash(int vehicleIndex)
    {
        return vehiclesIndexesInCrash.Contains(vehicleIndex);
    }

    public bool AreVehiclesInCrash(int firstVehicleIndex, int secondVehicleIndex, bool addAnotherToCrash = true)
    {
        foreach(int index in vehiclesIndexesInCrash)
        {
            if (index == firstVehicleIndex)
            {
                AddVehicleToCrash(secondVehicleIndex);
                return true;
            }
            else if (index == secondVehicleIndex)
            {
                AddVehicleToCrash(firstVehicleIndex);
                return true;
            }
        }

        return false;
    }

    public Crash(int firstVehicleIndex, int secondVehicleIndex)
    {
        vehiclesIndexesInCrash.Clear();
        vehiclesIndexesInCrash.Add(firstVehicleIndex);
        vehiclesIndexesInCrash.Add(secondVehicleIndex);
    }
}
