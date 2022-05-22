using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private TimeControl timeControl;
    [SerializeField] private VehicleCreatorControl vehicleCreatorControl;
    [SerializeField] private RoadsManager roadsManager;
    [SerializeField] private GameMaster gameMaster;
    [SerializeField] private RepairSitesControl repairSitesControl;
    [SerializeField] private SceneViewControl sceneViewControl;
    [SerializeField] private RoadPacksControl trafficLightsControl;
    [SerializeField] private WayPoints wayPoints;
    [SerializeField] private StartPointPacksControl startPointPacksControl;

    int maxCrashCounter = 3;
    int pointsEarned = 0;

    List<float> maxAdditionalTimeToWaitByRoad;
    public List<float> MaxAdditionalTimeToWaitByRoad { get => maxAdditionalTimeToWaitByRoad; }
    List<float> maxTimeByRoad;
    public List<float> MaxTimeByRoad { get => maxTimeByRoad; }

    public void SetLevel(Level newLevel)
    {
        maxCrashCounter = newLevel.MaxCrashesNumber;
        
        maxAdditionalTimeToWaitByRoad = newLevel.MaxAdditionalTimeToWaitByRoad;
        maxTimeByRoad = newLevel.MaxTimeByRoad;

        wayPoints.MakeCorrectionAccordingToLevel(newLevel.Corrections);
        if (startPointPacksControl != null && newLevel.StartPointPacksCorrections != null) startPointPacksControl.SetLevel(newLevel.StartPointPacksCorrections);
        else { wayPoints.CalculatePaths(); }

        //wayPoints.MakeStartPointPacksCorrectionsAccordingToLevel(newLevel.StartPointPacksCorrections);
        roadsManager.SetRoads(newLevel.PointNumberPairs, newLevel.EndPointsWithParking);
        roadsManager.SetMaxTimers(newLevel.MaxTimeByRoad, newLevel.MaxAdditionalTimeToWaitByRoad);
        vehicleCreatorControl.SetVehicleTimetable(newLevel.TimeIntervalsByRoads);

        sceneViewControl.SetColors(newLevel.Location);
        trafficLightsControl.SetLevel(newLevel);

        

        if (repairSitesControl != null)
            repairSitesControl.SetRepairSitesTimePoints(newLevel.RepairSiteTimePoints);
    }

    public void StartLevel()
    {
        pointsEarned = maxCrashCounter;
        timeControl.StartLevel();
    }

    public void CheckCrashCounter(int crashCounter, Vector2 crashPosition)
    {
        pointsEarned = maxCrashCounter - crashCounter;
        if (crashCounter > maxCrashCounter)
        {
            pointsEarned = 0;
            Logging.Log("LevelControl: A LOT OF CRASHES! ");
            gameMaster.EndLevel(false, crashPosition.x, crashPosition.y);
        }
    }

    public int GetFinalPointsNumber()
    {
        return pointsEarned;
    }

    public void GameOverBySpecialCar(Vector2 carPosition)
    {
        gameMaster.EndLevel(false, carPosition.x, carPosition.y);
    }
}
