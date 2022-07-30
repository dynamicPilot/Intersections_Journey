using DG.Tweening;
using System.Collections;
using IJ.Animations.Helper;
using UnityEngine;
using IJ.Animations.Waves;

namespace IJ.Animations.Objects
{
    
    public class PathMoveRectObjectAnimation : TweenAnimation, ITestAnimation, IAnimationWaveMember
    {
        [SerializeField] private protected RectTransform _transform;
        [SerializeField] private protected AnimationPath _path;
        [SerializeField] private bool _onStart = false;

        [Header("Update Mode")]
        [SerializeField] private protected bool _unscaledTime = false;

        WaitForSeconds _timer;
        WaitForSecondsRealtime _unscaledTimer;
        int _pathIndex;

        private void Start()
        {
            if (_onStart) StartMove();
        }

        public virtual void StartMove()
        {
            _pathIndex = 0;
            StartCoroutine(PathCoroutine(_path.Paths[_pathIndex]));
        }

        IEnumerator PathCoroutine(AnimationSinglePath singlePath, bool singleMove = false)
        {
            if (!_unscaledTime) _timer = new WaitForSeconds(singlePath.Duration);
            else _unscaledTimer = new WaitForSecondsRealtime(singlePath.Duration);

            if (singlePath.Points != null && singlePath.Points.Length > 0) 
                _transform.DOLocalPath(singlePath.Points, singlePath.Duration, PathType.CatmullRom).SetEase((Ease)singlePath.EasyIndex).SetUpdate(_unscaledTime);

            if (!_unscaledTime) yield return _timer;
            else yield return _unscaledTimer;

            _pathIndex++;
            if (_path != null && _pathIndex < _path.Paths.Length && !singleMove) StartCoroutine(PathCoroutine(_path.Paths[_pathIndex]));
            else OnEndMove();
        }

        public virtual void OnEndMove()
        {

        }

        public void TestMethodForHelper(AnimationSinglePath singlePath)
        {
            StartCoroutine(PathCoroutine(singlePath, true));
        }

        public void OnWaveStart(AnimationPath path)
        {
            StopAllCoroutines();
            if (path != null) _path = path;
            StartMove();
        }

        public virtual void OnInitialState()
        {

        }
    }
}
