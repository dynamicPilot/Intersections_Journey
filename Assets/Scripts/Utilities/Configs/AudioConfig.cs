using UnityEngine;
using UnityEngine.Audio;

namespace IJ.Utilities.Configs
{
    [CreateAssetMenu(fileName = "New AudioConfig", menuName = "Unit/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        //[SerializeField] private float _defaultVolume = 0.7f;
        //public float DefaultVolume { get => _defaultVolume; }
        [SerializeField] private float _effectsPeriod = 2f;
        public float EffectsPeriod { get => _effectsPeriod; }

        [Header("Defaults")]
        [SerializeField] private float _defaultMusicVolume = 0.9f;
        public float DefaultMusicVolume { get => _defaultMusicVolume; }
        [SerializeField] private float _defaultEffectsVolume = 0.9f;
        public float DefaultEffectsVolume { get => _defaultEffectsVolume; }
        [SerializeField] private float _defaultTotalVolume = 0.9f;
        public float DefaultTotalVolume { get => _defaultTotalVolume; }


        [Header("Snapshots")]
        [SerializeField] private AudioMixerSnapshot _startSnaphot;
        public AudioMixerSnapshot StartSnaphot { get => _startSnaphot; }
        [SerializeField] private AudioMixerSnapshot _activeSnaphot;
        public AudioMixerSnapshot ActiveSnaphot { get => _activeSnaphot; }
        [SerializeField] private AudioMixerSnapshot _pausedSnaphot;
        public AudioMixerSnapshot PausedSnaphot { get => _pausedSnaphot; }

        [Header("Transitions")]
        [SerializeField] private float _shortTransitionTime = 0.1f;
        public float ShortTransitionTime { get => _shortTransitionTime; }

        [SerializeField] private float _transitionTime = 0.5f;
        public float TransitionTime { get => _transitionTime; }
    }
}
