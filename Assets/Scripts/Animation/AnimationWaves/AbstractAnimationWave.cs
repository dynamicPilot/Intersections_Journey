using UnityEngine;

namespace IJ.Animations.Waves
{
    [System.Serializable]
    public abstract class AbstractAnimationWave
    {
        [SerializeField] private float _delayToNext;
        public float DelayToNext { get => _delayToNext; }

        [SerializeField] private GameObject[] _memberObjects;

        private protected IAnimationWaveMember[] _members;

        public abstract void SetInitialState();
        public abstract void StartWave();

        protected void GetMemebersFromObjects()
        {
            _members = new IAnimationWaveMember[_memberObjects.Length];

            for (int i = 0; i < _memberObjects.Length; i++)
            {
                _members[i] = _memberObjects[i].GetComponent<IAnimationWaveMember>();
            }
        }
    }
}
