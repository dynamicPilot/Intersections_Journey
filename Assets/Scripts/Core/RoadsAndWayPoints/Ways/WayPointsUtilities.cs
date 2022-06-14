using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJ.Utilities;

namespace IJ.Core.Ways
{
    public static class WayPointsUtilities
    {
        private static string _storageFolder = "Storage";
        private static string _storageSubFolder = "Paths";
        private static string _sceneSubFolder = "From_{0}";
        private static string _scneneSaveFileName = "path_{0}.txt";

        public static void DumpCopyToJson(IJ.Utilities.Path[] items)
        {
            string folderPath = "";
            CheckMainFolderToSave(Application.persistentDataPath + "/" + _storageFolder + "/" + _storageSubFolder + "/" +
                    string.Format(_sceneSubFolder, SceneManager.GetActiveScene().name));
            for (int i = 0; i < items.Length; i++)
            {
                folderPath = Application.persistentDataPath + "/" + _storageFolder + "/" + _storageSubFolder + "/" +
                    string.Format(_sceneSubFolder, SceneManager.GetActiveScene().name) + "/"  + string.Format(_scneneSaveFileName, i);
                ToFromJsonUtility<IJ.Utilities.Path>.DumpJsonToFile(folderPath, items[i]);


            }
        }

        public static void LoadCopyFromJson(ref IJ.Utilities.Path[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                LoadPathCopyFromJson(ref items[i]);
            }
        }

        public static void LoadPathCopyFromJson(ref IJ.Utilities.Path item)
        {
            string folderPath = Application.persistentDataPath + "/" + _storageFolder + "/" + _storageSubFolder + "/" + string.Format(_scneneSaveFileName, SceneManager.GetActiveScene().name);
            Debug.Log("Path to load " + folderPath);
            ToFromJsonUtility<IJ.Utilities.Path>.LoadJsonFromFile(folderPath, item);
        }

        static void CheckMainFolderToSave(string pathToFolder)
        {
            bool isExist = Directory.Exists(pathToFolder);

            if (!isExist)
            {
                Directory.CreateDirectory(pathToFolder);
            }
        }
    }
}

