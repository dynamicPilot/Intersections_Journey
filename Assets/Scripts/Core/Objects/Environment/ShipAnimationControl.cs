using AudioControls.SoundPlayers;
using IJ.Animations.Objects;
using System.Collections;
using UnityEngine;

namespace IJ.Core.Objects.Environment
{
    public class ShipAnimationControl : MonoBehaviour
    {
        [SerializeField] private ShipAnimation _animation;
        [SerializeField] private SoundsPlayerWithDelay _player;
        [SerializeField] private float _signalDelay = 5f;
        [SerializeField] private float _startDelay = 0;
        [SerializeField] private int _soundIndex = 0;
        private void Start()
        {
            StartCoroutine(StartDelay());
        }

        IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(_startDelay);
            StartShip();
        }

        void StartShip()
        {
            _animation.SailIn();
            _player.PlaySoundWithDelay(_soundIndex, _signalDelay, false, true);
        }
    }
}

