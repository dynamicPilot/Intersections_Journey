using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IJ.Animations.Objects
{
    public class TimeIndicatorAnimation : RectTransformScaleAnimation
    {
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private float _changeSignDuration = 3f;

        private float _fadeDuration = 1f;

        public void StartIndicator()
        {
            _image.DOFillAmount(0, _durationToPump);
            PumpAndBack(0);
        }

        public void ShowMenuSign()
        {
            _group.DOFade(0, _fadeDuration);
            StartCoroutine(ChangeSign());
        }

        IEnumerator ChangeSign()
        {
            yield return new WaitForSeconds(_fadeDuration + _changeSignDuration);
            _group.DOFade(1, _fadeDuration);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            DOTween.KillAll();
        }
    }
}
