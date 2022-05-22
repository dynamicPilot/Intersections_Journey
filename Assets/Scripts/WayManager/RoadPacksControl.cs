using System;
using UnityEngine;

public class RoadPacksControl : MonoBehaviour
{
    //[SerializeField] private TrafficLight[] trafficLights;
    [SerializeField] private RoadPack[] packs;

    public void SetLevel(Level level)
    {
        for (int i = 0; i < packs.Length; i++)
        {
            packs[i].SetPackState(Array.IndexOf(level.ActiveRoadPacks, i) != -1);
        }

    }

}
