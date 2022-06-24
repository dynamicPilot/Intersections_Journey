using DG.Tweening;
using UnityEngine;

namespace IJ.Animations
{
    public class PanelMoveAnimation : TweenAnimation
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private GameObject _panel;
        [SerializeField] private float _initialPosX = -2000f;
        [SerializeField] private float _duration = 1f;

        public void MoveIn()
        {
            _panel.SetActive(true);
            _transform.DOLocalMoveX(0f, _duration).SetEase(Ease.OutBack);
        }

        public void MoveOut()
        {
            _transform.DOLocalMoveX(_initialPosX, _duration).SetEase(Ease.InBack).OnComplete(() =>
           {
               _panel.SetActive(false);
           });
        }
    }
}
