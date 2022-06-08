using AudioControls.Commons;
using UnityEngine;

namespace AudioControls.SoundPlayers
{
    public class SoundsPlayer : AudioPlayer
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _source;

        public override void TurnOnOff(bool _isMute)
        {
            _source.mute = _isMute;
        }

        public override void PlaySound(int index)
        {
            Sound sound = collection.GetSoundOfIndex(index);
            if (sound != null)
            {
                sound.SetSource(_source, _volumeRate);
                _source.Play();
            }
        }
    }
}

