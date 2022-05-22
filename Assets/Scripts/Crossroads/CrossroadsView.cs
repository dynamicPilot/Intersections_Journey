using UnityEngine;

public class CrossroadsView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] roads;
    [SerializeField] private SpriteRenderer[] roadBorders;
    [SerializeField] private SpriteRenderer[] lines;

    public void SetColors(Color bordersColor, Color roadsColor, Color linesColor)
    {
        foreach(SpriteRenderer renderer in roads)
        {
            renderer.color = roadsColor;
        }

        foreach (SpriteRenderer renderer in roadBorders)
        {
            renderer.color = bordersColor;
        }

        foreach (SpriteRenderer renderer in lines)
        {
            renderer.color = linesColor;
        }
    }
}
