using AudioControls.SoundPlayers;
using System.Collections;
using UnityEngine;
using Utilites.Configs;

namespace AudioControls.SoundEffects
{
    [RequireComponent(typeof(EffectsPlayer))]
    public class SoundEffectsControl : MonoBehaviour
    {
        [SerializeField] private AudioConfig _config;
        private EffectsPlayer _player;
        private SoundsPlayer _playerForAmbient;
        private WaitForSeconds _timer;

        private void Awake()
        {
            _player = GetComponent<EffectsPlayer>();
            _playerForAmbient = GetComponent<SoundsPlayer>();

            _timer = new WaitForSeconds(_config.EffectsPeriod);
            StartEffects();
        }

        public void StartEffects()
        {
            _playerForAmbient.PlaySound(0);
            StartCoroutine(MakeEffect());
        }

        public void StopEffects()
        {
            StopAllCoroutines();
        }

        IEnumerator MakeEffect()
        {
            yield return _timer;
            Logging.Log("Play effect!");
            _player.PlaySound(-1);
            StartCoroutine(MakeEffect());

        }
    }
}

