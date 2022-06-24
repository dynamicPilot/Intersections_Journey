using UnityEngine;

namespace AudioControls.Commons
{
    public abstract class AudioPlayer : MonoBehaviour
    {
        [Header("Sounds")]
        [SerializeField] private protected SoundCollection collection;

        protected float _volumeRate = 1f;

        public abstract void TurnOnOff(bool _isMute);
        public abstract void PlaySound(int index);

        public abstract void StopPlaying();
    }
}

