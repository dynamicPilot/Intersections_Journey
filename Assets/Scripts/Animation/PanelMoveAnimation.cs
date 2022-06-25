using DG.Tweening;
using UnityEngine;

namespace IJ.Animations
{
    public class PanelMoveAnimation : TweenAnimation
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private GameObject _panel;

        [Header("Move Parameters")]
        [SerializeField] private bool _onX = true;
        [SerializeField] private float _initialPosX = -3000f;
        [SerializeField] private float _duration = 1f;

        public void MoveIn()
        {
            _panel.SetActive(true);
            if (_onX) _transform.DOLocalMoveX(0f, _duration).SetEase(Ease.OutBack);
            else _transform.DOLocalMoveY(0f, _duration).SetEase(Ease.OutBack);
        }

        public void MoveOut()
        {
            if (_onX) MoveOutX();
            else MoveOutY();
        }

        void MoveOutX()
        {
            _transform.DOLocalMoveX(_initialPosX, _duration).SetEase(Ease.InBack).OnComplete(() =>
            {
                _panel.SetActive(false);
            });
        }
        void MoveOutY()
        {
            _transform.DOLocalMoveY(_initialPosX, _duration).SetEase(Ease.InBack).OnComplete(() =>
            {
                _panel.SetActive(false);
            });
        }
    }
}
