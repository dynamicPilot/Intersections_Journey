using AudioControls.Commons;
using UnityEngine;

namespace AudioControls.SoundPlayers
{
    public class SoundsPlayer : AudioPlayer
    {
        [Header("Audio Sources")]
        [SerializeField] private protected AudioSource _source;

        public override void TurnOnOff(bool isMute)
        {
            _source.mute = isMute;
        }

        public override void PlaySound(int index)
        {
            Sound sound = collection.GetSoundOfIndex(index);
            if (sound != null)
            {
                sound.SetSource(_source);
                _source.Play();
            }
        }

        public override void StopPlaying()
        {
            _source.Stop();
        }
    }
}

