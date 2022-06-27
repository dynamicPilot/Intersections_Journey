using IJ.Core.CommandPattern.Commands;
using IJ.Core.CommandPattern.Receivers;
using UnityEngine;

namespace IJ.Core.CommandPattern.Invokers
{
    public class SaveSettingsCommandInvoker : CommandInvoker
    {
        [Header("Type")]
        [SerializeField] private ReceiverUI.SaveSettingsCommandType _type;

        private void Awake()
        {
            _command = new SaveSettingsCommand(ReceiverUI.Instance, _type);
        }
    }
}
