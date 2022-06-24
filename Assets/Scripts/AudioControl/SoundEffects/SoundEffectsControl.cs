using AudioControls.SoundPlayers;
using System.Collections;
using UnityEngine;
using Utilites.Configs;

namespace AudioControls.SoundEffects
{ 
    public class SoundEffectsControl : MonoBehaviour
    {
        [SerializeField] private EnvironmentEffects _effects;
        [SerializeField] private AmbientSoundsPlayer[] _playersForAmbient;
        [SerializeField] private float _effectTimer = 8f;
        private WaitForSeconds _timer;

        private void Awake()
        {
            _timer = new WaitForSeconds(_effectTimer);
            StartEffects();
        }

        public void StartEffects()
        {
            foreach (AmbientSoundsPlayer player in _playersForAmbient) player.PlayAmbient();

            StartCoroutine(MakeEffect());
        }

        public void StopEffects()
        {
            StopAllCoroutines();
        }

        IEnumerator MakeEffect()
        {
            yield return _timer;
            _effects.PlayEffect();
            StartCoroutine(MakeEffect());
        }
    }
}

