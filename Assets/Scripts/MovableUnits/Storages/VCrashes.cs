using System.Collections.Generic;

public class VCrashes
{
    List<Crash> crashes = new List<Crash>();
    public bool RegisterCrash(int firstVehicleIndex, int secondVehicleIndex)
    {
        bool newCrash = true;

        // check if index in crashes
        foreach (Crash crash in crashes)
        {
            if (crash.AreVehiclesInCrash(firstVehicleIndex, secondVehicleIndex))
            {
                newCrash = false;
                break;
            }
        }

        if (newCrash)
        {
            crashes.Add(new Crash(firstVehicleIndex, secondVehicleIndex));
            return true;
        }
        return false;
    }

    public void RemoveCrashByVehicleIndex(int vehicleManagerIndex)
    {
        if (crashes.Count == 0) return;

        Crash crashToRemove = null;
        foreach (Crash crash in crashes)
        {
            if (crash.IsVehicleInCrash(vehicleManagerIndex))
            {
                crashToRemove = crash;
                break;
            }
        }

        if (crashToRemove != null) crashes.Remove(crashToRemove);
    }
}
