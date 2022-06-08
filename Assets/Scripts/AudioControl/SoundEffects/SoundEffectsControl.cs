using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilites.Configs;

namespace AudioControls.SoundEffects
{
    [RequireComponent(typeof(EffectsPlayer))]
    public class SoundEffectsControl : MonoBehaviour
    {
        [SerializeField] private GameConfig _config;
        private EffectsPlayer _player;

        private WaitForSeconds timer;

        private void Awake()
        {
            _player = GetComponent<EffectsPlayer>();
            _player.SetVolumeRate(_config.DefaultVolume);
            timer = new WaitForSeconds(_config.EffectsPeriod);
            StartEffects();
        }

        public void StartEffects()
        {
            StartCoroutine(MakeEffect());
        }

        public void StopEffects()
        {
            StopAllCoroutines();
        }

        IEnumerator MakeEffect()
        {
            yield return timer;
            Logging.Log("Play effect!");
            _player.PlaySound(-1);
            StartCoroutine(MakeEffect());

        }
    }
}

