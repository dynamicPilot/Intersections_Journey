using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class DataSaveAndLoad : MonoBehaviour
{
    [SerializeField] private LevelsAndLocationsManager levelsAndLocationsManager;

    public void SaveData(PlayerState playerState)
    {
        List<int> levelsPoints = new List<int>();
        List<int> locationAvailability = new List<int>();

        foreach(LocationOrLevelProgress levelProgress in playerState.LevelsProgress)
        {
            if (levelProgress.PointsEarned > 0) levelsPoints.Add(levelProgress.PointsEarned);
            else if (levelProgress.IsPassed) levelsPoints.Add(-1);
            else levelsPoints.Add(0);
        }

        foreach(LocationProgress locationProgress in playerState.LocationsProgress)
        {
            if (locationProgress.IsAvailable) locationAvailability.Add(1);
            else locationAvailability.Add(0);
        }

        //Logging.Log("Data save and load: levels points count " + levelsPoints.Count);
        SaveSystem.SaveData(levelsPoints.ToArray(), locationAvailability.ToArray(), playerState.TotalPointsNumber);
    }

    public void LoadData(PlayerState playerState)
    {
        if (levelsAndLocationsManager == null) return;

        PlayerData data = SaveSystem.LoadData();

        playerState.CreateAllLists(levelsAndLocationsManager.Levels, levelsAndLocationsManager.Locations);
        playerState.ResetPoints();

        if (data == null) HaveNoData(playerState);
        else ParseData(data, playerState);
    }

    void ParseData(PlayerData data, PlayerState playerState)
    {
        Logging.Log("Start Parsing File");
        for (int i = 0; i < data.LevelPoints.Length; i++)
        {
            Level level = levelsAndLocationsManager.GetLevelByIndex(i);
            if (level == null) return;

            if (data.LevelPoints[i] > 0)
            {
                playerState.UpdateLevelDataInLevelsProgressWhenLoading(i, data.LevelPoints[i], true, level);
            }
            else
            {
                playerState.UpdateLevelDataInLevelsProgressWhenLoading(i, 0, (data.LevelPoints[i] == -1), level);
            }
        }

        for (int i = 0; i < data.LocationAvailability.Length; i++)
        {
            Location location = levelsAndLocationsManager.GetLocationByIndex(i);
            if (location == null) return;

            playerState.MakeLocationAvailableOrNot(i, data.LocationAvailability[i] == 1);
            playerState.UpdateLocationMaxPointsAndIsMax(i, location);
            playerState.UpdateLocationIsPassed(i);
        }

        playerState.AddPointsToTotalNumer(data.TotalPoints);
        playerState.DataIsLoaded();
    }

    void HaveNoData(PlayerState playerState)
    {
        // new game or no data

        // new game
        playerState.MakeLocationAvailableOrNot(0);
        SaveData(playerState);
        playerState.DataIsLoaded();
    }
}
