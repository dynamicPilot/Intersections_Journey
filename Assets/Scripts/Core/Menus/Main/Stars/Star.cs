using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public enum STATE { full, empty }

    [Header("Sprites")]
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite starBorders;

    [Header("Components")]
    [SerializeField] private Image image;

    [SerializeField] STATE state = STATE.empty;
    //private void Awake()
    //{
    //    state = STATE.empty;
    //    SetStarState();
    //}
    public void SetStarNewState(STATE newState)
    {
        if (state == newState)
        {
            return;
        }

        state = newState;
        SetStarState();
    }

    void SetStarState()
    {
        if (state == STATE.empty)
        {
            image.sprite = starBorders;
        }
        else
        {
            image.sprite = fullStar;
        }
    }

}
