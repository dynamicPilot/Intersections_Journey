using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // read and write json file for player's saves
    private static string subFolder = "Saves";
    private static string mainSaveFileName = "data.txt";

    public static void SaveData(int[] newLevelPoints, int[] newLocationAvailability, int newTotalPoints)
    {
        // check folder
        CheckMainFolderToSave();

        // do back up --> not now
        string path = Application.persistentDataPath + "/" + subFolder + "/" + mainSaveFileName;
        PlayerData data = new PlayerData(newLevelPoints, newLocationAvailability, newTotalPoints);
        using (StreamWriter sw = File.CreateText(path))
        {
            string json = JsonUtility.ToJson(data);
            sw.WriteLine(json);
        }
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/" + subFolder + "/" + mainSaveFileName;

        CheckMainFolderToSave();
        if (File.Exists(path))
        {
            // Read file and create config according to it
            string json = File.ReadAllLines(path)[0];
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            // no data or new game
            return null;
        }
    }

    static void CheckMainFolderToSave()
    {
        bool isExist = Directory.Exists(Application.persistentDataPath + "/" + subFolder);

        if (!isExist)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + subFolder);
        }
    }
}

