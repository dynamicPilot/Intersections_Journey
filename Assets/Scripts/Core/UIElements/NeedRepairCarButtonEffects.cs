using IJ.Animations.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Core.UIElements.NeedRepairCar
{
    [RequireComponent(typeof(MarkAnimation))]
    [RequireComponent(typeof(NeedRepairCarSound))]
    public class NeedRepairCarButtonEffects : MonoBehaviour
    {
        enum STATE { sleep, appearance, disappearance, inAction }
        
        [SerializeField] private MarkAnimation _animation;
        [SerializeField] private NeedRepairCarSound _sounds;

        STATE _state = STATE.sleep;
        List<STATE> _queue = new List<STATE>();

        private void Awake()
        {
            _state = STATE.sleep;
        }

        public void StartButton()
        {
            _queue.Add(STATE.appearance);
            MoveToNextState();
        }


        void MoveToNextState()
        {
            if (_state != STATE.sleep || _queue.Count == 0) return;

            _state = _queue[0];
            _queue.RemoveAt(0);

            if (_state == STATE.appearance)
            {
                _queue.Add(STATE.inAction);
                gameObject.SetActive(true);
                StartCoroutine(BlockForAction(_animation.Appearance()));
            }
            else if (_state == STATE.inAction)
            {
                _animation.InAction();
                _sounds.StartSound();
            }
            else if (_state == STATE.disappearance)
            {
                _queue.Clear();
                _sounds.StopSound();
                StartCoroutine(BlockForAction(_animation.Disappearance()));
            }
            
        }

        public void StopButton()
        {
            _queue.Add(STATE.disappearance);
            _state = STATE.sleep;
            MoveToNextState();
        }

        IEnumerator BlockForAction(float timeToBlock)
        {
            yield return new WaitForSeconds(timeToBlock);
            gameObject.SetActive(_state != STATE.disappearance);
            _state = STATE.sleep;
            MoveToNextState();
        }
    }
}

