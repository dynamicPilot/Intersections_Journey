using System.Collections;
using UnityEngine;

namespace IJ.Animations.Waves
{
    public interface IOnEndWaves
    {
        public void OnEndWaves();
    }

    public class AnimationWavesRoutine : MonoBehaviour
    {
        [SerializeField] private AnimationWave[] _waves;
        [SerializeField] private bool _onStart = false;

        WaitForSeconds _timer;
        int _waveIndex;
        IOnEndWaves _onEndWave;

        private void Start()
        {
            if (_onStart) StartWaves();
        }
        public void StartWaves(IOnEndWaves onEndWaves = null)
        {
            _waveIndex = 0;
            _onEndWave = onEndWaves;
            StartCoroutine(Wave());

        }
        IEnumerator Wave()
        {
            _timer = new WaitForSeconds(_waves[_waveIndex].DelayToNext);
            _waves[_waveIndex].StartWave();
            yield return _timer;

            _waveIndex++;

            if (_waveIndex < _waves.Length) StartCoroutine(Wave());
            else EndWaves();
        }

        void EndWaves()
        {
            if (_onEndWave != null) _onEndWave.OnEndWaves();
        }
    }
}
