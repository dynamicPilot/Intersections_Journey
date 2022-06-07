using UnityEngine;
using UnityEngine.EventSystems;

public class PauseUnpauseCommandInvoker : MonoBehaviour, IPointerClickHandler
{
    [Header("Type")]
    [SerializeField] private bool toPause = false;

    private ICommand command;

    private void Awake()
    {
        command = new PauseUnpauseCommand(ReceiverUI.Instance as ReceiverLevelUI, toPause);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        command.Execute();
    }
}
