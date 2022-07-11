using AudioControls.SoundPlayers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IJ.Testing
{
    public class AudioTest : MonoBehaviour
    {
        [SerializeField] private SoundsPlayerWithDelay _player;
        [SerializeField] private float _delay;
        [SerializeField] private int _soundIndex;

        private void Start()
        {
            _player.PlaySoundWithDelay(_soundIndex, _delay);
        }
    }
}
