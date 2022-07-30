using UnityEngine;
using DG.Tweening;
using IJ.Animations.Waves;

namespace IJ.Animations
{
    public class CanvasGroupDesolveAndAppearAnimation : TweenAnimation, IAnimationWaveMember
    {
        [SerializeField] private CanvasGroup _group;

        [Header("Settings")]
        [SerializeField] private bool _appearOnWaveStart = true;
        [SerializeField] private float _duration = 0.5f;

        [Header("Update Mode")]
        [SerializeField] private bool _unscaledTime = false;

        public void Appear()
        {
            _group.alpha = 0;
            _group.DOFade(1, _duration).SetUpdate(_unscaledTime);
        }

        public void Desolve(bool disable)
        {
            _group.DOFade(0, _duration).SetUpdate(_unscaledTime).OnComplete(() =>
            {
                if (disable) gameObject.SetActive(false);
            });
        }

        public void OnInitialState()
        {
        }

        public void OnWaveStart(AnimationPath path)
        {
            if (_appearOnWaveStart) Appear();
            else Desolve(true);
        }
    }
}
