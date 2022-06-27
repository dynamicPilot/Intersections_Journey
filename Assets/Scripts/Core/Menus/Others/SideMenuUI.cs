using UnityEngine;

namespace IJ.Core.Menus.Others
{
    public class SideMenuUI : MonoBehaviour
    {
        [SerializeField] private SettingsPanelUI _settingsPanel;
        public void OpenSettings()
        {           
            _settingsPanel.OpenPage();
            _settingsPanel.SetSettingsUI();
        }

        public void OpenAccount()
        {

        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

