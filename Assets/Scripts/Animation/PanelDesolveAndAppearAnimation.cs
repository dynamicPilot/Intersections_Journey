using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace IJ.Animations
{
    public class PanelDesolveAndAppearAnimation : TweenAnimation
    {
        [SerializeField] private CanvasGroup _group;

        [Header("Settings")]
        [SerializeField] private float _duration = 0.5f;

        private void Awake()
        {
            Appear();
        }
        public void Appear()
        {
            _group.alpha = 0;
            _group.DOFade(1, _duration);
        }
    }
}
