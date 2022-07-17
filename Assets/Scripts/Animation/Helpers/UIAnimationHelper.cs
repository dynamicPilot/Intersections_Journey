using UnityEngine;

namespace IJ.Animations.Helper
{
    public interface ITestAnimation
    {
        public abstract void TestMethodForHelper(AnimationSinglePath singlePath);
    }

    public class UIAnimationHelper : AnimationHelper
    {
        [SerializeField] private RectTransform[] _points;
        //[SerializeField] private AnimationPath _pathToWrite;
        //[SerializeField] private float _duration = 3f;
        //[SerializeField] private Ease _easeType;
        //[SerializeField] private bool _needWrite = false;

        //[Header("Test Animation Script")]
        //[SerializeField] private GameObject _testScriptObject;

        private void Start()
        {
            DoTestScript();
        }

        //public void DoTestScript()
        //{
        //    ITestAnimation testScript = _testScriptObject.GetComponent<ITestAnimation>();
        //    testScript.TestMethodForHelper(new AnimationSinglePath { Points = GetPoints(), Duration = _duration, EasyIndex = (int)_easeType });
        //    if (_needWrite) AddToPaths();
        //}

        protected override Vector3[] GetPoints()
        {
            Vector3[] result = new Vector3[_points.Length];
            for (int i = 0; i < _points.Length; i++)
            {
                result[i] = _points[i].anchoredPosition;
            }

            return result;
        }

        //void AddToPaths()
        //{
        //    List<AnimationSinglePath> temp = new List<AnimationSinglePath>();
        //    if (_pathToWrite.Paths != null) temp.AddRange(_pathToWrite.Paths);
        //    temp.Add( new AnimationSinglePath { Points = GetPoints(), Duration = _duration, EasyIndex = (int)_easeType });
        //    _pathToWrite.Paths = temp.ToArray();

        //}
    }
}
