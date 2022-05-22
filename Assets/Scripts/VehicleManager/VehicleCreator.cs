 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCreator : MonoBehaviour
{
    [Header("Vehicle Container")]
    [SerializeField] private Transform vehicleContainer;

    [Header("Vehicles")]
    [SerializeField] private List<Vehicle> vehicles;
    //[SerializeField] private Addre

    [Header("Scripts")]
    [SerializeField] private WayCreator wayCreator;
    [SerializeField] private VehicleManager vehicleManager;
    [SerializeField] private RoadsManager roadsManager;
    

    [Header("---------- TEST ONLY -----------")]
    [SerializeField] private float timeDeltsToSpawnCar = 1f;
    [SerializeField] private int totalNumber = 10;
    
    WaitForSeconds updater;
    int testNumberCounter = 0;
    IDictionary<VehicleUnit.TYPE, List<Vehicle>> vehiclesOfType = new Dictionary<VehicleUnit.TYPE, List<Vehicle>>();

    [SerializeField] private List<TimePoint> timePointsForRepairCars = new List<TimePoint>();
    [SerializeField] private List<int> indexesOfRepairSiteToRepairCars = new List<int>();
    [SerializeField] bool needRepaireCar = false;

    private RepairSitesControl repairSitesControl;

    private void Start()
    {
        testNumberCounter = 0;
        needRepaireCar = false;

        timePointsForRepairCars.Clear();
        indexesOfRepairSiteToRepairCars.Clear();

        // create vehicles dictionary
        foreach (Vehicle vehicle in vehicles)
        {
            if (!vehiclesOfType.ContainsKey(vehicle.Type))
            {
                vehiclesOfType[vehicle.Type] = new List<Vehicle>();

            }

            vehiclesOfType[vehicle.Type].Add(vehicle);
        }
        updater = new WaitForSeconds(timeDeltsToSpawnCar);
        //StartCoroutine(VehicleSpawner());
    }

    IEnumerator VehicleSpawner()
    {
        //CreateVehicle();
        
        yield return updater;

        if (testNumberCounter <= totalNumber)
        {
            StartCoroutine(VehicleSpawner());
        }
    }

    public void CallForRepairCar(TimePoint repairSiteTimePoint, int repairSiteIndex, RepairSitesControl newRepairSitesControl)
    {
        timePointsForRepairCars.Add(repairSiteTimePoint);
        indexesOfRepairSiteToRepairCars.Add(repairSiteIndex);

        repairSitesControl = newRepairSitesControl;

        needRepaireCar = (timePointsForRepairCars.Count != 0);
    }

    Vehicle GetVehiclePrefabOfType(VehicleUnit.TYPE type)
    {
        if (vehiclesOfType.ContainsKey(type))
        {
            if (vehiclesOfType.Count == 0) return null;

            return vehiclesOfType[type][Random.Range(0, vehiclesOfType[type].Count)];
        }
        else
        {
            return null;
        }
    }

    public void CreateVehicle(VehicleNumberByType startPointAndType)
    {
        //Logging.Log("VehicleCreator: start create vehicle...");

        List<Path> paths = new List<Path>();
        Vehicle newVehicle = null;

        // check for repair car
        if (startPointAndType.type != VehicleUnit.TYPE.emergencyCar && needRepaireCar)
        {
            // create repair car
            paths = wayCreator.CreatePathForVehicleWithMustHavePoints(startPointAndType.number, new List<int> { timePointsForRepairCars[0].RoadStartPointNumber, timePointsForRepairCars[0].RoadEndPointNumber });
            newVehicle = GetVehiclePrefabOfType(VehicleUnit.TYPE.repairCar);

            if (paths != null && newVehicle != null)
            {
                // add params to repair car
                timePointsForRepairCars.RemoveAt(0);
                //indexesOfRepairSiteToRepairCars.RemoveAt(0);

                needRepaireCar = (timePointsForRepairCars.Count != 0);
            }
        }

        if (paths == null || newVehicle == null)
        {
            paths = (startPointAndType.needSpecialEndPoint) ? wayCreator.CreatePathForVehicleWithMustHavePoints(startPointAndType.number, new List<int> { startPointAndType.endPointNumber } ) 
                :wayCreator.CreatePathsForVehicle(startPointAndType.number);

            if (paths == null)
            {
                return;
            }

            // choose vehicle type
            newVehicle = GetVehiclePrefabOfType(startPointAndType.type);
        }

        if (newVehicle == null)
        {
            Logging.Log("VehicleCreator: no vehicle prototype!");
            return;
        }


        // get free instance or create new instance
        int indexOfFreeVehicle = vehicleManager.GetFreeVehicleIndexOfType(newVehicle.Type, newVehicle.VehicleName.GetHashCode());

        if (indexOfFreeVehicle == -1)
        {
            // create new and add to AllVehicles
            indexOfFreeVehicle = vehicleManager.AddNewVehicle(Instantiate(newVehicle.Prefab, vehicleContainer).GetComponent<VehicleUnit>());
        }

        // get and place
        VehicleUnit unit = vehicleManager.AllVehicles[indexOfFreeVehicle];
        unit.SetVehicle(newVehicle, paths, vehicleManager, indexOfFreeVehicle, roadsManager.IsStopInParkingEndPoint(paths[paths.Count - 1].EndPointNumber));

        if (newVehicle.Type == VehicleUnit.TYPE.repairCar)
        {
            unit.GetComponent<RepairCar>().SetRepairCar(indexesOfRepairSiteToRepairCars[0], repairSitesControl);
            indexesOfRepairSiteToRepairCars.RemoveAt(0);

            if (indexesOfRepairSiteToRepairCars.Count != timePointsForRepairCars.Count)
            {
                Logging.Log("WARNING: not the same length for repir car lists!");
            }
        }

        testNumberCounter++;
        // Add vehicle to roadManager
        roadsManager.WhenAddVehicleOnRoad(paths[0].StartPointNumber, newVehicle.Type);
    }
}
