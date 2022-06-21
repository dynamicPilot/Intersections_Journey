using DG.Tweening;
using UnityEngine;

namespace IJ.Animations.Objects
{
    public class CargoShipAnimation : ShipAnimation
    {
        [SerializeField] private float _sailingInTimer = 20f;
        public override void SailIn()
        {
            _transform.position = _initialPosition;
            _transform.DOMove(_portPosition, _sailingInTimer).SetEase(Ease.OutCubic).OnComplete(() => SwingOnWaves());
        }

        protected override void SwingOnWaves()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_transform.DOMove(_portPosition + _swingDelta, _swingTimer).SetEase(Ease.InOutSine));
            sequence.Append(_transform.DOMove(_portPosition, _swingTimer).SetEase(Ease.InOutSine));
            sequence.SetLoops(-1);
        }

        public override void SailOut()
        {
            return;
        }
    }
}
