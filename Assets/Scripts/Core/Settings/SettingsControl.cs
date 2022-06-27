using UnityEngine;
using IJ.Utilities.Configs;
using IJ.AudioControls.VolumeControls;

namespace IJ.Core.Settings
{
    public class SettingsControl : MonoBehaviour
    {
        [SerializeField] private bool _updateVolumesWhenRead = false;

        [Header("Configs")]
        [SerializeField] private GameConfig _gameCongif;
        [SerializeField] private AudioConfig _audioConfig;

        [Header("Components")]
        [SerializeField] private VolumeControl _volumeControl;

        protected int _langIndex;

        private void Start()
        {
            ReadSettings();
        }

        protected virtual void ReadSettings()
        {
            PlayerConfig playerConfig = SettingsControlUtility.ReadData(_audioConfig, _gameCongif);
            _langIndex = playerConfig.LangIndex;

            if (_updateVolumesWhenRead) SetVolumes(playerConfig);
        }

        protected void SetVolumes(PlayerConfig playerConfig)
        {
            _volumeControl.SetVolumes(playerConfig);
        }

        public void SavePreferences()
        {
            UpdateLangugeIndex();
            _volumeControl.GetVolumes(out PlayerConfig playerConfig);
            playerConfig.LangIndex = _langIndex;
            SettingsControlUtility.SaveData(playerConfig);
        }

        protected virtual void UpdateLangugeIndex()
        {

        }
    }
}
