using IJ.Core.CommandPattern.Commands;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IJ.Core.CommandPattern.Invokers
{
    public class CommandInvoker : MonoBehaviour, IPointerClickHandler
    {
        private protected ICommand _command;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            _command.Execute();
        }
    }
}
