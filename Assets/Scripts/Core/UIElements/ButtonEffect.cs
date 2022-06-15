using IJ.Core.CommandPattern.Commands;
using IJ.Core.CommandPattern.Receivers;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    [SerializeField] private Vector3 pointerDownScale = new Vector3(0.95f, 0.95f, 0.95f);
    [SerializeField] private Vector3 pointerEnterScale = new Vector3(1.05f, 1.05f, 1.05f);
    [SerializeField] private int clickSoundIndex = 0;

    private Vector3 defaultScale;
    private RectTransform rectTransform;

    private ICommand makeClickSoundCommand;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale;
        makeClickSoundCommand = new ClickCommand(ReceiverUI.Instance, clickSoundIndex);

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (rectTransform.localScale != pointerDownScale)
            rectTransform.localScale = pointerDownScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (rectTransform.localScale != defaultScale)
            rectTransform.localScale = defaultScale;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        makeClickSoundCommand.Execute();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rectTransform.localScale != pointerEnterScale)
            rectTransform.localScale = pointerEnterScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rectTransform.localScale != defaultScale)
            rectTransform.localScale = defaultScale;
    }
}
