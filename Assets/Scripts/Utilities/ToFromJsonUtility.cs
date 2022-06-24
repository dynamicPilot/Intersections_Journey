using UnityEngine;
using System.IO;


public static class ToFromJsonUtility<T> where T : class
{
    public static void LoadJsonFromFile(string filename, T classObject)
    {
        //Debug.Log(string.Format("[{0}] Read file {1}", typeof(T).Name, filename));
        string json = File.ReadAllText(filename);
        JsonUtility.FromJsonOverwrite(json, classObject);
    }

    public static void DumpJsonToFile(string filename, T classObj)
    {
        //Debug.Log(string.Format("[{0}] Write file {1}", typeof(T).Name, filename));
        string json = JsonUtility.ToJson(classObj);
        File.WriteAllText(filename, json);
    }

}
