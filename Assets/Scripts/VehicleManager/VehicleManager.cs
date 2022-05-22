using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    [Header("Scripts")]
    //[SerializeField] private VehicleCreator vehicleCreator;
    [SerializeField] private LevelControl levelControl;
    [SerializeField] private RoadsManager roadsManager;
    [SerializeField] private MenusUIControl menusUIControl;
    [SerializeField] private CrashEffects crashEffects;

    [Header("----- Info -----")]
    [SerializeField] int crashCounter = 0;

    IDictionary<VehicleUnit.TYPE, Dictionary<int, List<int>>> freeVehicles = new Dictionary<VehicleUnit.TYPE, Dictionary<int, List<int>>>(); // free cars indexes   
    List<int> vehicles = new List<int>(); // all vehicles in scene
    List<Crash> crashes = new List<Crash>(); // current crashes

    List<VehicleUnit> allVehicles = new List<VehicleUnit>();
    public List<VehicleUnit> AllVehicles { get => allVehicles; }

    private void Start()
    {
        crashCounter = 0;
    }

    public int GetFreeVehicleIndexOfType(VehicleUnit.TYPE newType, int newVehicleName)
    {
        if (freeVehicles.ContainsKey(newType))
        {
            if (freeVehicles[newType].ContainsKey(newVehicleName))
            {
                if (freeVehicles[newType][newVehicleName].Count == 0)
                {
                    return -1;
                }
                else
                {
                    int index = freeVehicles[newType][newVehicleName][0];
                    freeVehicles[newType][newVehicleName].RemoveAt(0);
                    return index;
                }
            }
            else
            {
                freeVehicles[newType][newVehicleName] = new List<int>();
                return -1;
            }

        }
        else
        {
            freeVehicles[newType] = new Dictionary<int, List<int>>();
            return -1;
        }
    }

    public int AddNewVehicle(VehicleUnit newVehicle)
    {
        allVehicles.Add(newVehicle);
        int index = allVehicles.Count - 1;
        vehicles.Add(index);

        return index;
    }

    public void FreeEndPointWithParkingInCrash(int endPointWithParkingNumber)
    {
        roadsManager.SetNewEndPointWithParkingState(endPointWithParkingNumber, true);
    }

    public void AddVehicleToFree(int freeVehicleIndex, VehicleUnit.TYPE type, int vehicleName)
    {
        if (!freeVehicles.ContainsKey(type))
        {
            freeVehicles[type] = new Dictionary<int, List<int>>();            
        }

        if (!freeVehicles[type].ContainsKey(vehicleName))
        {
            freeVehicles[type][vehicleName] = new List<int>();
        }

        Logging.Log("VehicleManager: add to free index " + freeVehicleIndex + " of type " + type);
        freeVehicles[type][vehicleName].Add(freeVehicleIndex);
    }

    public void RemoveVehicleFromUnitsOnRoad(int startPointIndex, float totalTimeOnRoad, VehicleUnit.TYPE type)
    {
        Logging.Log("VehicleManager: remove vehicle of type " + type + " with start point " + startPointIndex);
        roadsManager.WhenRemoveVehicleFromRoadByStartPointNumber(startPointIndex, totalTimeOnRoad, type);
    }

    public void GameOverForEmergencyCar(Vector2 carPosition)
    {
        levelControl.GameOverBySpecialCar(carPosition);
    }

    public List<VehicleUnit> GetUnitsToAddToVehicleToFollow(VehicleScanner.DIRECTION direction, int roadStartPointNumber, int exceptIndex)
    {
        Logging.Log("VehicleManager: need range with direction " + direction + " start point " + roadStartPointNumber);
        List<VehicleUnit> result = new List<VehicleUnit>();

        foreach (int unitIndex in vehicles)
        {
            if (allVehicles[unitIndex].CheckUnitForDirectionAndRoad(exceptIndex, roadStartPointNumber, direction)) result.Add(allVehicles[unitIndex]);
        }

        return result;
    }

    #region Crashes
    public void RegisterCrash(int firstVehicleIndex, int secondVehicleIndex, Vector2 contactPosition)
    {
        bool newCrash = true;

        // check if index in crashes
        foreach (Crash crash in crashes)
        {
            if (crash.AreVehiclesInCrash(firstVehicleIndex, secondVehicleIndex))
            {
                crashEffects.StartCrashEffect(contactPosition);
                newCrash = false;
                break;
            }
        }

        if (newCrash)
        {
            // create new crash
            Logging.Log("VehicleManager: new crash! + contact position " + contactPosition);

            crashEffects.StartCrashEffect(contactPosition);
            crashes.Add(new Crash(firstVehicleIndex, secondVehicleIndex));
            crashCounter++;

            // update counter
            menusUIControl.UpdateCrashCounter(crashCounter, true);

            levelControl.CheckCrashCounter(crashCounter, allVehicles[firstVehicleIndex].gameObject.transform.position);           
        }
    }

    public void RemoveCrashByVehicleIndex(int vehicleManagerIndex)
    {
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
        else Logging.Log("VehicleManager: crash to remove is not found!");
    }
    #endregion
}