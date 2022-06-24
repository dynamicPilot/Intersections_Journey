using AudioControls.SoundPlayers;
using IJ.Animations.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimatorControl : MonoBehaviour
{
    [SerializeField] private PlaneAnimation[] _planeAnimations;
    [SerializeField] private float _updateInterval = 10f;

    [SerializeField] private SoundsPlayer _player;
    [SerializeField] private int _soundIndex = 1;

    WaitForSeconds _timer;
    List<int> _activePlaneAnimations = new List<int>();
    int _counter;
    bool _makeSound = true;

    private void Awake()
    {
        _counter = 0;
        _makeSound = true;
        _timer = new WaitForSeconds(_updateInterval);
        StartCoroutine(PlaneSpawner());
    }

    IEnumerator PlaneSpawner()
    {
        yield return _timer;

        SpawnNextPlane();
        if (_counter < _planeAnimations.Length)
        {
            StartCoroutine(PlaneSpawner());
        }
        else
        {
            StartCoroutine(PlaneGetOut());
        }
    }

    IEnumerator PlaneGetOut()
    {
        yield return _timer;

        GetOutNextPlane();
        if (_activePlaneAnimations.Count > 0) StartCoroutine(PlaneGetOut());
        else
        {
            _counter = 0;
            StartCoroutine(PlaneSpawner());
        }
    }

    void SpawnNextPlane()
    {
        if (_counter < _planeAnimations.Length)
        {
            _planeAnimations[_counter].gameObject.SetActive(true);
            _planeAnimations[_counter].ToStand();
            _activePlaneAnimations.Add(_counter);
            _counter++;

            MakeSound();
        }
    }

    void GetOutNextPlane()
    {
        if (_activePlaneAnimations.Count > 0)
        {
            int index = _activePlaneAnimations[0];           
            _planeAnimations[index].FromStand();
            _activePlaneAnimations.RemoveAt(0);            
        }
    }

    void MakeSound()
    {
        if (_makeSound) _player.PlaySound(_soundIndex);
        _makeSound = !_makeSound;
    }
}
