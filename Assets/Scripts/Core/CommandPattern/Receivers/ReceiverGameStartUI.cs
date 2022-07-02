using UnityEngine;

namespace IJ.Core.CommandPattern.Receivers
{
    public class ReceiverGameStartUI : ReceiverUI
    {
        [SerializeField] private GameStartFlow _flow;
        public override void ChangeGameflow(GameflowCommandType type)
        {
            if (type == GameflowCommandType.toMenu)
            {
                SaveSettings(SaveSettingsCommandType.preferences);
                _flow.BackToMenu();

            }
        }
    }
}
