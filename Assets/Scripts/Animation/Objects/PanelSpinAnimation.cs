using DG.Tweening;
using IJ.Animations.Waves;
using UnityEngine;

namespace IJ.Animations.Objects
{
    public class PanelSpinAnimation : TweenAnimation, IAnimationWaveMember
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private float _duration;
        [SerializeField] private int _rotationNumber = 2;

        [Header("Scale Settings")]
        [SerializeField] private Vector3 _targetScale = new Vector3(0.01f, 0.01f, 0.01f);

        float _singleFlipDutation;

        public void OnWaveStart(AnimationPath path = null)
        {
            Flip();
        }

        void Flip()
        {
            _singleFlipDutation = _duration / (2 * _rotationNumber);

            _transform.DOScale(_targetScale, _duration);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_transform.DORotate(new Vector3(_transform.rotation.eulerAngles.x, 90f, _transform.rotation.eulerAngles.z), _singleFlipDutation).SetEase(Ease.InSine))
                .Append(_transform.DORotate(new Vector3(_transform.rotation.eulerAngles.x, 0, _transform.rotation.eulerAngles.z), _singleFlipDutation).SetEase(Ease.InSine))
                .Append(_transform.DORotate(new Vector3(_transform.rotation.eulerAngles.x, 90f, _transform.rotation.eulerAngles.z), _singleFlipDutation).SetEase(Ease.InSine))
                .Append(_transform.DORotate(new Vector3(_transform.rotation.eulerAngles.x, 0, _transform.rotation.eulerAngles.z), _singleFlipDutation).SetEase(Ease.InSine));

        }

        public void OnInitialState()
        {
            _transform.localScale = Vector3.zero;
        }
    }
}