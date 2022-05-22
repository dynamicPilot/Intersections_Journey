using System.Collections;
using UnityEngine;

public class WheelSmoke : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] smokeParticles;
    [SerializeField] private float duration = 3f;

    WaitForSeconds timer;
    bool effectIsOn = false;
    private void Awake()
    {
        timer = new WaitForSeconds(duration);
    }
    public void StartEffect()
    {
        if (effectIsOn) return;

        StartCoroutine(SmokingEffect());
    }

    IEnumerator SmokingEffect()
    {
        effectIsOn = true;
        foreach(ParticleSystem system in smokeParticles)
        {
            system.gameObject.SetActive(true);
            system.Play();
        }

        yield return timer;

        foreach (ParticleSystem system in smokeParticles)
        {
            system.Stop();
            system.gameObject.SetActive(false);
            
        }
        effectIsOn = false;
    }

    //public void SetRendererSortingLayer(string newLayerName)
    //{
    //    foreach(ParticleSystem system in smokeParticles)
    //    {
    //        system.ren
    //    }
    //}

}
