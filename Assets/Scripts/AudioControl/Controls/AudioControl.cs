using AudioControls.Radios;
using AudioControls.SoundPlayers;
using UnityEngine;
using Utilites.Configs;

namespace IJ.AudioControls.Controls
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

        public virtual void SetAudio()
        {
            isMute = false;
            _radio = GameObject.FindGameObjectWithTag("Radio").GetComponent<Radio>();
        }

        public void TurnOnOffSound(bool _isMute, bool musicOnly = false)
        {
            // UI sounds
            if (isMute != _isMute)
            {
                isMute = _isMute;
                _soundsPlayer.TurnOnOff(isMute);
            }
        }
    }
}

