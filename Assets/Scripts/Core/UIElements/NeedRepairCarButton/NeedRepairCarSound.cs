using AudioControls.SoundPlayers;
using System.Collections;
using UnityEngine;

namespace IJ.Core.UIElements.NeedRepairCar
{
    public class NeedRepairCarSound : MonoBehaviour
    {
        [SerializeField] private SoundsPlayer _player;
        [SerializeField] private int _soundIndex = 3;
        [SerializeField] private float _loopTime = 3.2f;

        public void StartSound()
        {
            StartCoroutine(PlayingSound(new WaitForSeconds(_loopTime)));
        }

        public void StopSound()
        {
            StopAllCoroutines();
        }

        IEnumerator PlayingSound(WaitForSeconds timer)
        {
            _player.PlaySound(_soundIndex);
            yield return timer;
            StartCoroutine(PlayingSound(timer));
        }
    }
}
