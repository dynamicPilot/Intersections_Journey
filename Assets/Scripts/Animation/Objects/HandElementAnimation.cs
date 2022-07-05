using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using IJ.Utilities;

namespace IJ.Animations.Objects
{
    public class HandElementAnimation : TweenAnimation
    {
        [Header("Sprites")]
        [SerializeField] private SpriteCollection _sprites;

        [Header("Pumping")]
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Vector3 _pumpScale;
        [SerializeField] private float _duration;

        [Header("Fading")]
        [SerializeField] private Image _image;
        [SerializeField] private float _fadeDuration;

        private Vector3 _initialScale = Vector3.one;

        public void Desolve(bool needUnactive = false)
        {
            StopPumping();
            _image.DOFade(0, _fadeDuration).OnComplete(() => gameObject.SetActive(!needUnactive));
        }

        public void Appear()
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
            _image.DOFade(1, _fadeDuration).OnComplete(() => Pumping());
        }

        public void StopPumping()
        {
            DOTween.CompleteAll();
        }

        //public void StartPumping()
        //{
        //    if (!gameObject.activeSelf) gameObject.SetActive(true);
        //    Pumping();
        //}

        void Pumping()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_transform.DOScale(_pumpScale, _duration / 2).OnComplete(() => { _image.sprite = _sprites.Collection[1]; }))
                .Append(_transform.DOScale(_initialScale, _duration / 2).OnComplete(() => { _image.sprite = _sprites.Collection[0]; }))
                .SetLoops(-1);
        }
    }
}
