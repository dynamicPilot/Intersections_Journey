using UnityEngine;


public class VTrainCrasher : VCrasher
{
    private Vector3 positionToHold = Vector3.zero;
    private Quaternion rotationToHold;

    
    public override void CollisionWithCar(Collision2D collision, Vector3 contactPosition)
    {
        // analyze and make working
        //if (!effectsControl.CheckContactPositionToBeInCrashZone(contactPosition)) return;
        
        positionToHold = transform.position;
        rotationToHold = transform.rotation;

        base.CollisionWithCar(collision, contactPosition);
    }

    public override void AfterCrash()
    {
        //vehicleManager.RemoveCrashByVehicleIndex(managerIndex);

        transform.position = positionToHold;
        transform.rotation = rotationToHold;

        //inCrash = false;
        //isSimulating = true;
    }
}
