using IJ.Core.Objects.LevelAndLocation;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class PlayerState : MonoBehaviour
{
    [SerializeField] private List<LocationProgress> _locationsProgress = new List<LocationProgress>();    
    [SerializeField] private List<LocationOrLevelProgress> _levelsProgress = new List<LocationOrLevelProgress>();   
    [SerializeField] private int _totalPointsNumber;   
    [SerializeField] private Level currentLevel;

    private bool _needLoadData =true;
    private bool _listsIsCreated = true;   

    #region PublicVariables
    public List<LocationProgress> LocationsProgress { get => _locationsProgress; }
    public List<LocationOrLevelProgress> LevelsProgress { get => _levelsProgress; }
    public int TotalPointsNumber { get => _totalPointsNumber; }
    public bool NeedLoadData { get => _needLoadData; }
    public bool ListsIsCreated { get => _listsIsCreated; }

    #endregion

    public delegate void PointsNumberChanged(int points);
    public event PointsNumberChanged OnPointsNumberChanged;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _listsIsCreated = false;
    }

    public void DataIsLoaded(bool needLoad = true)
    {
        _needLoadData = needLoad;
    }

    public void ResetPoints()
    {
        _totalPointsNumber = 0;
    }

    public void PrepairProgressLists(Level[] allLevels, Location[] allLocations)
    {
        if (_listsIsCreated) return;

        for (int i = 0; i < allLevels.Length; i++)
        {
            _levelsProgress.Add(new LocationOrLevelProgress(i, allLevels[i].MaxCrashesNumber));
        }
        
        for (int i = 0; i < allLocations.Length; i++)
        {
            _locationsProgress.Add(new LocationProgress(i, allLocations[i].Levels, allLocations[i].MaxPoints));
        }

        _listsIsCreated = true;
    }

    public bool AddPointsToTotalNumber(int amount)
    {
        if (_totalPointsNumber + amount < 0) return false;

        _totalPointsNumber += amount;

        if (OnPointsNumberChanged != null) OnPointsNumberChanged.Invoke(_totalPointsNumber);

        return true;
    }

    public void MakeLocationAvailableOrNot(int locationIndex, bool isAvailable = true)
    {
        if (locationIndex < _locationsProgress.Count)
        { 
            _locationsProgress[locationIndex].IsAvailable = isAvailable;

            foreach(int levelIndex in _locationsProgress[locationIndex].LevelsPoints.Keys)
            {
                _levelsProgress[levelIndex].IsAvailable = isAvailable;
            }
        }
    }

    public bool IsLocationAvailable(int locationIndex)
    {
        if (locationIndex < _locationsProgress.Count)
        {
            return _locationsProgress[locationIndex].IsAvailable;
        }

        return false;
    }

    public void UpdateLevelDataInLevelsProgressWhenLoading(int levelIndex, int levelPoints, bool isAvailable, Level level)
    {
        _levelsProgress[levelIndex].MaxPoints = level.MaxCrashesNumber;
        _levelsProgress[levelIndex].IsAvailable = isAvailable;
        _levelsProgress[levelIndex].AddPoints(levelPoints, true, true);

        // update location
        UpdateLocationDataInLocationsProgress(level.Location.LocationIndex, levelIndex, levelPoints, false);
    }

    public void UpdateLocationDataInLocationsProgress(int locationIndex, int levelIndex, int levelPoints, bool needUpdateIsMax = true)
    {
        _locationsProgress[locationIndex].AddLevelPoints(levelIndex, levelPoints, needUpdateIsMax);
    }

    public void UpdateLocationMaxPointsAndIsMax(int locationIndex, Location location)
    {
        _locationsProgress[locationIndex].MaxPoints = location.MaxPoints;
        _locationsProgress[locationIndex].UpdateIsMax();
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
