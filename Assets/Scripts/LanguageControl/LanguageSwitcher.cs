using IJ.UIElements;
using UnityEngine;

namespace IJ.LanguageControl.UI
{
    public class LanguageSwitcher : PanelWithSwitches
    {
        [SerializeField] private LanguageMaster _master;

        public void UpdateParameter()
        {
            SetImageByIndex(_master.GetLanguageIndex());
            _master.UpdateLanguageViews();
        }

        public void UpdateParameterToDefault(int langIndex)
        {
            SetImageByIndex(langIndex);
            _master.SetLanguageByIndex(_index);
        }

        public override void Move(int step)
        {
            base.Move(step);
            _master.SetLanguageByIndex(_index);
        }
    }
}
