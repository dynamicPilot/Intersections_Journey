using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class LevelsAndLocationsManager : MonoBehaviour
{
    [SerializeField] private Location[] locations;
    public Location[] Locations { get => locations; }
    [SerializeField] private Level[] levels;
    public Level[] Levels { get => levels; }

    [SerializeField] private bool needUpdateScriptable = false;

    

    private void Awake()
    {
        // onces !!!!!
        if (needUpdateScriptable) FillLocationsAndLevelsIndexes();
    }

    private void FillLocationsAndLevelsIndexes()
    {
        int levelIndex = 0;
        for (int i = 0; i < locations.Length; i++)
        {
            locations[i].LocationIndex = i;
            locations[i].MaxPoints = 0;

            foreach (Level level in locations[i].Levels)
            {
                level.LevelIndex = levelIndex;
                level.Number = levelIndex + 1;
                locations[i].MaxPoints += level.MaxCrashesNumber;
                //level.Location = locations[i];
                levelIndex++;
            }
        }
        //for(int i = 0; i < levels.Count; i++)
        //{
        //    levels[i].LevelIndex = i;
        //    levels[i].Number = i + 1;
        //    if (i < locations.Count)
        //    {
        //        locations[i].LocationIndex = i;

        //        foreach (l)
        //    }
        //}

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
