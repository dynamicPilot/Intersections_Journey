using DG.Tweening;
using IJ.Animations.Waves;
using UnityEngine;

namespace IJ.Animations.Objects
{
    public class ObjectScaleAnimation : TweenAnimation, IAnimationWaveMember
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _pumpScale;
        [SerializeField] private protected float _durationToPump = 0.7f;
        [SerializeField] private float _durationToBack = 0.25f;

        private protected Vector3 _initialScale = Vector3.one;

        public void PumpAndBack(int loops = -1)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_transform.DOScale(_pumpScale, _durationToPump))
                .Append(_transform.DOScale(_initialScale, _durationToBack))
                .SetLoops(loops);
        }

        public void OnWaveStart(AnimationPath path = null)
        {
            PumpAndBack(0);
        }

        public void OnInitialState()
        {
        }
    }
}
