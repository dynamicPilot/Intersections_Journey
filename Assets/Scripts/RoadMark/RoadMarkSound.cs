using System.Collections;
using UnityEngine;
using AudioControls.SoundPlayers;
using Utilites.Configs;

public class RoadMarkSound : MonoBehaviour
{
    [SerializeField] private SoundsPlayer _player;
    [SerializeField] private int _indicatorSoundIndex = 0;
    [SerializeField] private int _allertSoundIndex = 1;
    [SerializeField] private float _indicatorLoopTime = 3f;
    [SerializeField] private float _allertLoopTime = 1.5f;

    //[Header("Settings")]
    //[SerializeField] private AudioConfig _config;

    Coroutine _playSoundByTimer;

    public void PlayIndicatorSound()
    {
        if (_playSoundByTimer!= null) StopCoroutine(_playSoundByTimer);
        _playSoundByTimer = StartCoroutine(PlaySoundByTimer(_indicatorSoundIndex, new WaitForSeconds(_indicatorLoopTime)));
    }

    public void PlayAllertSound()
    {
        if (_playSoundByTimer != null) StopCoroutine(_playSoundByTimer);
        _playSoundByTimer = StartCoroutine(PlaySoundByTimer(_allertSoundIndex, new WaitForSeconds(_allertLoopTime)));
    }

    public void StopPlaying()
    {
        StopAllCoroutines();
        _player.StopPlaying();
    }

    IEnumerator PlaySoundByTimer(int index, WaitForSeconds timer)
    {
        _player.PlaySound(index);
        yield return timer;
        _playSoundByTimer = StartCoroutine(PlaySoundByTimer(index, timer));
    }
}
