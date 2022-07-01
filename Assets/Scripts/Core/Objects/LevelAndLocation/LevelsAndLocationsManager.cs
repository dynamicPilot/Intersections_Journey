using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class LevelsAndLocationsManager : MonoBehaviour
{
    [SerializeField] private bool needUpdateScriptable = false;
    [SerializeField] private bool needUnloadScriptables = false;

    [Header("Loading levels -----")]
    [SerializeField] private bool needLoadLevels = false;
    [SerializeField] private int[] indexesToLoad;

    [SerializeField] private Location[] locations;
    public Location[] Locations { get => locations; }
    [SerializeField] private Level[] levels;
    public Level[] Levels { get => levels; }


    private void Awake()
    {

        if (needUpdateScriptable) LocationAndLevelUtilities.FillLocationsAndLevelsIndexes(locations);
        if (needUnloadScriptables) LocationAndLevelUtilities.DumpCopyToJson(levels);

        if (needLoadLevels) LocationAndLevelUtilities.LoadLevelsCopyFromJson(levels, indexesToLoad);
    }

    public Level GetLevelByIndex(int index)
    {
        if (index < levels.Length) return levels[index];
        else return null;
    }

    public Location GetLocationByIndex(int index)
    {
        if (index < locations.Length) return locations[index];
        else return null;
    }
}
