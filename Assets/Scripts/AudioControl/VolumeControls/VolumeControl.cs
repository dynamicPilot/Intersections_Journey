using IJ.Utilities.Configs;
using UnityEngine;

namespace IJ.AudioControls.VolumeControls
{
    public class VolumeControl : MonoBehaviour
    {
        [Header("Mixer Parameters Data")]
        [SerializeField] private VolumeGroupData _musicVolume;
        [SerializeField] private VolumeGroupData _effectsVolume;
        [SerializeField] private VolumeGroupData _totalVolume;

        public void SetVolumes(PlayerConfig playerConfig)
        {
            _musicVolume.SetValue(playerConfig.MusicVolume);
            _effectsVolume.SetValue(playerConfig.EffectsVolume);
            _totalVolume.SetValue(playerConfig.TotalVolume);
        }

        public void GetVolumes(out PlayerConfig playerConfig)
        {
            playerConfig = new PlayerConfig(_musicVolume.GetValue(), _effectsVolume.GetValue(), _totalVolume.GetValue());
        }
    }

}

