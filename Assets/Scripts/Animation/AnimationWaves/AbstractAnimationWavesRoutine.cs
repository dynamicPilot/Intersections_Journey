using System.Collections;
using UnityEngine;

namespace IJ.Animations.Waves
{
    public interface IOnEndWaves
    {
        public void OnEndWaves();
    }

    public abstract class AbstractAnimationWavesRoutine : MonoBehaviour, IAnimationWaveMember
    {
        [SerializeField] private bool _onStart = false;
        [SerializeField] private bool _unscaledTime = false;

        protected AbstractAnimationWave[] _basicWaves;
        protected WaitForSeconds _timer;
        protected WaitForSecondsRealtime _unscaledTimer;
        protected int _waveIndex;
        protected IOnEndWaves _onEndWave;
        private bool _isSet = false;

        private void Start()
        {
            if (_onStart) StartWaves();
        }

        protected abstract void SetBasicWaves();

        public void StartWaves(IOnEndWaves onEndWaves = null)
        {
            _waveIndex = 0;
            _onEndWave = onEndWaves;

            SetWavesAndInititalState();
            StartCoroutine(Wave());
        }

        IEnumerator Wave()
        {
            if (!_unscaledTime) _timer = new WaitForSeconds(_basicWaves[_waveIndex].DelayToNext);
            else _unscaledTimer = new WaitForSecondsRealtime(_basicWaves[_waveIndex].DelayToNext);

            _basicWaves[_waveIndex].StartWave();

            if (!_unscaledTime) yield return _timer;
            else yield return _unscaledTimer;

            _waveIndex++;

            if (_waveIndex < _basicWaves.Length) StartCoroutine(Wave());
            else EndWaves();
        }

        void WavesToInitial()
        {
            for (int i = 0; i < _basicWaves.Length; i++)
            {
                _basicWaves[i].SetInitialState();
            }
        }

        void EndWaves()
        {
            if (_onEndWave != null) _onEndWave.OnEndWaves();
        }

        private void SetWavesAndInititalState()
        {
            if (!_isSet)
            {
                SetBasicWaves();
                WavesToInitial();
                _isSet = true;
            }
        }

        public void OnWaveStart(AnimationPath path = null)
        {
            StartWaves();
        }

        public virtual void OnInitialState()
        {
            SetWavesAndInititalState();
        }
    }
}
