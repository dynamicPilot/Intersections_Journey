using IJ.AudioControls.VolumeControls;
using IJ.LanguageControl;
using IJ.Utilities.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Core.Menus.Others
{
    public class SettingsPanelUI : MovablePanelUI
    {
        [Header("UI Components")]
        [SerializeField] private Transform[] _parameters;

        [Header("Configs")]
        [SerializeField] private GameConfig _gameCongif;
        [SerializeField] private AudioConfig _audioConfig;

        public void SetSettingsUI()
        {
            foreach (Transform parameter in _parameters)
            {
                parameter.GetComponent<ISettingsParameter>().UpdateParameter();
            }
        }

        public void ResetToDefaults()
        {
            foreach (Transform parameter in _parameters)
            {
                
            }
        }
    }
}
