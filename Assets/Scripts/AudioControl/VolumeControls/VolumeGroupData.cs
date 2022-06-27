using UnityEngine;
using UnityEngine.Audio;

namespace IJ.AudioControls.VolumeControls
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Volume Group", menuName = "Unit/VolumeGroup")]
    public class VolumeGroupData : ScriptableObject
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private string _parameter;
        [SerializeField] private float _minVolume = -80f;
        [SerializeField] private float _maxVolume = 0f;

        private float _muteDelta = 10f;
        public void SetValue(float value)
        {
            _mixer.SetFloat(_parameter, Mathf.Lerp(_minVolume, _maxVolume, value));
        }

        public float GetValue()
        {
            if (_mixer.GetFloat(_parameter, out float volume))
            {
                return Mathf.InverseLerp(_minVolume, _maxVolume, volume);
            }

            return 0f;
        }

        public bool IsMute()
        {
            if (_mixer.GetFloat(_parameter, out float volume))
            {
                return volume < (_minVolume + _muteDelta);
            }

            return true;
        }

        public void MakeMute()
        {
            SetValue(_minVolume);
        }
    }
}
