using UnityEngine;

public class TurningEffect : MonoBehaviour
{    
    [Header("Front Lights")]
    [SerializeField] private VehicleLightEffect leftFrontLightEffect;
    [SerializeField] private VehicleLightEffect rightFrontLightEffect;

    [Header("Effects Colors")]
    [SerializeField] private Color turningColor;

    [Header("Parameters")]
    [SerializeField] private float turningPeriod = 0.5f;

    Path.TURN currentTurn = Path.TURN.none;

    public void SetSortingLayerById(int id)
    {
        if (leftFrontLightEffect != null) leftFrontLightEffect.SetSortingLayerById(id);
        if (rightFrontLightEffect != null) rightFrontLightEffect.SetSortingLayerById(id);
    }

    public void TurnOnOffLights(bool mode)
    {
        StopTurningEffect();
        leftFrontLightEffect.TurnOnOffLights(mode);
        rightFrontLightEffect.TurnOnOffLights(mode);
    }

    public void StartTurningEffect(Path.TURN turn) // mode 0 - leftTurn, mode 1 - rightTurn
    {
        if (currentTurn != Path.TURN.none || turn == Path.TURN.none)
        {
            return;
        }
        else if (turn == Path.TURN.leftBend || turn == Path.TURN.rightBend) return;

        currentTurn = turn;
        if (currentTurn == Path.TURN.left)
        {
            // make left turning effect
            leftFrontLightEffect.StartTurningEffect(turningColor, turningPeriod);
        }
        else if (currentTurn == Path.TURN.right)
        {
            // make right turning effect
            rightFrontLightEffect.StartTurningEffect(turningColor, turningPeriod);
        }
    }

    public void StopTurningEffect()
    {
        if (currentTurn == Path.TURN.none)
        {
            return;
        }
        else if (currentTurn == Path.TURN.left)
        {
            // stop left turning effect
            leftFrontLightEffect.StopTurningEffect();
        }
        else if (currentTurn == Path.TURN.right)
        {
            // stop right turning effect
            rightFrontLightEffect.StopTurningEffect();
        }

        currentTurn = Path.TURN.none;
    }
}
