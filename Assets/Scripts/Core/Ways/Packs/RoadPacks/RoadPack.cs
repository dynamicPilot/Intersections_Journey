using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoadPack
{
    [SerializeField] private GameObject[] objectsWithColliders;
    [SerializeField] private TrafficLight[] trafficLights;

    public void SetPackState(bool state)
    {
        foreach(GameObject collider in objectsWithColliders)
        {
            collider.SetActive(state);
            if (collider.GetComponent<EdgeCollider2D>() != null) collider.GetComponent<EdgeCollider2D>().enabled = state;
        }

        for (int i = 0; i < trafficLights.Length; i++)
        {
            trafficLights[i].gameObject.SetActive(state);
            trafficLights[i].SetTrafficLightLevelState(trafficLights[i].gameObject.activeSelf);
        }
    }
}
