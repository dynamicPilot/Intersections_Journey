using AudioControls.SoundPlayers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Core.Objects.CrashEffects
{
    public class CrashEffects : MonoBehaviour
    {
        [Header("Paricles")]
        [SerializeField] private ParticleSystem[] crashParticles;
        [SerializeField] private float effectDuration = 2.1f;

        List<bool> particlesState = new List<bool>();
        private void Awake()
        {
            foreach (ParticleSystem system in crashParticles) particlesState.Add(true);
        }

        public void StartCrashEffect(Vector2 position)
        {
            int index = GetFreeEffectIndex();

            if (index < 0) return;

            particlesState[index] = false;
            StartCoroutine(CrashEffect(index, position));
        }

        IEnumerator CrashEffect(int index, Vector2 position)
        {
            crashParticles[index].GetComponent<Transform>().position = position;
            crashParticles[index].gameObject.SetActive(true);
            crashParticles[index].Play();

            SoundsPlayer player = crashParticles[index].GetComponent<SoundsPlayer>();
            player.PlaySound(0);

            yield return new WaitForSeconds(effectDuration);

            crashParticles[index].Stop();
            crashParticles[index].gameObject.SetActive(false);
            particlesState[index] = true;
            player.StopPlaying();
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
}
