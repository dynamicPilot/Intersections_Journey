using UnityEngine;


namespace AudioControls.Commons
{
    [System.Serializable]
    public class Sound
    {
        [SerializeField] private string soundName;
        [SerializeField] private AudioClip clip;

        [Header("Settings")]
        [SerializeField] private bool isLoop = false;
        [SerializeField][Range(0f, 1f)] private float _defaultVolume = 1f;

        public Sound(string _soundName, bool _isLoop = false)
        {
            soundName = _soundName;
            isLoop = _isLoop;
        }

        public void SetSource(AudioSource source)
        {
            source.volume = _defaultVolume;
            source.clip = clip;
            source.loop = isLoop;
        }

        public float GetLenght()
        {
            return clip.length;
        }
    }
}

