using IJ.Core.CommandPattern.Commands;
using IJ.Core.CommandPattern.Receivers;
using UnityEngine;

namespace IJ.Core.CommandPattern.Invokers
{
    public class GameflowCommandInvoker : CommandInvoker
    {
        [Header("Type")]
        [SerializeField] private ReceiverUI.GameflowCommandType type;

        private void Awake()
        {
            _command = new GameFlowCommand(ReceiverUI.Instance, type);
        }

    }
}