using AudioControls.SoundPlayers;
using UnityEngine;

namespace IJ.Core.UIElements.GameFlowPanels
{
    public class PanelsUISound : MonoBehaviour
    {
        [SerializeField] private SoundsPlayerWithDelay _player;
        [SerializeField] private int _startIndex = 0;
        [SerializeField] private int _winIndex = 1;
        [SerializeField] private int _loseIndex = 2;
        [SerializeField] private float _delay = 0.5f;

        public void PlayStartSound()
        {
            _player.PlaySoundWithDelay(_startIndex, _delay, true);
        }

        public void PlayWinSound()
        {
            _player.PlaySoundWithDelay(_winIndex, _delay, true);
        }

        public void PlayLoseSound()
        {
            _player.PlaySoundWithDelay(_loseIndex, _delay, true);
        }

    }
}
