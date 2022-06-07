using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour
{
    [SerializeField] private Image[] backgrounds;
    [SerializeField] private Image[] signs;
    [SerializeField] private Image repairSign;
    [SerializeField] private TextMeshProUGUI text;

    public void SetColors(Color backgroundColor, Color signsColor, Color repairSignColor)
    {
        foreach(Image image in backgrounds)
        {
            float alpha = image.color.a;
            image.color = new Color (backgroundColor.r, backgroundColor.g, backgroundColor.b, alpha);
        }

        foreach (Image image in signs)
        {
            image.color = signsColor;
        }

        repairSign.color = repairSignColor;
        text.color = signsColor;
    }
}
