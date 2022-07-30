using AudioControls.SoundPlayers;
using UnityEngine;

namespace IJ.Animations.Waves
{
    public class SoundsRowAnimationWaveMember : MonoBehaviour, IAnimationWaveMember
    {
        [SerializeField] private SoundsPlayer _player;
        [SerializeField] private int _startSoundIndex = 0;

        int _index;
        public void OnInitialState()
        {
            _index = _startSoundIndex;
        }

        void PlaySound()
        {
            _player.PlaySound(_index);
            _index++;
        }

        public void OnWaveStart(AnimationPath path = null)
        {
            PlaySound();
        }
    }
}
