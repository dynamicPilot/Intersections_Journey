using DG.Tweening;
using System.Collections;
using IJ.Animations.Helper;
using UnityEngine;
using IJ.Animations.Waves;

namespace IJ.Animations.Objects
{
    
    public class PathMoveRectObjectAnimation : TweenAnimation, ITestAnimation, IAnimationWaveMember
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private AnimationPath _path;
        [SerializeField] private bool _onStart = false;

        WaitForSeconds _timer;
        int _pathIndex;

        private void Start()
        {
            if (_onStart) StartMove();
        }

        public void StartMove()
        {
            _pathIndex = 0;
            StartCoroutine(PathCoroutine(_path.Paths[_pathIndex]));
        }

        IEnumerator PathCoroutine(AnimationSinglePath singlePath, bool singleMove = false)
        {
            _timer = new WaitForSeconds(singlePath.Duration);

            if (singlePath.Points != null && singlePath.Points.Length > 0) 
                _transform.DOLocalPath(singlePath.Points, singlePath.Duration, PathType.CatmullRom).SetEase((Ease)singlePath.EasyIndex);

            yield return _timer;

            _pathIndex++;
            if (_path != null && _pathIndex < _path.Paths.Length && !singleMove) StartCoroutine(PathCoroutine(_path.Paths[_pathIndex]));
        }

        public void TestMethodForHelper(AnimationSinglePath singlePath)
        {
            StartCoroutine(PathCoroutine(singlePath, true));
        }

        public void OnWaveStart(AnimationPath path)
        {
            StopAllCoroutines();
            _path = path;
            StartMove();
        }

        public void OnInitialState()
        {
        }
    }
}
