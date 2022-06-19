using DG.Tweening;
using UnityEngine;

namespace IJ.Animations.Objects
{
    public class RectObjectRotatingAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private float _duration;
        [SerializeField] private float _angle;

        private void OnEnable()
        {
            Rotating();
        }

        private void OnDisable()
        {
            DOTween.KillAll();
        }

        void Rotating()
        {
            float oneMoveDuration = _duration / 4f;

            Sequence rotationSeq = DOTween.Sequence();

            rotationSeq.Append(_transform.DORotate(new Vector3(_transform.rotation.eulerAngles.x, _transform.rotation.eulerAngles.y, -1 * _angle), oneMoveDuration).SetEase(Ease.OutSine))
                .Append(_transform.DORotate(new Vector3(_transform.rotation.eulerAngles.x, _transform.rotation.eulerAngles.y, 2f * _angle), oneMoveDuration * 2).SetEase(Ease.InOutSine))
                .Append(_transform.DORotate(new Vector3(_transform.rotation.eulerAngles.x, _transform.rotation.eulerAngles.y, 0f), oneMoveDuration).SetEase(Ease.InSine)).SetLoops(-1);
        }

    }
}
