using System.Collections;
using UnityEngine;

public class VehicleLightEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer lightRenderer;
    [SerializeField] private SpriteRenderer lightEffect;

    Color defaultColor;
    WaitForSeconds waitForSeconds;
    Coroutine turningEffectCoroutine;

    bool needEffect = false;

    private void Awake()
    {
        needEffect = (lightEffect != null);

        if (needEffect) lightEffect.gameObject.SetActive(true);
    }

    public void TurnOnOffLights(bool mode)
    {
        if (needEffect) lightEffect.gameObject.SetActive(mode);
    }

    public void StartTurningEffect(Color turningColor, float turningPeriod)
    {
        defaultColor = lightRenderer.color;
        waitForSeconds = new WaitForSeconds(turningPeriod);

        turningEffectCoroutine = StartCoroutine(TurningEffect(turningColor));
    }

    IEnumerator TurningEffect(Color turningColor)
    {
        lightRenderer.color = turningColor;
        if (needEffect) lightEffect.color = turningColor;

        yield return waitForSeconds;

        lightRenderer.color = defaultColor;
        if (needEffect) lightEffect.color = defaultColor;

        yield return waitForSeconds;

        turningEffectCoroutine = StartCoroutine(TurningEffect(turningColor));
    }

    public void StopTurningEffect()
    {
        StopCoroutine(turningEffectCoroutine);

        lightRenderer.color = defaultColor;
        if (needEffect) lightEffect.color = defaultColor;
    }

    // set order layer
    //public void SetRendererSortingLayerByID(int newID)
    //{
    //    if (lightRenderer.sortingLayerID == newID) return;

    //    lightRenderer.sortingLayerID = newID;
    //    lightEffect.sortingLayerID = newID;
    //}

    public void SetSortingLayerById(int id)
    {
        if (lightRenderer.sortingLayerID == id) return;

        lightRenderer.sortingLayerID = id;
        lightEffect.sortingLayerID = id;
    }
}
