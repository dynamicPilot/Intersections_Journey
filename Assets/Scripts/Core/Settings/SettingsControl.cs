using UnityEngine;
using IJ.LanguageControl;
using IJ.Utilities.Configs;
using IJ.AudioControls.VolumeControls;

namespace IJ.Core.Settings
{
    public class SettingsControl : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private LanguageMaster _languageMaster;
        [SerializeField] private VolumeControl _volumeControl;

        [Header("Configs")]
        [SerializeField] private GameConfig _gameCongif;
        [SerializeField] private AudioConfig _audioConfig;

        private void Start()
        {
            SetSettings();
        }

        void SetSettings()
        {
            PlayerConfig playerConfig = SettingsControlUtility.ReadData(_audioConfig, _gameCongif);
            _languageMaster.SetLanguageByIndex(playerConfig.LangIndex);
            _volumeControl.SetVolumes(playerConfig);
        }

        public void SavePreferences()
        {
            _volumeControl.GetVolumes(out PlayerConfig playerConfig);
            playerConfig.LangIndex = _languageMaster.GetLanguageIndex();
            SettingsControlUtility.SaveData(playerConfig);
        }
    }
}
