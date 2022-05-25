using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    [SerializeField] private Vector3 pointerDownScale = new Vector3(0.95f, 0.95f, 0.95f);
    [SerializeField] private Vector3 pointerEnterScale = new Vector3(1.05f, 1.05f, 1.05f);
    [SerializeField] private int clickSoundIndex = 0;
    //[SerializeField] private Color pointerDownColor;
    //[SerializeField] private Color pointerDownSignColor;
    //[SerializeField] private int clickSoundIndex = 0;

    [Header("Options")]
    [SerializeField] private bool needScaleChange;
    //[SerializeField] private bool needColorChange;
    //[SerializeField] private bool needSignColorChange;
    [SerializeField] private bool needClickSound = false;

    [Header("Scripts")]
    [SerializeField] private AudioControl audioControl;

    //[Header("UI Elements")]
    //[SerializeField] private Image image;
    //[SerializeField] private Text text;

    private Vector3 defaultScale;
    //private Color defaultColor;
    //private Color defaultTextColor;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale;

        //if (image != null)
        //{
        //    defaultColor = image.color;
        //}
        //else
        //{
        //    needColorChange = false;
        //}

        //if (text != null)
        //{
        //    defaultTextColor = text.color;
        //}
        //else
        //{
        //    needSignColorChange = false;
        //}
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (rectTransform.localScale != pointerDownScale && needScaleChange)
            rectTransform.localScale = pointerDownScale;

        //if (needColorChange)
        //{
        //    image.color = pointerDownColor;
        //}

        //if (needSignColorChange)
        //{
        //    text.color = pointerDownSignColor;
        //}
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (rectTransform.localScale != defaultScale && needScaleChange)
            rectTransform.localScale = defaultScale;

        //if (needColorChange)
        //{
        //    image.color = defaultColor;
        //}

        //if (needSignColorChange)
        //{
        //    text.color = defaultTextColor;
        //}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (needClickSound && audioControl != null) audioControl.PlaySound(clickSoundIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rectTransform.localScale != pointerEnterScale && needScaleChange)
            rectTransform.localScale = pointerEnterScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rectTransform.localScale != defaultScale && needScaleChange)
            rectTransform.localScale = defaultScale;
    }
}
