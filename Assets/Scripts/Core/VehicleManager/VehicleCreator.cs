using RepairSites;
using System.Collections.Generic;
using UnityEngine;

public interface IInstantiatePrefab
{
    public Transform InstantiatePrefab(GameObject prefab);
}

public class VehicleCreator : MonoBehaviour, IInstantiatePrefab
{
    [Header("Units Container")]
    [SerializeField] private Transform container;

    [Header("Units")]
    [SerializeField] private List<Unit> units;

    [Header("Scripts")]
    [SerializeField] private WayCreator wayCreator;
    [SerializeField] private VehicleManager vehicleManager;
    [SerializeField] private RoadsManager roadsManager;
    [SerializeField] private RepairSitesControl repairSitesControl;

    private List<RepairSiteInfo> _repairSiteInfos = new List<RepairSiteInfo>();
    [SerializeField] bool _needRepaireCar = false;

    private VUnitFactory factory;

    private void Awake()
    {
        repairSitesControl.OnNeedRepairCar += CallForRepairCar;
    }

    private void OnDisable()
    {
        try
        {
            repairSitesControl.OnNeedRepairCar -= CallForRepairCar;
        }
        catch
        {

        }
    }

    private void Start()
    {
        _needRepaireCar = false;
        _repairSiteInfos.Clear();
        factory = new VUnitFactory(units);
    }

    public void CallForRepairCar(TimePoint repairSiteTimePoint, int repairSiteIndex)
    {
        Logging.Log("VehicleCreator: need repair car from " + repairSiteTimePoint.RoadStartPointNumber + " to " + repairSiteTimePoint.RoadEndPointNumber);
        _repairSiteInfos.Add(new RepairSiteInfo { Point = repairSiteTimePoint, Index = repairSiteIndex });
        _needRepaireCar = (_repairSiteInfos.Count != 0);
    }

    public Transform InstantiatePrefab(GameObject prefab)
    {
        return Instantiate(prefab, container).GetComponent<Transform>();
    }

    public void CreateVehicle(VehicleNumberByType startPointAndType)
    {
        if (startPointAndType._type != TYPE.emergencyCar && _needRepaireCar)
        {
            CreateRepairCar(startPointAndType);
        }
        else
        {
            CreateUnit(startPointAndType);
        }
    }

    void CreateRepairCar(VehicleNumberByType startPointAndType)
    {
        Logging.Log("VehicleCreator: start creating repair car --------");
        //create repair car
        List<Path> paths = new List<Path>();
        VInfo info = null;

        RepairSiteInfo siteInfo = _repairSiteInfos[0];
        paths = wayCreator.CreatePathForVehicleWithMustHavePoints(startPointAndType.number, new List<int> { siteInfo.Point.RoadStartPointNumber,
            siteInfo.Point.RoadEndPointNumber });

        if (paths == null)
        {
            Logging.Log("-------- wrong route.");
            CreateUnit(startPointAndType);
            return;
        }

        info = factory.SpawnUnitAndGetInfo(this, TYPE.repairCar, paths, vehicleManager.GetStorage(), roadsManager.IsStopInParkingEndPoint(paths[paths.Count - 1].EndPointNumber));

        // set repair car params
        VRepairSiteTagForRepairCar repairCarTag = info.GetComponent<VRepairSiteTagForRepairCar>();
        repairCarTag.SetTargetIndex(siteInfo.Index);

        _repairSiteInfos.RemoveAt(0);
        _needRepaireCar = (_repairSiteInfos.Count != 0);

        AddToRepairSiteControl(repairCarTag);
        AddToRoad(info, paths);
        Logging.Log("-------- VehicleCreator: stop creating repair car ");
    }

    void CreateUnit(VehicleNumberByType startPointAndType)
    {
        List<Path> paths = new List<Path>();
        VInfo info = null;

        paths = (startPointAndType.needSpecialEndPoint) ? wayCreator.CreatePathForVehicleWithMustHavePoints(startPointAndType.number, new List<int> { startPointAndType.endPointNumber })
                  : wayCreator.CreatePathsForVehicle(startPointAndType.number);

        if (paths == null) return;

        info = factory.SpawnUnitAndGetInfo(this, startPointAndType._type, paths, vehicleManager.GetStorage(), roadsManager.IsStopInParkingEndPoint(paths[paths.Count - 1].EndPointNumber));
        AddToRoad(info, paths);
    }


    void AddToRoad(VInfo info, List<Path> paths)
    {
        if (info == null) return;

        vehicleManager.SubscribeUnitInfo(info);
        roadsManager.AddVehicleOnRoad(paths[0].StartPointNumber, info.Type);
    }

    void AddToRepairSiteControl(VRepairSiteTagForRepairCar repairCarTag)
    {
        repairSitesControl.SubscribeUnitInfo(repairCarTag);
    }
}

public struct RepairSiteInfo
{
    public TimePoint Point;
    public int Index;
}
