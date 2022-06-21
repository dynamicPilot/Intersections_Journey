using DG.Tweening;
using UnityEngine;

namespace IJ.Animations.Objects
{
    public class PlaneAnimation : TweenAnimation
    {
        [SerializeField] private Transform _transform;

        [Header("Inititals")]
        [SerializeField] private Vector3 _initialPosition = new Vector3(36.05f, -6.15f, 0f);
        [SerializeField] private Vector3 _inititalRotZ = new Vector3(0f, 0f, 90f);

        [Header("In Gate")]
        [SerializeField] private Vector3 _gatePosition = new Vector3(26.29f, -1.97f, 0f);
        [SerializeField] private Vector3 _gateRotZ = new Vector3(0f, 0f, 46.7f);
        [SerializeField] private float _duration = 6.3f;

        private void Awake()
        {
            ToStand();
        }
        public void ToStand()
        {
            _transform.SetPositionAndRotation(_initialPosition, Quaternion.Euler(_inititalRotZ));
            _transform.DOMove(_gatePosition, _duration).SetEase(Ease.OutCirc);
            _transform.DORotate(_gateRotZ, _duration).SetEase(Ease.OutCubic);
        }

        public void FromStand()
        {
            _transform.DOMove(_initialPosition, _duration).SetEase(Ease.InCirc);
            _transform.DORotate(_inititalRotZ, _duration).SetEase(Ease.InCubic);
        }
    }
}
