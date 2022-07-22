using DG.Tweening;
using IJ.Animations.Waves;
using UnityEngine;
using UnityEngine.UI;

namespace IJ.Animations.Objects
{
    public class ImageChangeColorAnimation : TweenAnimation, IAnimationWaveMember
    {
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _targetColor;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Image _image;

        private void ChangeColor()
        {
            //_image.color = _startColor;
            _image.DOColor(_targetColor, _duration);
        }

        public void OnWaveStart(AnimationPath path)
        {
            ChangeColor();
        }

        public void OnInitialState()
        {
            _image.color = _startColor;
        }
    }
}
