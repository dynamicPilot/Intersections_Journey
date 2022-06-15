using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Core.CameraControls
{
    public class CameraEffects : MonoBehaviour
    {
        Camera _mainCamera;

        Vector2 _targetPoint = Vector2.zero;
        Vector2 _startPoint = Vector2.zero;

        float _startSize;
        float _targetSize;

        float _totalEffectTimePassed = 0f;
        float _duration = 1f;

        bool _pauseUpdate = true;
        bool _needMove = false;

        public delegate void EffectEnd();
        public event EffectEnd OnEffectEnd;


        private void Awake()
        {
            _mainCamera = Camera.main;
            _pauseUpdate = true;
        }

        private void Update()
        {
            if (_pauseUpdate) return;

            _totalEffectTimePassed += Time.deltaTime;
            MakeEffectStep();
        }

        public void StartCameraEffect(float targetSize, Vector2 targetPoint, float duration, bool needMove)
        {
            _targetSize = targetSize;
            _startSize = _mainCamera.orthographicSize;
            _needMove = needMove;

            if (needMove)
            {
                _targetPoint = targetPoint;
                _startPoint = _mainCamera.transform.position;
            }

            _duration = duration;
            _totalEffectTimePassed = 0f;

            _pauseUpdate = false;

        }

        void MakeEffectStep()
        {
            float t = _totalEffectTimePassed / _duration;

            if (t > 1) t = 1f;

            ChangeCameraSize(t);
            if (_needMove) ChangeCameraPosition(t);

            if (t == 1) EffectIsEnded();
        }

        void ChangeCameraSize(float t)
        {
            _mainCamera.orthographicSize = Mathf.Lerp(_startSize, _targetSize, t);
        }

        void ChangeCameraPosition(float t)
        {
            Vector2 position = Vector2.Lerp(_startPoint, _targetPoint, t);
            _mainCamera.transform.position = new Vector3(position.x, position.y, _mainCamera.transform.position.z);
        }


        void EffectIsEnded()
        {
            _pauseUpdate = true;
            if (OnEffectEnd != null) OnEffectEnd.Invoke();
        }
    }

}
