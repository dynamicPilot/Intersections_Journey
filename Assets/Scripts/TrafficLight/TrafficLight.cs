using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLight : MonoBehaviour
{
    public enum MODE { red, green }

    [SerializeField] private EdgeCollider2D colliderComponent;
    [SerializeField] private Image image;
    [SerializeField] private Image effect;

    [Header("Points")]
    [SerializeField] private List<int> pointsNumberToBeAffected = new List<int>();

    [Header("Colors")]
    [SerializeField] private Color greenColor;
    [SerializeField] private Color redColor;

    [Header("Scripts")]
    [SerializeField] private TrafficLightObject trafficLightObject;

    MODE mode = MODE.red;

    private void Awake()
    {       
        colliderComponent.enabled = true;
        SetTrafficLightColor();
    }

    public void SetTrafficLightLevelState(bool state)
    {
        colliderComponent.enabled = state;

        if (state)
        {
            // active traffic light                      
            mode = MODE.red;
            SetTrafficLightColor();
        }

        trafficLightObject.gameObject.SetActive(state);
    }

    public void SwitchTrafficLight()
    {
        colliderComponent.enabled = !colliderComponent.enabled;
        SetTrafficLightColor();
    }

    void SetTrafficLightColor()
    {
        if (colliderComponent.enabled)
        {
            image.color = redColor;
            mode = MODE.red;

            if (effect != null) effect.color = redColor;
        }
        else
        {
            image.color = greenColor;
            mode = MODE.green;

            if (effect != null) effect.color = greenColor;
        }

        trafficLightObject.SetColor(mode);
    }

    public bool CheckPointNumber(int pointNumberToCheck)
    {
        return pointsNumberToBeAffected.Contains(pointNumberToCheck);
    }
}
