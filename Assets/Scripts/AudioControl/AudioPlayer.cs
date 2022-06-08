using UnityEngine;

namespace AudioControls.Commons
{
    public abstract class AudioPlayer : MonoBehaviour
    {
        [Header("Sounds")]
        [SerializeField] private protected SoundCollection collection;

        protected float _volumeRate = 0f;

        public virtual void SetVolumeRate(float volumeRate)
        {
            _volumeRate = volumeRate;
            if (_volumeRate == 0) TurnOnOff(true);
        }

        public abstract void TurnOnOff(bool _isMute);
        public abstract void PlaySound(int index);
    }
}

