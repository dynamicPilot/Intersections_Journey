using UnityEngine;

namespace IJ.Core.CameraControls
{
    [RequireComponent(typeof(CameraEffects))]
    public class CameraControl : MonoBehaviour
    {
        public enum MODE { start, inGame, end }

        [Header("Settings")]
        [SerializeField] private float startSize = 5f;
        [SerializeField] private float onStartEffectSizeDelta = 1f;
        [SerializeField] private float effectTotalTime = 1f;
        [SerializeField] private float endSize = 5f;
        [SerializeField] private Vector2 levelStartPoint;

        [Header("Scripts")]
        [SerializeField] private InputControl inputControl;
        [SerializeField] private LevelFlow _levelFlow;

        private CameraEffects _effects;

        MODE mode = MODE.start;

        private void Awake()
        {
            SetCamera();
            _effects = GetComponent<CameraEffects>();
            _effects.OnEffectEnd += OnEffectEnd;
        }

        private void OnDestroy()
        {
            try
            {
                _effects.OnEffectEnd -= OnEffectEnd;
            }
            catch { }
        }

        void SetCamera()
        {
            Camera mainCamera = Camera.main;
            mainCamera.orthographicSize = startSize + onStartEffectSizeDelta;
            mainCamera.transform.position = new Vector3(levelStartPoint.x, levelStartPoint.y, -10f);
        }

        public void OnLevelStart()
        {
            mode = MODE.start;
            _effects.StartCameraEffect(startSize, Vector2.zero, effectTotalTime, false);
        }

        public void OnGameOverEndLevel(Vector2 movingToPoint)
        {
            inputControl.EndInputControl();

            mode = MODE.end;
            Logging.Log("CameraControl: ending effect to point " + movingToPoint);
            _effects.StartCameraEffect(endSize, movingToPoint, effectTotalTime, true);
        }

        void OnEffectEnd()
        {
            if (mode == MODE.start) inputControl.StartInputControl();
            else if (mode == MODE.end)
            {
                _levelFlow.OpenGameOverMenu();
                _effects.OnEffectEnd -= OnEffectEnd;
            }
            
            mode = MODE.inGame;
        }
    }
}

