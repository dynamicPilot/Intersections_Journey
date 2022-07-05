using IJ.Animations.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.UIElements
{
    public class TutorialElement : MonoBehaviour
    {
        [SerializeField] private HandElementAnimation _animation;
        [SerializeField] private Vector3[] _positions;
        [SerializeField] private float _moveDelay = 1.5f;

        WaitForSeconds _timer;
        int _positionIndex = 0;

        private void Awake()
        {
            _timer = new WaitForSeconds(_moveDelay);
            _positionIndex = 0;
        }
        public void StartElement()
        {
            _positionIndex = 0;
            _animation.GetComponent<RectTransform>().localPosition = _positions[_positionIndex];
            _animation.Appear();
        }

        public void MoveToNextPosition()
        {
            if (!gameObject.activeSelf) return;

            _positionIndex++;
            if (_positionIndex > _positions.Length - 1) StopElement();
            else StartCoroutine(DelayAndMove());
        }

        public void StopElement()
        {
            _animation.Desolve(true);
        }

        IEnumerator DelayAndMove()
        {
            _animation.Desolve(true);
            yield return _timer;
            _animation.GetComponent<RectTransform>().localPosition = _positions[_positionIndex];
            _animation.Appear();
        }

    }
}
