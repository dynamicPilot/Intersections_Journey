using IJ.LanguageControl.UI;
using UnityEngine;

namespace IJ.Core.Settings
{
    public class GameStartSettingsControl : MenuSettingsControl
    {
        [SerializeField] private LanguageSwitcher _languageSwitcher;

        protected override void SetLanguageIndex()
        {
            base.SetLanguageIndex();
            _languageSwitcher.UpdateParameter();
        }
    }
}
