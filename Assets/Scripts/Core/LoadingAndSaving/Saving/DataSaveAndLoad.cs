using IJ.Core.Objects.LevelAndLocation;
using System.Collections.Generic;
using UnityEngine;
using Utilites.Configs;

[DefaultExecutionOrder(-1)]
public class DataSaveAndLoad : MonoBehaviour
{
    [SerializeField] private LevelsAndLocationsManager levelsAndLocationsManager;
    [SerializeField] private GameConfig _config;

    private int _levelIsAvailableConst = 0;
    private int _levelIsNotAvailableConst = -1;

    private int _locationIsAvailableConst = 1;
    private int _LocationIsNotAvailableConst = 0;
    public void SaveData(PlayerState playerState)
    {
        List<int> levelsPoints = new List<int>();
        List<int> locationAvailability = new List<int>();

        foreach(LocationOrLevelProgress levelProgress in playerState.LevelsProgress)
        {
            if (levelProgress.PointsEarned > 0) levelsPoints.Add(levelProgress.PointsEarned);
            else if (!levelProgress.IsAvailable) levelsPoints.Add(_levelIsNotAvailableConst);
            else levelsPoints.Add(_levelIsAvailableConst);
        }

        foreach(LocationProgress locationProgress in playerState.LocationsProgress)
        {
            if (locationProgress.IsAvailable) locationAvailability.Add(_locationIsAvailableConst);
            else locationAvailability.Add(_LocationIsNotAvailableConst);
        }

        SaveSystem.SaveData(levelsPoints.ToArray(), locationAvailability.ToArray(), playerState.TotalPointsNumber);
    }

    public void LoadData(PlayerState playerState)
    {
        if (levelsAndLocationsManager == null) return;

        PlayerData data = SaveSystem.LoadData();

        playerState.PrepairProgressLists(levelsAndLocationsManager.Levels, levelsAndLocationsManager.Locations);
        playerState.ResetPoints();

        if (data == null) HaveNoData(playerState);
        else ParseData(data, playerState);
    }

    void ParseData(PlayerData data, PlayerState playerState)
    {
        ParseLevels(data, playerState);
        ParseLocations(data, playerState);

        playerState.AddPointsToTotalNumber(data.TotalPoints);
        playerState.DataIsLoaded();
    }

    void ParseLevels(PlayerData data, PlayerState playerState)
    {
        for (int i = 0; i < data.LevelPoints.Length; i++)
        {
            Level level = levelsAndLocationsManager.GetLevelByIndex(i);

            if (level == null) return;

            bool isAvailable = (data.LevelPoints[i] > _levelIsNotAvailableConst);
            int points = (data.LevelPoints[i] > 0) ? data.LevelPoints[i] : 0;

            playerState.UpdateLevelDataInLevelsProgressWhenLoading(i, points, isAvailable, level);
        }
    }

    void ParseLocations(PlayerData data, PlayerState playerState)
    {
        for (int i = 0; i < data.LocationAvailability.Length; i++)
        {
            Location location = levelsAndLocationsManager.GetLocationByIndex(i);
            if (location == null) return;

            bool isAvailable = data.LocationAvailability[i] == _locationIsAvailableConst;

            playerState.MakeLocationAvailableOrNot(i, isAvailable);
            playerState.UpdateLocationMaxPointsAndIsMax(i, location);
        }
    }

    void HaveNoData(PlayerState playerState)
    {
        // new game or no data

        // new game
        playerState.AddPointsToTotalNumber(_config.StartGamePoints);
        SaveData(playerState);
        playerState.DataIsLoaded();
    }
}
