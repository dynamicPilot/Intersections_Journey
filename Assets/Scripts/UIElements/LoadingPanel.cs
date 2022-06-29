using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private Image[] lights;
    [SerializeField] private float[] progressBorders;

    [Header("Auto Progress Options")]
    [SerializeField] private float _updateTime = 3f;

    WaitForSeconds _timer;
    int _nextLightIndex = 0;
    bool _inAuto = false;

    public void StartLoading(bool autoUpdate = false)
    {
        if (_inAuto) StopAutoProgress();

        foreach (Image image in lights) image.enabled = false;
        _nextLightIndex = 0;

        if (autoUpdate) StartAutoProgress();
    }

    public void UpdateProgress(float progress)
    {
        CheckIndex();

        if (progress >= progressBorders[_nextLightIndex])
        {
            if (_nextLightIndex > 0) lights[_nextLightIndex - 1].enabled = false;
            else lights[lights.Length - 1].enabled = false;

            _nextLightIndex++;

            lights[_nextLightIndex - 1].enabled = true;
        }
    }

    public void ContinueLoading()
    {
        _nextLightIndex = 4;
        for (int i = 0; i < lights.Length; i++)
        {
            if (i < lights.Length - 1)
            {
                lights[i].enabled = false;
            }
            else
            {
                lights[i].enabled = true;
            }
        }
    }

    void StartAutoProgress()
    {
        _timer = new WaitForSeconds(_updateTime);
        _inAuto = true;
        StartCoroutine(AutoUpdater());
    }

    public void StopAutoProgress(bool closePanel = false)
    {
        StopAllCoroutines();
        _inAuto = false;

        if (closePanel) gameObject.SetActive(false);
    }

    IEnumerator AutoUpdater()
    {
        UpdateProgress(progressBorders[_nextLightIndex]);
        yield return _timer;
        StartCoroutine(AutoUpdater());
    }

    void CheckIndex()
    {
        if (_nextLightIndex > lights.Length - 1) _nextLightIndex = 0;
    }

}
