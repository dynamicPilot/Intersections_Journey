using System.Collections;
using UnityEngine;

namespace IJ.Animations.Waves
{
    public interface IOnEndWaves
    {
        public void OnEndWaves();
    }

    public abstract class AbstractAnimationWavesRoutine : MonoBehaviour
    {
        [SerializeField] private bool _onStart = false;

        protected AbstractAnimationWave[] _basicWaves;
        protected WaitForSeconds _timer;
        protected int _waveIndex;
        protected IOnEndWaves _onEndWave;

        private void Start()
        {
            if (_onStart) StartWaves();
        }

        protected abstract void SetBasicWaves();

        public void StartWaves(IOnEndWaves onEndWaves = null)
        {
            _waveIndex = 0;
            _onEndWave = onEndWaves;
            SetBasicWaves();
            WavesToInitial();
            StartCoroutine(Wave());
        }

        IEnumerator Wave()
        {
            _timer = new WaitForSeconds(_basicWaves[_waveIndex].DelayToNext);
            _basicWaves[_waveIndex].StartWave();
            yield return _timer;

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
    }
}
