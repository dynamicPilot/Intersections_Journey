using IJ.LanguageControl;
using UnityEngine;

namespace IJ.Core.Settings
{
    public class MenuSettingsControl : SettingsControl
    {
        [SerializeField] private LanguageMaster _languageMaster;
        
        protected override void ReadSettings()
        {
            base.ReadSettings();
            SetLanguageIndex();
        }

        protected virtual void SetLanguageIndex()
        {
            _languageMaster.SetLanguageByIndex(_langIndex);           
        }

        protected override void UpdateLangugeIndex()
        {
            _langIndex = _languageMaster.GetLanguageIndex();
        }
    }
}
