using UnityEngine;

public class TrafficLightObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer lightRenderer;
    [SerializeField] private SpriteRenderer lightEffect;

    [Header("Colors")]
    [SerializeField] private Color redColor;
    [SerializeField] private Color greenColor;

    public void SetColor(TrafficLight.MODE newMode)
    {
        if (newMode == TrafficLight.MODE.green)
        {
            lightRenderer.color = greenColor;
            lightEffect.color = greenColor;
        }
        else //if (newMode == TrafficLight.MODE.green)
        {
            lightRenderer.color = redColor;
            lightEffect.color = redColor;
        }
    }
}
