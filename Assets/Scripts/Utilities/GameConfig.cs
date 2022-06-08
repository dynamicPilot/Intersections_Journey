using UnityEngine;

namespace Utilites.Configs
{
    [CreateAssetMenu(fileName = "New Config", menuName = "Unit/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private string _menuSceneName = "MENU";
        public string MenuSceneName { get => _menuSceneName; }

        [Header("Time Settings")]
        [SerializeField] private float _minutesPerRealSecond = 7.5f;
        public float MinutesForRealSecond { get => _minutesPerRealSecond; }
        [SerializeField] private float _secondsBetweenTimeUpdate = 2f;
        public float SecondsBetweenTimeUpdate { get => _secondsBetweenTimeUpdate; }

        [SerializeField] private int _endHour = 12;
        public int EndHour { get => _endHour; }

        [Header("UI Update Settings")]
        [SerializeField] private float _indicatorUpdateTimer = 0.92f;
        public float IndicatorUpdateTimer { get => _indicatorUpdateTimer; }

        [Header("Unit Settings")]
        [SerializeField] private float _stayInCrashTimer = 2f;
        public float StayInCrashTimer { get => _stayInCrashTimer; }
        [SerializeField] private float _crashForce = 1f;
        public float CrashForce { get => _crashForce; }

        [Header("Input Control")]
        [SerializeField] private float _timeThreshold = 0.2f;
        public float TimeThreshold { get => _timeThreshold; }
        [SerializeField] private float _distanceMultiplier = 0.01f;
        public float DistanceMultiplier { get => _distanceMultiplier; }
        [SerializeField] private float _cameraSpeed = 15f;
        public float CameraSpeed { get => _cameraSpeed; }

        [Header("Audio Control")]
        [SerializeField] private float _defaultVolume = 0.7f;
        public float DefaultVolume { get => _defaultVolume; }
        [SerializeField] private float _effectsPeriod = 2f;
        public float EffectsPeriod { get => _effectsPeriod; }
    }
}


