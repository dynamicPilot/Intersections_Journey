using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class PlayerState : MonoBehaviour
{
    [SerializeField] private List<LocationProgress> locationsProgress = new List<LocationProgress>();
    public List<LocationProgress> LocationsProgress { get => locationsProgress; }
    [SerializeField] private List<LocationOrLevelProgress> levelsProgress = new List<LocationOrLevelProgress>();
    public List<LocationOrLevelProgress> LevelsProgress { get => levelsProgress; }
    [SerializeField] private int totalPointsNumber;
    public int TotalPointsNumber { get => totalPointsNumber; }
    [SerializeField] private Level currentLevel;

    private bool needLoadData =true;
    public bool NeedLoadData { get => needLoadData; }

    private bool listsIsCreated = true;
    public bool ListsIsCreated { get => listsIsCreated; }

    public delegate void PointsNumberChanged(int points);
    public event PointsNumberChanged OnPointsNumberChanged;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        listsIsCreated = false;
    }

    public void DataIsLoaded(bool needLoad = true)
    {
        needLoadData = needLoad;
    }

    public void ResetPoints()
    {
        totalPointsNumber = 0;
    }

    public void CreateAllLists(Level[] allLevels, Location[] allLocations)
    {
        if (listsIsCreated) return;

        for (int i = 0; i < allLevels.Length; i++)
        {
            levelsProgress.Add(new LocationOrLevelProgress(i, allLevels[i].MaxCrashesNumber));
            if (i < allLocations.Length)
            {
                locationsProgress.Add(new LocationProgress(i, allLocations[i].Levels, allLocations[i].MaxPoints));
            }
        }  
        listsIsCreated = true;
    }

    public bool AddPointsToTotalNumer(int amount)
    {
        if (totalPointsNumber + amount < 0) return false;

        totalPointsNumber += amount;

        if (OnPointsNumberChanged != null) OnPointsNumberChanged.Invoke(totalPointsNumber);

        return true;
    }

    public void MakeLocationAvailableOrNot(int locationIndex, bool isAvailable = true)
    {
        if (locationIndex < locationsProgress.Count)
        {
            Logging.Log("PlayerState: make location available " + locationIndex);
            locationsProgress[locationIndex].IsAvailable = isAvailable;

            foreach(int levelIndex in locationsProgress[locationIndex].LevelsPoints.Keys)
            {
                Logging.Log("PlayerState: make level available " + levelIndex);
                levelsProgress[levelIndex].IsAvailable = isAvailable;
            }
        }
    }

    public bool IsLocationAvailable(int locationIndex)
    {
        if (locationIndex < locationsProgress.Count)
        {
            return locationsProgress[locationIndex].IsAvailable;
        }

        return false;
    }

    public void UpdateLevelDataInLevelsProgressWhenLoading(int levelIndex, int levelPoints, bool levelIsPassed, Level level)
    {
        levelsProgress[levelIndex].MaxPoints = level.MaxCrashesNumber;
        levelsProgress[levelIndex].IsPassed = levelIsPassed;
        levelsProgress[levelIndex].IsAvailable = levelIsPassed;
        levelsProgress[levelIndex].AddPoints(levelPoints, true, true);

        // update location
        UpdateLocationDataInLocationsProgress(level.Location.LocationIndex, levelIndex, levelPoints, false);
    }

    public void UpdateLocationDataInLocationsProgress(int locationIndex, int levelIndex, int levelPoints, bool needUpdateIsMax = true)
    {
        locationsProgress[locationIndex].AddLevelPoints(levelIndex, levelPoints, needUpdateIsMax);
    }

    public void UpdateLocationMaxPointsAndIsMax(int locationIndex, Location location)
    {
        locationsProgress[locationIndex].MaxPoints = location.MaxPoints;
        locationsProgress[locationIndex].UpdateIsMax();
    }

    public void UpdateLocationIsPassed(int locationIndex)
    {
        locationsProgress[locationIndex].IsPassed = true;
        foreach (int levelIndex in locationsProgress[locationIndex].LevelsPoints.Keys)
        {
            if (!levelsProgress[levelIndex].IsPassed) locationsProgress[locationIndex].IsPassed = false;
            return;
        }
    }

    public void SetCurrentLevel(Level newLevel)
    {
        currentLevel = newLevel;
    }

    public Level GetCurrentLevel()
    {
        return currentLevel;
    }
}
