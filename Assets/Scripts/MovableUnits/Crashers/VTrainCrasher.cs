using UnityEngine;

[RequireComponent(typeof(TrainEffects))]
public class VTrainCrasher : VCrasher
{
    private TrainEffects _trainEffects;

    private void Awake()
    {
        _trainEffects = GetComponent<TrainEffects>();
        base.OnAwake();
    }
    public override void CollisionWithCar(Collision2D collision, Vector3 contactPosition)
    {
        if (!_trainEffects.IsContactPointInCrashZone(contactPosition)) return;
       
        base.CollisionWithCar(collision, contactPosition);
    }

    public override void AfterCrash()
    {
        base.AfterCrash();
    }
}
