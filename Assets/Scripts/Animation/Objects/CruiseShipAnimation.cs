using DG.Tweening;
using UnityEngine;

namespace IJ.Animations.Objects
{
    public class CruiseShipAnimation : ShipAnimation
    {
        [Header("In ---")]
        [SerializeField] private Vector3[] _pointsIn;
        [SerializeField] private float _inTimer = 10f;
        [Header("--- Out")]
        [SerializeField] private Vector3[] _pointsOut;
        [SerializeField] private float _outTimer = 10f;
        [SerializeField] private int _loopsNumber = 10;

        public override void SailIn()
        {
            _transform.position = _initialPosition;
            _transform.DOPath(_pointsIn, _inTimer).SetEase(Ease.OutCubic).OnComplete(() => SwingOnWaves());
        }

        protected override void SwingOnWaves()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_transform.DOMove(_portPosition + _swingDelta, _swingTimer).SetEase(Ease.InOutSine));
            sequence.Append(_transform.DOMove(_portPosition, _swingTimer).SetEase(Ease.InOutSine));
            sequence.SetLoops(_loopsNumber).OnComplete(() => SailOut());
        }

        public override void SailOut()
        {
            _transform.DOPath(_pointsOut, _outTimer).SetEase(Ease.InCubic);
        }
    }
}
