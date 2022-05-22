using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private Image[] lights;
    [SerializeField] private float[] progressBorders;

    int nextLightIndex = 0;
    public void StartLoading()
    {
        foreach (Image image in lights) image.enabled = false;
        nextLightIndex = 0;
    }

    public void UpdateProgress(float progress)
    {
        if (progress >= progressBorders[nextLightIndex] && nextLightIndex < lights.Length)
        {
            if (nextLightIndex > 0) lights[nextLightIndex - 1].enabled = false;
            
            nextLightIndex++;

            lights[nextLightIndex - 1].enabled = true;
        }
    }

    public void ContinueLoading()
    {
        nextLightIndex = 4;
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


}
