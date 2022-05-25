using UnityEngine;

public class SoundsPlayerAndStorage: MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource soundsSource;

    [Header("Sounds")]
    [SerializeField] private Sound[] sounds;

    float volumeRate = 0f;

    public void SetVolumeRate(float _volumeRate)
    {
        volumeRate = _volumeRate;
        if (volumeRate == 0) TurnOnOff(true);
    }

    public void TurnOnOff(bool _isMute)
    {
        soundsSource.mute = _isMute;
    }

    public void PlaySound(int index)
    {
        if (index > -1 || index < sounds.Length)
        {
            sounds[index].SetSource(soundsSource, volumeRate);
            soundsSource.Play();
        }
    }
}
