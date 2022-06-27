using UnityEngine;
using UnityEngine.UI;

namespace IJ.AudioControls.VolumeControls
{
    public class VolumeSlider : MonoBehaviour
    { 
        [SerializeField] private Slider _slider;
        [SerializeField] private VolumeGroupData _group;

        public void SetValue(float value)
        {
            _group.SetValue(value);
        }

        public void UpdateParameter()
        {
            _slider.value = _group.GetValue();
        }

        public void UpdateParameterToDefault(float defaultFloat)
        {
            SetValue(defaultFloat);
            UpdateParameter();
        }
    }
}
