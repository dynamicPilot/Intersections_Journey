using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string soundName;
    [SerializeField] private AudioClip clip;

    [Header("Settings")]
    [SerializeField] private bool isLoop = false;
    [SerializeField][Range(0f, 1f)] private float defaultVolume = 0.7f;

    public Sound(string _soundName, bool _isLoop = false)
    {
        soundName = _soundName;
        isLoop = _isLoop;
    }

    public void SetSource(AudioSource source, float volumeRate)
    {
        source.volume = volumeRate * defaultVolume;
        source.clip = clip;
        source.loop = isLoop;
    }
}
