using UnityEngine;
using RepairSites;
using IJ.Core.Ways;
using IJ.Core.GameTime;

public class LevelControl : SceneControl
{
    [Header("Components")]
    [SerializeField] private TimeControl timeControl;
    [SerializeField] private VehicleCreatorControl vehicleCreatorControl;
    [SerializeField] private RoadsManager roadsManager;
    [SerializeField] private LevelFlow _levelFlow;
    [SerializeField] private RepairSitesControl repairSitesControl;
    [SerializeField] private SceneViewControl sceneViewControl;
    [SerializeField] private RoadPacksControl trafficLightsControl;
    [SerializeField] private WayPoints wayPoints;
    [SerializeField] private StartPointPacksControl startPointPacksControl;
    [SerializeField] private VehicleManager vehicleManager;

    int _maxCrashCounter = 3;
    int _pointsEarned = 0;

    private void OnDestroy()
    {
        try
        {
            roadsManager.OnGameOver -= LevelGameOver;
            vehicleManager.OnGameOver -= LevelGameOver;
            vehicleManager.OnNewCrash -= CheckCrashCounter;
            timeControl.OnGameOver -= LevelGameOver;
        }
        catch { };
    }

    public void SetLevel(Level newLevel)
    {
        _maxCrashCounter = newLevel.MaxCrashesNumber;

        wayPoints.MakeCorrectionAccordingToLevel(newLevel.Corrections);
        if (startPointPacksControl != null && newLevel.StartPointPacksCorrections != null) startPointPacksControl.SetLevel(newLevel.StartPointPacksCorrections);
        else { wayPoints.CalculatePaths(); }

        //wayPoints.MakeStartPointPacksCorrectionsAccordingToLevel(newLevel.StartPointPacksCorrections);
        roadsManager.SetRoads(newLevel.PointNumberPairs, newLevel.EndPointsWithParking);
        roadsManager.SetMaxTimers(newLevel.MaxTimeByRoad, newLevel.MaxAdditionalTimeToWaitByRoad, newLevel.PointNumberPairs.Length);
        vehicleCreatorControl.SetVehicleTimetable(newLevel.TimeIntervalsByRoads);

        sceneViewControl.SetColors(newLevel.Location);
        trafficLightsControl.SetLevel(newLevel);       

        if (repairSitesControl != null)
            repairSitesControl.SetRepairSitesTimePoints(newLevel.RepairSiteTimePoints);

        roadsManager.OnGameOver += LevelGameOver;
        vehicleManager.OnGameOver += LevelGameOver;
        vehicleManager.OnNewCrash += CheckCrashCounter;
        timeControl.OnGameOver += LevelGameOver;
    }

    public void StartLevel()
    {
        _pointsEarned = _maxCrashCounter;
        timeControl.StartLevel();
    }

    public void CheckCrashCounter(int crashCounter, Vector3 crashPosition)
    {
        _pointsEarned = _maxCrashCounter - crashCounter;

        if (crashCounter > _maxCrashCounter)
        {
            _pointsEarned = 0;
            vehicleManager.OnNewCrash -= CheckCrashCounter;
            LevelGameOver(crashPosition);
        }
    }

    public void LevelGameOver(Vector3 position, bool endByTimer = false)
    {
        Logging.Log("Game over in level control --- position " + position);
        _levelFlow.EndLevel(endByTimer, position.x, position.y);
    }

    public int GetFinalPointsNumber()
    {
        return _pointsEarned;
    }
}
