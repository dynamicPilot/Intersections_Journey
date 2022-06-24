using AudioControls.SoundPlayers;
using IJ.MovableUnits.MediatorAndComponents;
using System.Collections;
using UnityEngine;

namespace MovableUnits.Effects
{
    public class VTrainSound : MonoBehaviour, IHoldTrainUnitComponent
    {
        [SerializeField] private SoundsPlayer _player;
        [SerializeField] private float _delay = 2f;
        [SerializeField] private int _signalIndex = 0;
        [SerializeField] private int _moveIndex = 0;

        WaitForSeconds _timer;
        private void Awake()
        {
            _timer = new WaitForSeconds(_delay);
        }

        public void DoInStartRoute()
        {
            _player.PlaySound(_signalIndex);
            DoInRestart();
        }

        public void DoInRestart()
        {
            StartCoroutine(StartPlaying());
        }

        public void DoInStop()
        {
            _player.StopPlaying();
        }

        IEnumerator StartPlaying()
        {
            yield return _timer;
            _player.PlaySound(_moveIndex);
        }
    }
}
