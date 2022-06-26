using UnityEngine;
using IJ.Utilities.Configs;

namespace AudioControls.Radios
{
    //[DefaultExecutionOrder(-2)]
    public class Radio : MonoBehaviour
    {
        [SerializeField] private AudioConfig _config;

        private AudioSource _source;
        float _volumeRate = 0.7f;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _source = GetComponent<AudioSource>();
            //SetVolumeRate(_config.DefaultVolume);
            StartRadio();
        }

        public void SetVolumeRate(float volumeRate)
        {
            //_volumeRate = volumeRate;
            //if (_volumeRate == 0) TurnOnOff(true);
        }

        public void TurnOnOff(bool _isMute)
        {
            _source.mute = _isMute;
        }

        public void StartRadio()
        {
            _source.Play();
        }

        public void PauseUnpauseRadio(bool toPause)
        {
            if (toPause) _source.Pause();
            else _source.UnPause();
        }
    }

}
