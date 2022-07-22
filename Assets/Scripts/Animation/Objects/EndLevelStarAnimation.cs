using DG.Tweening;
using UnityEngine;

namespace IJ.Animations.Objects
{
    public class EndLevelStarAnimation : PathMoveRectObjectAnimation
    {
        [SerializeField] private Vector3 _initialRotation = new Vector3(0f, 0f, -45f);
        [SerializeField] private Vector3 _targetRotation;
        [SerializeField] private Vector3 _endRotation = new Vector3(0f, 0f, -45f);
        [SerializeField] private float _rotationDuration;
        [SerializeField] private Vector3 _targetScale = Vector3.one;
        
        private Vector3 _initialScale = Vector3.zero;
        private float _endEffectDuration = 0.5f;

        public override void OnInitialState()
        {
            _transform.anchoredPosition = _path.GetInitialPosition();
            _transform.localScale = _initialScale;
            _transform.localEulerAngles = _initialRotation;
        }

        public override void StartMove()
        {
            OnInitialState();
            _transform.DOLocalRotate(_targetRotation, _rotationDuration, RotateMode.FastBeyond360);
            _transform.DOScale(_targetScale, _rotationDuration / 2);
            base.StartMove();
        }

        public override void OnEndMove()
        {
            _transform.DOLocalRotate(_endRotation, _endEffectDuration);
            _transform.DOScale(_initialScale, _endEffectDuration);
        }
    }
}
