using UnityEngine;

public class AirBladesAnimation : MonoBehaviour
{
    [SerializeField] private float blendParameter = 0;

    private void Awake()
    {
        if (blendParameter == 0.01f) blendParameter = 0;

        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetFloat("Blend", blendParameter);
        }
    }
}
