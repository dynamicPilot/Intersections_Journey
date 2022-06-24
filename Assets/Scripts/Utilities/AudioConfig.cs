using UnityEngine;
using UnityEngine.Audio;

namespace Utilites.Configs
{
    [CreateAssetMenu(fileName = "New AudioConfig", menuName = "Unit/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        //[SerializeField] private float _defaultVolume = 0.7f;
        //public float DefaultVolume { get => _defaultVolume; }
        [SerializeField] private float _effectsPeriod = 2f;
        public float EffectsPeriod { get => _effectsPeriod; }


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
