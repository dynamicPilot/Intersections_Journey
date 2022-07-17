using DG.Tweening;
using IJ.Animations.Waves;
using UnityEngine;

namespace IJ.Animations.Objects
{
    public class RenderersFadeAnimation : TweenAnimation, IAnimationWaveMember
    {
        [SerializeField] private SpriteRenderer[] _renderers;
        [SerializeField] private float _duration = 2f;

        void HideAll()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].color = new Color(_renderers[i].color.r, _renderers[i].color.g, _renderers[i].color.b, 0f);
            }
        }

        public void StartFadeIn()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].DOFade(1f, _duration);
            }
        }

        public void OnWaveStart(AnimationPath path)
        {
            StartFadeIn();
        }

        public void OnInitialState()
        {
            HideAll();
        }
    }
}
