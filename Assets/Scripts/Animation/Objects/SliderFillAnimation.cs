using DG.Tweening;
using IJ.Animations.Waves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IJ.Animations.Objects
{
    public class SliderFillAnimation : TweenAnimation, IAnimationWaveMember
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _stepDuration = 1f;
        [SerializeField] private float _fillStep = 0.33f;

        private float _inittialValue = 0f;

        public void OnInitialState()
        {
            _slider.value = _inittialValue;
        }

        public void OnWaveStart(AnimationPath path = null)
        {
            _slider.DOValue(_slider.value + _fillStep, _stepDuration);
        }
    }
}
