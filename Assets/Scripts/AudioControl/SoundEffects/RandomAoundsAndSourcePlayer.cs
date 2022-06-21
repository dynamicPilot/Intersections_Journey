using AudioControls.Commons;
using UnityEngine;

namespace AudioControls.SoundPlayers
{
    public class RandomAoundsAndSourcePlayer : AudioPlayer
    {
        [SerializeField] private AudioSource[] _sources;
        bool _isMute = false;

        public override void PlaySound(int index)
        {
            if (_isMute) return;

            Sound sound;
            AudioSource source = _sources[Random.Range(0, _sources.Length)];

            if (index > -1) sound = collection.GetSoundOfIndex(index);
            else sound = collection.GetRandomSound();

            if (sound != null && source != null)
            {
                sound.SetSource(source);
                source.Play();
            }
        }

        public override void StopPlaying()
        {
            foreach (AudioSource source in _sources) source.Stop();
        }

        public override void TurnOnOff(bool isMute)
        {
            foreach (AudioSource source in _sources) source.mute = isMute;
            _isMute = isMute;
        }
    }
}

