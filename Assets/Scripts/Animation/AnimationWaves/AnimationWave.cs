using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Animations.Waves
{
    public interface IAnimationWaveMember
    {
        public void OnWaveStart(AnimationPath path);
    }

    [System.Serializable]
    public class AnimationWave
    {
        [SerializeField] private float _delayToNext;
        public float DelayToNext { get => _delayToNext; }

        [SerializeField] private GameObject[] _memberObjects;
        [SerializeField] private AnimationPath[] _paths;

        private IAnimationWaveMember[] _members;

        public void StartWave()
        {
            if (_members == null || _members.Length < 1) GetMemebersFromObjects();

            for (int i = 0; i < _members.Length; i++)
            {
                _members[i].OnWaveStart(_paths[i]);
            }
        }

        void GetMemebersFromObjects()
        {
            _members = new IAnimationWaveMember[_memberObjects.Length];

            for (int i = 0; i < _memberObjects.Length; i++)
            {
                _members[i] = _memberObjects[i].GetComponent<IAnimationWaveMember>();
            }
        }
    }
}
