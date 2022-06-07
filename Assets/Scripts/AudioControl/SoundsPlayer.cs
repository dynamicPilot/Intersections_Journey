using UnityEngine;

public class SoundsPlayer: MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource source;

    [Header("Sounds")]
    [SerializeField] private SoundCollection collection;

    float volumeRate = 0f;

    public void SetVolumeRate(float _volumeRate)
    {
        volumeRate = _volumeRate;
        if (volumeRate == 0) TurnOnOff(true);
    }

    public void TurnOnOff(bool _isMute)
    {
        source.mute = _isMute;
    }

    public void PlaySound(int index)
    {
        Sound sound = collection.GetSoundOfIndex(index);
        if (sound != null)
        {
            sound.SetSource(source, volumeRate);
            source.Play();
        }
    }
}
