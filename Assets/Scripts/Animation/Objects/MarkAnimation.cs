using DG.Tweening;
using UnityEngine;

namespace IJ.Animations.Objects
{   
    public class MarkAnimation : TweenAnimation
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Canvas _canvas;

        [Header("Settings: -- durations -- ")]
        [SerializeField] private float _appearanceDuration = 0.5f;

        [Header("Settings: -- scales -- ")]
        [SerializeField] private Vector3 _pumpScale = new Vector3 (1.05f, 1.05f, 1.05f);
        [SerializeField] private Vector3 _crounchScale = new Vector3(0.95f, 0.95f, 0.95f);

        [Header("Settings: -- positions -- ")]
        [SerializeField] private float _jumpDeltaY = 25.2f;

        private Vector3 _initialScale = Vector3.zero;

        public float Appearance()
        {
            _transform.localScale = _initialScale;
            _transform.DOScale(_crounchScale, _appearanceDuration).SetEase(Ease.InOutSine);
            return _appearanceDuration;
        }

        public float Disappearance()
        {
            DOTween.KillAll();
            _transform.DOScale(_initialScale, _appearanceDuration).SetEase(Ease.InOutSine);
            return _appearanceDuration;
        }

        public void InAction()
        {
            Vector2 _correctionToCenteredPosition = _canvas.renderingDisplaySize;
            Vector2 _initialLocal = _transform.anchoredPosition - new Vector2 (_correctionToCenteredPosition.x / 2f, _correctionToCenteredPosition.y / 2f);
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_transform.DOScale(_pumpScale, 1.2f))
                .Append(_transform.DOLocalMoveY(_initialLocal.y + _jumpDeltaY, 0.2f))
                .Append(_transform.DOLocalMoveY(_initialLocal.y, 0.2f))
                .Append(_transform.DOLocalMoveY(_initialLocal.y + _jumpDeltaY, 0.2f))
                .Append(_transform.DOLocalMoveY(_initialLocal.y, 0.2f))
                .Append(_transform.DOScale(_crounchScale, 1.2f))
                .SetLoops(-1);
        }
    }
}
