using UnityEngine;

namespace IJ.Core.Menus.Others
{
    public class SideMenuUI : MonoBehaviour
    {
        [SerializeField] private SettingsPanelUI _settingsPanel;
        [SerializeField] private MovablePanelUI _creditsPanel;
        public void OpenSettings()
        {           
            _settingsPanel.OpenPage();
            _settingsPanel.SetSettingsUI();
        }

        public void OpenCredits()
        {
            _creditsPanel.OpenPage();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

