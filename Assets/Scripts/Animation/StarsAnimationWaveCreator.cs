using IJ.Animations.Waves;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Animations
{
    public class StarsAnimationWaveCreator : MonoBehaviour
    {
        [SerializeField] private AnimationWaveWithPath[] _starWaves;
        [SerializeField] private AnimationWaveWithPath _sliderWave;
        [SerializeField] private AnimationWaveWithPath _endStarWave;
        [SerializeField] private AnimationWaveWithPath _soundsWave;
        [SerializeField] private AnimationWavesWithPathRoutine _animationWaveRoutine;

        private List<AnimationWaveWithPath> _waves = new List<AnimationWaveWithPath>();

        private void Awake()
        {
            SetStars(3);
        }
        public void SetStars(int starNumber)
        {
            _waves.Clear();
            for (int i = 0; i < starNumber; i++)
            {
                _waves.Add(_starWaves[i]);
                _waves.Add(_soundsWave);
                _waves.Add(_sliderWave);
            }

            _waves.Add(_endStarWave);

            _animationWaveRoutine.SetWaves(_waves.ToArray());
        }
    }
}
