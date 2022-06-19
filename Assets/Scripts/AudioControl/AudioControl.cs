using AudioControls.Radios;
using AudioControls.SoundPlayers;
using UnityEngine;
using Utilites.Configs;

namespace AudioControls
{
    [RequireComponent(typeof(SoundsPlayer))]
    [RequireComponent(typeof(SnapshotTransition))]
    public class AudioControl : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private AudioConfig _config;
        
        private SoundsPlayer _soundsPlayer;
        private Radio _radio;

        bool isMute = false;

        private void Awake()
        {
            _soundsPlayer = GetComponent<SoundsPlayer>();
            SetAudio();
        }

        public void SetAudio()
        {
            isMute = false;
            _radio = GameObject.FindGameObjectWithTag("Radio").GetComponent<Radio>();
            //_soundsPlayer.SetVolumeRate(_config.DefaultVolume);
        }

        public void TurnOnOffSound(bool _isMute, bool musicOnly = false)
        {
            // if music only --> radio control

            // UI sounds
            if (isMute != _isMute)
            {
                isMute = _isMute;
                _soundsPlayer.TurnOnOff(isMute);
            }
        }
    }
}

