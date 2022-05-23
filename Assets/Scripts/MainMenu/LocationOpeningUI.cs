using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationOpeningUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] panelsForLocations;

    [Header("Settings")]
    [SerializeField] private float animationLength = 3f;

    WaitForSeconds timer;
    int activeIndex = -1;

    private void Awake()
    {
        timer = new WaitForSeconds(animationLength);
    }

    public void OpenLocationByIndex(int locationIndex)
    {
        if (locationIndex > panelsForLocations.Length - 1) return;

        activeIndex = locationIndex;
        panel.SetActive(true);
        panelsForLocations[activeIndex].SetActive(true);
        StartCoroutine(MakeOpening());
    }

    IEnumerator MakeOpening()
    {
        yield return timer;

        // close all
        panel.SetActive(false);

        if (activeIndex > -1) panelsForLocations[activeIndex].SetActive(false);

    }
}
