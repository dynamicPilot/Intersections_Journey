using IJ.Core.CommandPattern.Commands;
using IJ.Core.CommandPattern.Receivers;
using UnityEngine;

namespace IJ.Core.CommandPattern.Invokers
{
    public class PauseUnpauseCommandInvoker : CommandInvoker
    {
        [Header("Type")]
        [SerializeField] private bool toPause = false;

        private void Awake()
        {
            _command = new PauseUnpauseCommand(ReceiverUI.Instance as ReceiverLevelUI, toPause);
        }
    }
}
