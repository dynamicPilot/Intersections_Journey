using IJ.Core.Menus;
using IJ.Core.Menus.MainMenu;
using UnityEngine;

namespace IJ.Core.CommandPattern.Receivers
{
    public class ReceiverMenuUI : ReceiverUI
    {
        [SerializeField] private SideMenuUI _sideMenuUI;
        [SerializeField] private MenuFlow _flow;

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
    }
}
