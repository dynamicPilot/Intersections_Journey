using UnityEngine;
using DG.Tweening;
using IJ.Animations.Waves;

namespace IJ.Animations
{
    public class CanvasGroupDesolveAndAppearAnimation : TweenAnimation, IAnimationWaveMember
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

        public void OnWaveStart(AnimationPath path)
        {
            Appear();
        }
    }
}
