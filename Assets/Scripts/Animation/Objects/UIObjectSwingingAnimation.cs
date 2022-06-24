using DG.Tweening;
using UnityEngine;

namespace IJ.Animations.Objects
{
    /// <summary>
    /// RectTransform animation: swing around start position with delta to both sides.
    /// Loops: -1 -> endless.
    /// </summary>
    public class UIObjectSwingingAnimation : TweenAnimation
    {
        [SerializeField] private RectTransform _transform;       
        [SerializeField] private Vector2 _correctionToCenteredPosition;
        [Header("Animation Settings")]
        [SerializeField] private int _loops = -1;
        [SerializeField] private bool _playOnAwake = false;
        [SerializeField] private Vector2 _delta;
        [SerializeField] private float _duration = 1f;

        private void Awake()
        {
            if (_playOnAwake) SwingAnimation();
        }

        void SwingAnimation()
        {
            //Vector2 _correctionToCenteredPosition = _canvas.renderingDisplaySize;
            Vector2 _initialLocal = _transform.anchoredPosition - _correctionToCenteredPosition;
            float _singleMoveDuration =  _duration / 4f;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(_transform.DOLocalMove(new Vector3(_initialLocal.x - _delta.x, _initialLocal.y - _delta.y, _transform.position.z), _singleMoveDuration).SetEase(Ease.OutSine))
                .Append(_transform.DOLocalMove(new Vector3(_initialLocal.x + 2 * _delta.x, _initialLocal.y + 2 * _delta.y, _transform.position.z), _singleMoveDuration * 2f).SetEase(Ease.InOutSine))
                .Append(_transform.DOLocalMove(new Vector3(_initialLocal.x, _initialLocal.y, _transform.position.z), _singleMoveDuration).SetEase(Ease.InSine))
                .SetLoops(_loops);
        }
    }
}
