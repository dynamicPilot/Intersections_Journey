using IJ.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IJ.Testing
{
    public class PointsAndWaysTest : MonoBehaviour
    {
        [SerializeField] private List<int> _points;
        [SerializeField] private List<IJ.Utilities.Path> _paths;

        [Header("Testing Settings")]
        [SerializeField] private string _sceneName;
        [SerializeField] private int[] _startPoints;
        [SerializeField] private int[] _endPoints;

        Graph _graph;

        private void Awake()
        {
            MakeTest();
        }

        #region Load

        private static string _storageFolder = "Storage";
        private static string _storageSubFolder = "Paths";
        private string _sceneSubFolder = "From_{0}";
        //private string _scneneSaveFileName = "path_{0}.txt";

        void LoadCopyFromJson()
        {
            string folderPath = Application.persistentDataPath + "/" + _storageFolder + "/" + _storageSubFolder + "/" + string.Format(_sceneSubFolder, _sceneName);
            string[] files = Directory.GetFiles(folderPath);
            for (int i = 0; i < files.Length; i++)
            {
                IJ.Utilities.Path item = new Utilities.Path();
                LoadPathCopyFromJson(files[i], item);
                _paths.Add(item);
            }
        }

        void  LoadPathCopyFromJson(string fileName, IJ.Utilities.Path item)
        {
            string folderPath = fileName;
            //Debug.Log("Path to load " + folderPath);
            ToFromJsonUtility<IJ.Utilities.Path>.LoadJsonFromFile(folderPath, item);
        }

        #endregion


        public void MakeTest()
        {
            GetAllReady();

            Logging.Log(" ---- START GRAPH TESTING ---- ");
            Logging.Log(" scene name " + _sceneName);

            for (int i = 0; i < _startPoints.Length; i++)
            {
                Logging.Log(" \n--- start point number is " + _startPoints[i] + " --- ");
                for (int k = 0; k < _endPoints.Length; k++)
                {
                    List<int> route = SearchForRoute(_startPoints[i], _endPoints[k]);
                    if (route == null || route.Count == 0)
                    {
                        Logging.Log("       -> to " + _endPoints[k] + " ERROR ");
                    }
                    else
                    {
                        Logging.Log("       -> to " + _endPoints[k] + " fine ");
                    }
                }
            }
        }

        void GetAllReady()
        {
            LoadPaths();
            LoadPoints();
            BuildGraph();
        }

        void LoadPaths()
        {
            LoadCopyFromJson();
        }

        private void LoadPoints()
        {
            _points.Clear();

            for (int i = 0; i < _paths.Count; i++)
            {
                if (!_points.Contains(_paths[i].StartPointNumber)) _points.Add(_paths[i].StartPointNumber);
                if (!_points.Contains(_paths[i].EndPointNumber)) _points.Add(_paths[i].EndPointNumber);
            }

            _points.Sort();
        }

        private void BuildGraph()
        {
            _graph = new Graph();

            // set vertexes
            for (int i = 0; i < _points.Count; i++)
            {
                _graph.AddPoint(_points[i], Vector2.zero);
            }

            // add edges
            foreach (IJ.Utilities.Path path in _paths)
            {
                _graph.AddEdgeByPointNumbers(path.StartPointNumber, path.EndPointNumber, path.Weight);
            }
        }

        private List<int> SearchForRoute(int startPoint, int endPoint)
        {
            if (_graph == null) BuildGraph();

            if (startPoint < 0 || endPoint < 0) return null;

            //Logging.Log("WayCreator: start search for road from " + startPoint + " to " + endPoint);
            GraphSearch graphSearch = new GraphSearch();
            return graphSearch.SearchForRoute(_graph, startPoint, endPoint);
        }
    }
}
