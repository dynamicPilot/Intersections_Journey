using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocationAndLevelUtilities
{
    private static string subFolder = "Storage";
    private static string levelSaveFileName = "level_{0}.txt";

    public static void FillLocationsAndLevelsIndexes(Location[] locations)
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
    }

    public static void DumpCopyToJson(Level[] items)
    {
        string path = "";
        for (int i = 0; i < items.Length; i++)
        {
            path = Application.persistentDataPath + "/" + subFolder + "/" + string.Format(levelSaveFileName, i);
            ToFromJsonUtility<Level>.DumpJsonToFile(path, items[i]);


        }
    }

    public static void LoadLevelsCopyFromJson(Level[] items, int[] indexes)
    {
        for (int i = 0; i < indexes.Length; i++)
        {
            LoadLevelCopyFromJson(items[indexes[i]], indexes[i]);
        }
    }

    public static void LoadLevelCopyFromJson(Level item, int index)
    {
        string path = Application.persistentDataPath + "/" + subFolder + "/" + string.Format(levelSaveFileName, index);
        Debug.Log("Path to load " + path);
        ToFromJsonUtility<Level>.LoadJsonFromFile(path, item);
    }
}
