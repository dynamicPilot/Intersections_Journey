using DG.Tweening;
using UnityEngine;

namespace IJ.Animations
{
    public class SwingOnWavesAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _amplitudeX;
        [SerializeField] private float _amplitudeY;
        [SerializeField] private float _duration = 0.6f;

        private void Start()
        {
            SwingAnimation();
        }

        void SwingAnimation()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOLocalMove(new Vector3(_amplitudeX, _amplitudeY, _transform.position.z),
                _duration).SetEase(Ease.InOutSine));
            sequence.Append(_transform.DOLocalMove(Vector3.zero,
                _duration).SetEase(Ease.InOutSine));
            sequence.SetLoops(-1);
        }
    }

}
