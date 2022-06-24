using UnityEngine;

namespace IJ.Animations.Objects
{
    public abstract class ShipAnimation : TweenAnimation
    {
        [SerializeField] private protected Transform _transform;
        [SerializeField] private protected Vector3 _initialPosition = new Vector3(41.5f, -20.74f, 0f);
        [SerializeField] private protected Vector3 _portPosition = new Vector3(12.58f, -17.62f, 0f);
        [SerializeField] private protected Vector3 _swingDelta = new Vector3(0.36f, 0.32f, 0f);        
        [SerializeField] private protected float _swingTimer = 2f;


        public abstract void SailIn();
        protected abstract void SwingOnWaves();
        public abstract void SailOut();
    }
}