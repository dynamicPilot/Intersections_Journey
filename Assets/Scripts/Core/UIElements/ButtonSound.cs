using IJ.Core.CommandPattern.Commands;
using IJ.Core.CommandPattern.Receivers;
using UnityEngine;
using UnityEngine.EventSystems;


namespace IJ.Core.UIElements
{
    /// <summary>
    /// Simple Click effect. 
    /// Need ReceiverUI Singlton Instance in Scene.
    /// </summary>
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private int clickSoundIndex = 0;
        private ICommand makeClickSoundCommand;

        private void Awake()
        {
            makeClickSoundCommand = new ClickCommand(ReceiverUI.Instance, clickSoundIndex);

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            makeClickSoundCommand.Execute();
        }
    }
}
