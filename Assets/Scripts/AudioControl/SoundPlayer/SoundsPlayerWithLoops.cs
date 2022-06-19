using AudioControls.Commons;
using System.Collections;
using UnityEngine;

namespace AudioControls.SoundPlayers
{
    public class SoundsPlayerWithLoops : SoundsPlayer
    {
        [Header("Loops Settings")]
        [SerializeField] private float _defaultDuration = 1f;

        WaitForSeconds _timer;
        int _loopsCounter = 0;
        public override void PlaySound(int index)
        {
            StopAllCoroutines();
            base.PlaySound(index);
        }

        public void PlayLoopSound(int index, int loopsNumber, float duration = -1f)
        {
            StopAllCoroutines();
            Sound sound = collection.GetSoundOfIndex(index);
        
            if (sound == null) return;

            sound.SetSource(_source);
            SetTimer(duration, sound);
            _loopsCounter = loopsNumber;
            StartCoroutine(PlayingLoop());

        }

        public override void StopPlaying()
        {
            StopAllCoroutines();
            base.StopPlaying();
        }

        public override void TurnOnOff(bool isMute)
        {
            StopAllCoroutines();
            base.TurnOnOff(isMute);
        }

        void SetTimer(float duration, Sound sound)
        {
            if (duration < 0) duration = _defaultDuration;

            float value = Mathf.Max(sound.GetLenght(), duration);
            _timer = new WaitForSeconds(value);
        }

        IEnumerator PlayingLoop()
        {
            _source.Play();
            yield return _timer;
            if (_loopsCounter > 0)_loopsCounter--;
            if (_loopsCounter != 0) StartCoroutine(PlayingLoop());
        }
    }
}
