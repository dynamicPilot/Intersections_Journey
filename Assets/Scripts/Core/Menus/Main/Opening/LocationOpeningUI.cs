using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class LocationOpeningUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] panelsForLocations;
    [SerializeField] private PlayableDirector _director;

    [Header("Settings")]
    [SerializeField] private PlayableAsset[] _assets;

    public void OpenLocationByIndex(int locationIndex)
    {
        if (locationIndex > panelsForLocations.Length - 1) return;

        _director.playableAsset = _assets[locationIndex];
        StartCoroutine(MakeOpening());
    }

    IEnumerator MakeOpening()
    {
        _director.Play();
        yield return _director.duration;
    }
}
