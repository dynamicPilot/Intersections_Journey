using DG.Tweening;
using UnityEngine;

namespace IJ.Animations
{
    public class PanelsFlipAnimation : TweenAnimation
    {
        private enum VIEW { first, second }
        [SerializeField] private float _SingleFlipDuration = 0.5f;
        [SerializeField] private RectTransform _firstTransform;
        [SerializeField] private RectTransform _secondTransform;
        VIEW _activeView = VIEW.first;
        public void DoFlip()
        {
            if (_activeView == VIEW.first) FlipToSecond();
            else if (_activeView == VIEW.second) FlipToFirst();
        }

        void FlipToSecond()
        {
            Flip(_firstTransform, _secondTransform);
            _activeView = VIEW.second;
        }

        void FlipToFirst()
        {
            Flip(_secondTransform, _firstTransform);
            _activeView = VIEW.first;
        }

        void Flip(RectTransform fromTransform, RectTransform toTransform)
        {
            toTransform.rotation = Quaternion.Euler(fromTransform.rotation.eulerAngles.x, 90f, fromTransform.rotation.eulerAngles.z);

            var tween = fromTransform.DORotate(new Vector3(fromTransform.rotation.eulerAngles.x, 90f, fromTransform.rotation.eulerAngles.z), _SingleFlipDuration).SetEase(Ease.InSine).OnComplete(() =>
            {
                fromTransform.gameObject.SetActive(false);
                toTransform.gameObject.SetActive(true);
                toTransform.DORotate(new Vector3(fromTransform.rotation.eulerAngles.x, 0f, fromTransform.rotation.eulerAngles.z), _SingleFlipDuration).SetEase(Ease.OutSine);
            }
            );
        }
    }
}
