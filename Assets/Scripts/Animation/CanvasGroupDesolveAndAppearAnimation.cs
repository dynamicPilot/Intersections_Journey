using UnityEngine;
using DG.Tweening;

namespace IJ.Animations
{
    public class CanvasGroupDesolveAndAppearAnimation : TweenAnimation
    {
        [SerializeField] private CanvasGroup _group;

        [Header("Settings")]
        [SerializeField] private float _duration = 0.5f;

        public void Appear()
        {
            _group.alpha = 0;
            _group.DOFade(1, _duration);
        }

        public void Desolve(bool disable)
        {
            _group.DOFade(0, _duration).OnComplete(() =>
            {
                if (disable) gameObject.SetActive(false);
            });
        }
    }
}