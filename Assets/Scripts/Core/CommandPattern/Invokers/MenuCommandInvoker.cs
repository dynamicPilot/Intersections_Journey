using IJ.Core.CommandPattern.Commands;
using IJ.Core.CommandPattern.Receivers;
using UnityEngine;

namespace IJ.Core.CommandPattern.Invokers
{
    public class MenuCommandInvoker : CommandInvoker
    {
        [Header("Type")]
        [SerializeField] private ReceiverUI.MenuCommandType _type;

        private void Awake()
        {
            _command = new MenuCommand(ReceiverUI.Instance as ReceiverMenuUI, _type);
        }
    }

}
