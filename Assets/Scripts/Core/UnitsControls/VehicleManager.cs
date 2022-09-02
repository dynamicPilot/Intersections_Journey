using IJ.Core.Objects.CrashEffects;
using System.Collections.Generic;
using UnityEngine;

public interface IIsEndPointParking
{
    public bool IsEndPointParking(int endPointNumber);
}

[RequireComponent(typeof (VEndMover))]
public class VehicleManager : MonoBehaviour, IIsEndPointParking
{
    [Header("Scripts")]
    [SerializeField] private RoadsManager roadsManager;
    [SerializeField] private MenusUIControl menusUIControl;
    [SerializeField] private CrashEffects crashEffects;

    private List<VInfo> subscriptions = new List<VInfo>();

    public delegate void NewCrash(int counter, Vector3 point);
    public event NewCrash OnNewCrash;

    public delegate void GameOver(Vector3 position, bool endByTimer);
    public event GameOver OnGameOver;

    private VStorage storage;
    private VCrashes crashes;
    private VEndMover _endMover;

    private int crashCounter = 0;

    private void Awake()
    {
        storage = new VStorage();
        crashes = new VCrashes();
        _endMover = GetComponent<VEndMover>();
    }

    public VStorage GetStorage()
    {
        return storage;
    }

    private void OnDestroy()
    {
        foreach(VInfo info in subscriptions)
        {
            info.OnFreeUnit -= FreeUnit;
            info.OnStartCrashWithUnit -= UnitInCrash;
            info.OnGameOver -= GameOverState;
        }
    }

    public void SubscribeUnitInfo(VInfo info)
    {
        info.OnFreeUnit += FreeUnit;
        info.OnStartCrashWithUnit += UnitInCrash;
        info.OnGameOver += GameOverState;
        subscriptions.Add(info);
    }

    public void UnsubscribeUnitInfo(VInfo info)
    {
        info.OnFreeUnit -= FreeUnit;
        info.OnStartCrashWithUnit -= UnitInCrash;
        info.OnGameOver -= GameOverState;
        subscriptions.Remove(info);
    }

    void FreeUnit(int unitIndex, int endPointNumber, VInfo info)
    {
        KeyValuePair<TYPE, IDirectionShearer> unitRoadInfo = storage.GetUnitTypeAndDirection(unitIndex);
        crashes.RemoveCrashByVehicleIndex(unitIndex);
        UnsubscribeUnitInfo(info);
        RemoveFromRoad(unitRoadInfo.Value, unitRoadInfo.Key);
        roadsManager.FreeParking(endPointNumber);

        _endMover.AddUnitToMove(unitIndex, info);
    }

    void UnitInCrash(int selfIndex, int otherIndex, Vector3 contactPoint)
    {
        if (crashes.RegisterCrash(selfIndex, otherIndex))
        {
            crashCounter++;
            menusUIControl.UpdateCrashCounter(crashCounter, true);
            if (OnNewCrash != null) OnNewCrash.Invoke(crashCounter, contactPoint);
        }

        crashEffects.StartCrashEffect(contactPoint);        
    }

    public void SetUnitAsFree(int unitIndex)
    {
        storage.FreeUnit(unitIndex);        
    }

    void RemoveFromRoad(IDirectionShearer unitDirection, TYPE type)
    {
        if (unitDirection == null)
        {
            return;
        }

        roadsManager.RemoveVehicleFromRoad(unitDirection.GetRoadStartPoint(), unitDirection.GetTotalTimeOnRoad(), type);
    }

    public bool IsEndPointParking(int endPointNumber)
    {
        return roadsManager.IsStopInParkingEndPoint(endPointNumber);
    }

    public void GameOverState(Vector3 position)
    {
        Logging.Log("Game over in manager");
        if (OnGameOver != null) OnGameOver.Invoke(position, false);
    }
}