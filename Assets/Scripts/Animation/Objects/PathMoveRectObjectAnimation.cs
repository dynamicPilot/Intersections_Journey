using DG.Tweening;
using IJ.Animations.Helper;
using UnityEngine;

namespace IJ.Animations.Objects
{
    
    public class PathMoveRectObjectAnimation : TweenAnimation, ITestAnimation
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private AnimationPath _path;
        [SerializeField] private float _duration;
        [SerializeField] private bool _effectOnIn = false;
        [SerializeField] private bool _effectOnOut = true;

        public void MoveViaPath(Vector3[] points)
        {
            if (_effectOnOut) _transform.DOLocalPath(points, _duration, PathType.CatmullRom).SetEase(Ease.OutSine);
        }

        public void TestMethodForHelper(Vector3[] points)
        {
            MoveViaPath(points);
        }
    }
}
