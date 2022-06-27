using IJ.AudioControls.VolumeControls;
using IJ.LanguageControl.UI;
using IJ.Utilities.Configs;
using UnityEngine;

namespace IJ.Core.Menus.Others
{
    public class SettingsPanelUI : MovablePanelUI
    {
        [Header("UI Components")]
        [SerializeField] private VolumeSlider _musicVolume;
        [SerializeField] private VolumeSlider _effectsVolume;
        [SerializeField] private VolumeSlider _totalVolume;
        [SerializeField] private LanguageSwitcher _languageSwitcher;

        [Header("Configs")]
        [SerializeField] private GameConfig _gameCongif;
        [SerializeField] private AudioConfig _audioConfig;

        public void SetSettingsUI()
        {
            _musicVolume.UpdateParameter();
            _effectsVolume.UpdateParameter();
            _totalVolume.UpdateParameter();
            _languageSwitcher.UpdateParameter();
        }

        public void ResetToDefaults()
        {
            _musicVolume.UpdateParameterToDefault(_audioConfig.DefaultMusicVolume);
            _effectsVolume.UpdateParameterToDefault(_audioConfig.DefaultEffectsVolume);
            _totalVolume.UpdateParameterToDefault(_audioConfig.DefaultTotalVolume);
            _languageSwitcher.UpdateParameterToDefault(_gameCongif.DefaultLangIndex);
        }
    }
}
