using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace IJ.Animations.Helper
{
    public class AnimationHelper : MonoBehaviour
    {
        [SerializeField] private AnimationPath _pathToWrite;
        [SerializeField] private float _duration = 3f;
        [SerializeField] private Ease _easeType;
        [SerializeField] private bool _needWrite = false;

        [Header("Test Animation Script")]
        [SerializeField] private GameObject _testScriptObject;

        public void DoTestScript()
        {
            ITestAnimation testScript = _testScriptObject.GetComponent<ITestAnimation>();
            testScript.TestMethodForHelper(new AnimationSinglePath { Points = GetPoints(), Duration = _duration, EasyIndex = (int)_easeType });
            if (_needWrite) AddToPaths();
        }

        protected virtual Vector3[] GetPoints()
        {
            return null;
        }

        void AddToPaths()
        {
            List<AnimationSinglePath> temp = new List<AnimationSinglePath>();
            if (_pathToWrite.Paths != null) temp.AddRange(_pathToWrite.Paths);
            temp.Add(new AnimationSinglePath { Points = GetPoints(), Duration = _duration, EasyIndex = (int)_easeType });
            _pathToWrite.Paths = temp.ToArray();

        }
    }
}
