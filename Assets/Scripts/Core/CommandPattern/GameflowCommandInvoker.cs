using UnityEngine;
using UnityEngine.EventSystems;

public class GameflowCommandInvoker : MonoBehaviour, IPointerClickHandler
{
    [Header("Type")]
    [SerializeField] private ReceiverUI.GameflowCommandType type;

    private ICommand command;

    private void Awake()
    {
        command = new GameFlowCommand(ReceiverUI.Instance as ReceiverLevelUI, type);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        command.Execute();
    }
}