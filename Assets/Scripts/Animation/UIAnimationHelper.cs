using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Animations.Helper
{
    public interface ITestAnimation
    {
        public abstract void TestMethodForHelper(Vector3[] points);
    }

    public class UIAnimationHelper : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _points;
        [SerializeField] private AnimationPath _pathToWrite;
        [SerializeField] private bool _needWrite = false;

        [Header("Test Animation Script")]
        [SerializeField] private GameObject _testScriptObject;

        private void Start()
        {
            DoTestScript();
        }

        public void DoTestScript()
        {
            ITestAnimation testScript = _testScriptObject.GetComponent<ITestAnimation>();
            testScript.TestMethodForHelper(GetPoints());
            if (_needWrite) AddToPaths();
        }

        void WritePoints()
        {
            _pathToWrite.Points = GetPoints();
        }

        Vector3[] GetPoints()
        {
            Vector3[] result = new Vector3[_points.Length];
            for (int i = 0; i < _points.Length; i++)
            {
                result[i] = _points[i].anchoredPosition;
            }

            return result;
        }

        void AddToPaths()
        {
            List<AnimationSinglePath> temp = new List<AnimationSinglePath>();
            if (_pathToWrite.Paths != null) temp.AddRange(_pathToWrite.Paths);
            temp.Add( new AnimationSinglePath { Points = GetPoints() });
            _pathToWrite.Paths = temp.ToArray();

        }
    }
}
