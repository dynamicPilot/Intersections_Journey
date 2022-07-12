using IJ.Core.Objects.LevelAndLocation;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class LevelsAndLocationsManager : MonoBehaviour
{
    [SerializeField] private bool needUpdateScriptable = false;

    [Header("Uploading levels -----")]
    [SerializeField] private bool needUnloadScriptables = false;
    [SerializeField] private int indexToWrite;

    [Header("Loading levels -----")]
    [SerializeField] private bool needLoadLevels = false;
    [SerializeField] private int[] indexesToLoad;

    [SerializeField] private LevelsAndLocationsCollection _collection;

    public Location[] Locations { get => _collection.Locations; }
    public Level[] Levels { get => _collection.Levels; }


    private void Awake()
    {
#if UNITY_EDITOR
        if (needUpdateScriptable) LocationAndLevelUtilities.FillLocationsAndLevelsIndexes(_collection.Locations);
        if (needUnloadScriptables)
        {
            LocationAndLevelUtilities.DumpCopyToJsonByIndex(_collection.Levels, indexToWrite);
        }

        if (needLoadLevels) LocationAndLevelUtilities.LoadLevelsCopyFromJson(_collection.Levels, indexesToLoad);
#endif
    }

    public Level GetLevelByIndex(int index)
    {
        if (index < _collection.Levels.Length) return _collection.Levels[index];
        else return null;
    }

    public Location GetLocationByIndex(int index)
    {
        if (index < _collection.Locations.Length) return _collection.Locations[index];
        else return null;
    }
}
