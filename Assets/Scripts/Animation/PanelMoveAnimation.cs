using DG.Tweening;
using UnityEngine;

namespace IJ.Animations
{
    public class PanelMoveAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private float _initialPosX = -2000f;
        [SerializeField] private float _duration = 1f;

        private void OnDestroy()
        {
            DOTween.KillAll();
        }

        public void MoveIn()
        {
            _transform.DOLocalMoveX(0f, _duration).SetEase(Ease.OutBack);
        }

        public void MoveOut()
        {
            _transform.DOLocalMoveX(_initialPosX, _duration).SetEase(Ease.InBack).OnComplete(() =>
           {
               gameObject.SetActive(false);
           });
        }
    }
}
