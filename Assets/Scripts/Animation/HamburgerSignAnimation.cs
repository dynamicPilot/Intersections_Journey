using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerSignAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _stripe0;
    [SerializeField] private RectTransform _stripe1;
    [SerializeField] private RectTransform _stripe2;
    [Header("Settings")]
    [SerializeField] private float _duration = 1f;

    private float _angleAbs = 50;
    private float _initialPosYAbs = 32;
    private float _fadeDuration;

    private void Awake()
    {
        _fadeDuration = 0.83f * _duration;
    }

    public void ToQuitSign()
    {
        _stripe0.DORotate(new Vector3(0f, 0f, _angleAbs * -1), _duration);
        _stripe0.DOLocalMoveY(0f, _duration);

        _stripe1.DORotate(new Vector3(0f, 90f, 0f), _fadeDuration);

        _stripe2.DORotate(new Vector3(0f, 0f, _angleAbs), _duration);
        _stripe2.DOLocalMoveY(0f, _duration);
    }

    public void ToHamburgerSign()
    {        
        _stripe0.DORotate(new Vector3(0f, 0f, 0f), _duration);
        _stripe0.DOLocalMoveY(_initialPosYAbs, _duration);

        Sequence sequence = DOTween.Sequence();

        sequence.PrependInterval(_duration - _fadeDuration)
            .Append(_stripe1.DORotate(new Vector3(0f, 0f, 0f), _fadeDuration));

        _stripe2.DORotate(new Vector3(0f, 0f, 0f), _duration);
        _stripe2.DOLocalMoveY(-1 * _initialPosYAbs, _duration);
    }
}
