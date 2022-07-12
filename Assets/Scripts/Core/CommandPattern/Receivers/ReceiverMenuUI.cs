using IJ.Core.Menus;
using IJ.Core.Menus.Others;
using UnityEngine;

namespace IJ.Core.CommandPattern.Receivers
{
    public class ReceiverMenuUI : ReceiverUI
    {       
        [SerializeField] private SideMenuUI _sideMenuUI;
        [SerializeField] private MenuFlow _flow;
        [SerializeField] private AnimatedDropdownMenuUI _sidePanelDropdownMenu;

        public void MakeMenuCommand(MenuCommandType type)
        {
            _sidePanelDropdownMenu.OpenClosePanel();
            if (type == MenuCommandType.toSettings) _sideMenuUI.OpenSettings();
            else if (type == MenuCommandType.toCredits) _sideMenuUI.OpenCredits();
            else if (type == MenuCommandType.quit) _sideMenuUI.QuitGame();
        }

        public override void ChangeGameflow(GameflowCommandType type)
        {
            if (type == GameflowCommandType.toMenu) _flow.BackToMenu();
        }

        public override void SaveSettings(SaveSettingsCommandType type)
        {
            base.SaveSettings(type);
            _flow.BackToMenu();
        }
    }
}
