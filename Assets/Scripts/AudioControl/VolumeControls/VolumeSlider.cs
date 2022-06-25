using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace IJ.AudioControls.VolumeControls
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private string _parameter;

        private float _minVolume = -80f;
        private float _maxVolume = 0f;

        public void SetVolume(float value)
        {
            _mixer.SetFloat(_parameter, Mathf.Lerp(_minVolume, _maxVolume, value));
        }

        public void SetValue(float value)
        {
            Logging.Log(value);

            if (_mixer.GetFloat(_parameter, out float volume))
            {
                _slider.value = Mathf.InverseLerp(-80.0f, 0.0f, volume);
            }
        }
    }
}
