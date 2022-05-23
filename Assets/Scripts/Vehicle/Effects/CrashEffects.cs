using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] crashParticles;
    [SerializeField] private float effectDuration = 2.1f;

    List<bool> particlesState = new List<bool>();

    private void Awake()
    {
        foreach (ParticleSystem system in crashParticles) particlesState.Add(true);
    }

    public void StartCrashEffect(Vector2 position)
    {
        Logging.Log("CrashEffect: starting effect....");
        int index = GetFreeEffectIndex();

        if (index < 0) return;

        Logging.Log("CrashEffect: .... index " + index);
        particlesState[index] = false;
        StartCoroutine(CrashEffect(index, position));
    }

    IEnumerator CrashEffect(int index, Vector2 position)
    {
        crashParticles[index].gameObject.GetComponent<Transform>().position = position;
        crashParticles[index].gameObject.SetActive(true);
        crashParticles[index].Play();

        yield return new WaitForSeconds(effectDuration);

        crashParticles[index].Stop();
        crashParticles[index].gameObject.SetActive(false);
        particlesState[index] = true;

    }

    int GetFreeEffectIndex()
    {
        return particlesState.IndexOf(true);
    }

    public void StopAllEffects()
    {
        StopAllCoroutines();
        foreach (ParticleSystem system in crashParticles)
        {
            system.gameObject.SetActive(false);
            particlesState.Add(true);
        }

    }
}
