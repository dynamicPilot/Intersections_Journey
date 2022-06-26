using IJ.Core.Menus.Others;
using IJ.Core.Settings;
using UnityEngine;

namespace IJ.Core.CommandPattern.Receivers
{
    public class ReceiverMenuUI : ReceiverUI
    {
        public enum SaveSettingsCommandType { preferences, account }
        [SerializeField] private SideMenuUI _sideMenuUI;
        [SerializeField] private MenuFlow _flow;
        [SerializeField] private SettingsControl _settings;

        public void MakeMenuCommand(MenuCommandType type)
        {
            if (type == MenuCommandType.toSettings) _sideMenuUI.OpenSettings();
            else if (type == MenuCommandType.toAccount) _sideMenuUI.OpenAccount();
            else if (type == MenuCommandType.quit) _sideMenuUI.QuitGame();
        }

        public override void ChangeGameflow(GameflowCommandType type)
        {
            if (type == GameflowCommandType.toMenu) _flow.BackToMenu();
        }

        public void SaveSettings(SaveSettingsCommandType type)
        {
            if (type == SaveSettingsCommandType.preferences) _settings.SavePreferences();
        }
    }
}
