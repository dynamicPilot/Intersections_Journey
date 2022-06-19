using DG.Tweening;
using UnityEngine;

namespace IJ.Animations
{
    public class TweenAnimation : MonoBehaviour
    {
        private void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}
