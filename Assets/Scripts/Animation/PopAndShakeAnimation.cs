using DG.Tweening;
using UnityEngine;

namespace IJ.Animations
{
    public interface ISingleActionAnimation
    {
        public void MakeAction();
    }
    public class PopAndShakeAnimation : TweenAnimation, ISingleActionAnimation
    {
        [SerializeField] private RectTransform _transform;

        [Header("Settings")]
        [SerializeField] private float _targetScale = 1.1f;
        [SerializeField] private float _rotationAmplitude = 30f;
        [SerializeField] private float _popInterval = 0.42f;
        [SerializeField] private float _shakeInterval = 0.84f;

        private float _turnInterval;
        private void Awake()
        {
            _turnInterval = _shakeInterval / 3f;
        }


        public void MakeAction()
        {
            Sequence scaleAndShakeSequence = DOTween.Sequence();

            scaleAndShakeSequence.PrependInterval(1f);
            scaleAndShakeSequence.Append(_transform.DOScale(_targetScale, _popInterval).SetEase(Ease.InOutSine));

            Sequence shakeSequence = DOTween.Sequence();

            shakeSequence.Append(_transform.DORotate(new Vector3(0f, 0f, _rotationAmplitude), _turnInterval).SetEase(Ease.InOutSine));
            shakeSequence.Append(_transform.DORotate(new Vector3(0f, 0f, -2f * _rotationAmplitude), _turnInterval * 2f).SetEase(Ease.InOutSine));
            shakeSequence.Append(_transform.DORotate(new Vector3(0f, 0f, 2f * _rotationAmplitude), _turnInterval * 2f).SetEase(Ease.InOutSine));
            shakeSequence.Append(_transform.DORotate(new Vector3(0f, 0f, 0f), _turnInterval).SetEase(Ease.InOutSine));

            scaleAndShakeSequence.Append(shakeSequence);

            scaleAndShakeSequence.Append(_transform.DOScale(1.0f, _popInterval)).SetEase(Ease.InOutSine);
        }
    }
}


 