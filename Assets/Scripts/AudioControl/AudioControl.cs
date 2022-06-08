using AudioControls.Radios;
using AudioControls.SoundPlayers;
using UnityEngine;
using Utilites.Configs;

namespace AudioControls
{
    [RequireComponent(typeof(SoundsPlayer))]
    public class AudioControl : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameConfig _config;

        [Header("Scripts")]
        [SerializeField] private SoundsPlayer soundsPlayer;

        private Radio _radio;
        bool isMute = false;

        private void Awake()
        {
            SetAudio();
        }

        public void SetAudio()
        {
            isMute = false;
            _radio = GameObject.FindGameObjectWithTag("Radio").GetComponent<Radio>();
            soundsPlayer.SetVolumeRate(_config.DefaultVolume);
        }

        public void TurnOnOffSound(bool _isMute, bool musicOnly = false)
        {
            // if music only --> radio control

            // UI sounds
            if (isMute != _isMute)
            {
                isMute = _isMute;
                soundsPlayer.TurnOnOff(isMute);
            }
        }
    }
}

