using AudioControls.Commons;
using System.Collections;
using UnityEngine;

namespace AudioControls.SoundPlayers
{
    public class SoundsPlayerWithDelay : SoundsPlayer
    {
        bool isPlaying = false;
        public override void PlaySound(int index)
        {
            if (isPlaying) return;
            base.PlaySound(index);
        }

        public void PlaySoundWithDelay(int index, float delayTime, bool inRealTime = false)
        {
            Sound sound = collection.GetSoundOfIndex(index);
            if (sound != null) sound.SetSource(_source);
            StartCoroutine(PlayWithDelay(delayTime, inRealTime));
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

        IEnumerator PlayWithDelay(float delayTime, bool inRealTime)
        {
            isPlaying = true;

            if (inRealTime) yield return new WaitForSecondsRealtime(delayTime);
            else yield return new WaitForSeconds(delayTime);

            _source.Play();
            isPlaying = false;
        }
    }
}
