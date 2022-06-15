using System.Collections;
using System.Collections.Generic;
using RepairSites;
using UnityEngine;
using IJ.Utilities;

public interface IEmergencyUnit
{
    public abstract void StopTimer();
}

public class VehicleUnit : MonoBehaviour
{
    public enum TYPE { car, fastCar, busCar, emergencyCar, taxi, repairCar, truck, train }
    [Header("Settings")]
    [SerializeField] private int stepsAfterCollision = 5;
    [SerializeField] private float trafficLightGapCorrector = 0.75f;
    [SerializeField] private float stayInCrashTimer = 2f;
    [SerializeField] private float velocitySensitivity = 0.01f;
    [SerializeField] private float minVectorDotProductValueToView = -0.8f; // -1 -- opposite 1 --the same
    [SerializeField] public bool needLogging = false;

    [Header("Rigidbody Parameters")]
    [SerializeField] private Rigidbody2D rigidbodyComponent;
    [SerializeField] private PolygonCollider2D colliderComponent;
    [SerializeField] private BoxCollider2D boxColliderComponent;
    [SerializeField] private Transform transformComponents;
    
    [Header("Scripts")]
    [SerializeField] private VehicleManager vehicleManager;
    [SerializeField] private VehicleScanner vehicleScanner;
    [SerializeField] private RepairCar repairCar;
    [SerializeField] private VEffects effectsControl;

    [Header("----- Info -----")]
    // Initial data
    [SerializeField] Vector2 size;
    public Vector2 Size { get => size; }
    [SerializeField] float maxVelocity, maxTurningVelocity, maxAcceleration, normalAcceleration, turningAcceleration, maxRepairSiteVelocity;
    public float MaxVelocity { get => maxVelocity; }

    // Moving
    List<Path> paths = new List<Path>(); // all paths from start to end point
    List<Vector2> points = new List<Vector2>(); // current path part points

    // FOR TEST START

    [SerializeField] List<float> distanceToOtherVehicles = new List<float>(); // for test
    [SerializeField] List<float> accelerationToOtherVehicles = new List<float>();// for test
    [SerializeField] List<float> sizeOfOtherVehicles = new List<float>();// for test
    [SerializeField] private float distanceToTrafficLight = 0f;// for test
    [SerializeField] private float accelerationToTrafficLight = 0f;// for test

    // FOR TEST END

    float t = 0f; // path part from 0 to 1
    [SerializeField] float velocity, acceleration, gap; // deceleration;
    public float Velocity { get => velocity; }

    [SerializeField] float totalTimeOnRoad = 0f;
    private Vector3 positionToHold = Vector3.zero;
    private Quaternion rotationToHold;
    
    int pathPartIndex = 0; // current path part index
    int managerIndex = 0;   
    public int ManagerIndex { get => managerIndex; }
    [SerializeField] private int roadStartPointNumber = -1; // road start point to count vehicle on road
    [SerializeField] private int collisionCounter = -1;


    [SerializeField] bool isLine = true; // is the current path a line --> calculate curve by line rule
    [SerializeField] bool isSimulating = false; // is this vehicle in simulation
    [SerializeField] bool inCrash = false;
    public bool InCrash { get => inCrash; }
    private bool needTimer = false;
    [SerializeField] bool isInRepairSide = false;
    [SerializeField] bool isInTurn = false;
    [SerializeField] bool stopInParking = false;

    private TYPE type;
    public TYPE Type { get => type; }
    private string vehicleName;
    private Vector2 repairSitePoint;


    private void Awake()
    {
        size = colliderComponent.bounds.extents;
    }

    private void FixedUpdate()
    {
        if (!isSimulating)
        {
            if (velocity != 0)
            {
                velocity = 0f;
                acceleration = 0f;
            }
            
            return;
        }

        MoveAlongCurve();

        if (collisionCounter == 0)
        {
            isSimulating = false;
            InCrashTimer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSimulating)
        {
            Logging.Log("VehicleUnit: collision!");
            
            // crash
            if (collision.gameObject.CompareTag("Car"))
            {
                Vector3 contactPosition = collision.contacts[0].point;
                // if train --> store correct position
                if (type == TYPE.train)
                {
                    positionToHold = transformComponents.position;
                    rotationToHold = transformComponents.rotation;

                    // check contact position to be in zone
                    //if (!effectsControl.CheckContactPositionToBeInCrashZone(contactPosition))
                    //{
                    //    return;
                    //}
                }

                // if in crossroads or target in crash
                VehicleUnit unit = collision.gameObject.GetComponent<VehicleUnit>();
                inCrash = true;

                collisionCounter = (type == TYPE.train) ? 0 : stepsAfterCollision;

                unit.StopSimulatingIfMovingInCollision(contactPosition - transformComponents.position);
                //vehicleManager.RegisterCrash(managerIndex, unit.ManagerIndex, contactPosition);
            }
            else if (collision.gameObject.CompareTag("Train"))
            {
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!inCrash && !isSimulating) isSimulating = true;
    }

    public void StopSimulatingIfMovingInCollision(Vector3 pushVector)
    {
        if (isSimulating) isSimulating = false;

        rigidbodyComponent.AddForce(pushVector.normalized * 2f, ForceMode2D.Impulse);
        collisionCounter = 0;
        inCrash = true;

        InCrashTimer();
    }

    public void SetVehicle(Vehicle newVehicle, List<Path> newPaths, VehicleManager newVehicleManager, int newManagerIndex, bool newStopIsParking = false)
    {
        managerIndex = newManagerIndex;
        vehicleManager = newVehicleManager;

        // Set vehicle
        if (newPaths == null)
        {
            Logging.Log("VehicleUnit: newPaths is null!");
            StopVehicle();
            return;
        }
        else if (newPaths.Count < 1)
        {
            Logging.Log("VehicleUnit: newPaths is empty!");
            StopVehicle();
            return;
        }
        // Parameters
        type = newVehicle.Type;
        vehicleName = newVehicle.VehicleName;
        maxVelocity = newVehicle.MaxVelocity;
        maxTurningVelocity = newVehicle.MaxTurningVelocity;
        maxAcceleration = newVehicle.MaxAcceleration;
        normalAcceleration = newVehicle.NormalAcceleration;
        turningAcceleration = newVehicle.TurningAcceleration;

        rigidbodyComponent.mass = newVehicle.Mass;
        rigidbodyComponent.drag = 2f;

        // Sprites
        if (newVehicle.Packs != null)
        {
            int index = Random.Range(0, newVehicle.Packs.Length);
            //effectsControl.SetVehicleView(newVehicle.Packs[index]);
        }
        
        //effectsControl.SetDefaultSortingLayer();


        // new paths
        paths = newPaths;
        pathPartIndex = -1;

        if (paths[0].CurvePoints.Count == 0)
        {
            Logging.Log("VehicleUnit: newPaths curve is EMPTY!");
            StopVehicle();
            return;
        }
        // set on start point
        transformComponents.position = paths[0].CurvePoints[0];
        velocity = maxVelocity / 2;
        acceleration = 0f;
        collisionCounter = -1;
        totalTimeOnRoad = 0f;
        inCrash = false;
        needTimer = true;
        isInRepairSide = false;
        isInTurn = false;
        stopInParking = newStopIsParking;
        roadStartPointNumber = paths[0].StartPointNumber;

        // set scanner
        gap = newVehicle.Gap;
        vehicleScanner.EnableCollider();
        vehicleScanner.SetScanner(gap, CalculateDistance(maxVelocity, 0f, normalAcceleration));

        // set timer if needed
        if (type == TYPE.emergencyCar)
        {
            //effectsControl.TimerEnableOrNot(true);
            //effectsControl.SetNewVehicleParametersForTimer(newVehicle as EmergencyCar, vehicleManager);
        }

        //effectsControl.TurnOnOffLights(true);

        // cargo for trucks
        //if (type == TYPE.truck) effectsControl.MakeCargo(true);

        gameObject.SetActive(true);
        StartNewPathsPart();
    }

    void StartNewPathsPart()
    {
        Logging.Log("VehicleUnit: start new path part ... ");
        // stop current turning effects
        if (pathPartIndex >= 0 && paths[pathPartIndex].Turn != Path.TURN.none)
        {
            //effectsControl.StopTurningEffect();
        }

        pathPartIndex++;

        if (pathPartIndex >= paths.Count)
        {
            // route is ended
            StopVehicle();
            return;
        }

        // next path
        points = paths[pathPartIndex].CurvePoints;
        isLine = (points.Count == 2);
        isInTurn = (paths[pathPartIndex].Turn != Path.TURN.none);
        t = 0f;

        // start current turning effects
        if (paths[pathPartIndex].Turn != Path.TURN.none)
        {
            isLine = false;
            //effectsControl.StartTurningEffect(paths[pathPartIndex].Turn);    
        }

        isSimulating = true;
    }


    void MoveAlongCurve()
    {
        float prevVelocity = velocity;

        if (isInTurn)
        {
            acceleration = turningAcceleration;
        }
        else
        {
            acceleration = normalAcceleration;
        }

        if (vehicleScanner.NeedVehicleControl())
        {
            // correct acceleration according to vehicles

            float vehicleAcceleration = CalculateAccelerationForVehicles(vehicleScanner.GetDataAboutVehicles());
            if (needLogging) Logging.Log("VehicleUnit: acceleration " + acceleration + " newAcceleration " + vehicleAcceleration);
            acceleration = vehicleAcceleration;
        }

        if (vehicleScanner.NeedTrafficLightControl())
        {
            // correct acceleration according to traffic lights
            float trafficLightAcceleration = CalculateAccelerationForTrafficLight(vehicleScanner.GetDataAboutTrafficLight());
            if (trafficLightAcceleration < acceleration)
            {
                //Logging.Log("VehicleUnit: acceleration " + acceleration + " newAcceleration " + trafficLightAcceleration);
                acceleration = trafficLightAcceleration;
            }
                
        }
        else
        {
            distanceToTrafficLight = -1000f;
            accelerationToTrafficLight = -1000f;
        }

        if (isInRepairSide)
        {
            float repairSiteAcceleration = CalculateAccelerationForRepairSite();
            if (repairSiteAcceleration < acceleration)
            {
                //Logging.Log("VehicleUnit: acceleration " + acceleration + " newAcceleration " + trafficLightAcceleration);
                acceleration = repairSiteAcceleration;
            }

            if (velocity == 0 && type == TYPE.repairCar && repairCar != null)
            {
                repairCar.GoToTargetRepairSite();
            }
        }

        velocity += (acceleration * Time.deltaTime);

        if (needTimer) totalTimeOnRoad += Time.deltaTime;

        if (velocity < velocitySensitivity)
        {
            velocity = 0;
            //acceleration = 0f;

            // update timer
            //if (type == TYPE.emergencyCar) effectsControl.CheckNewVelocityForTimer(velocity, Time.fixedDeltaTime);

            return;
        }
        else if (isInTurn && velocity > maxTurningVelocity)
        {
            velocity = maxTurningVelocity;
        }
        else if (velocity > maxVelocity)
        {
            velocity = maxVelocity;
        }

        // update timer
        //if (type == TYPE.emergencyCar) effectsControl.CheckNewVelocityForTimer(velocity, Time.fixedDeltaTime);

        //collisionCounter--;

        if (collisionCounter > 0) collisionCounter--;

        if (Mathf.Abs(prevVelocity - velocity) == 0 && velocity == 0)
        {
            Logging.Log("VehicleUnit: zero step!");
            return;
        }

        t += (float)Time.fixedDeltaTime * velocity / paths[pathPartIndex].CurveLength;

        // move to endPoint
        if (Mathf.Abs(1f - t) < 0.001f || t > 1) t = 1f;

        Vector2 newPoint;

        // get new point
        if (isLine)
        {
            newPoint = Vector2.Lerp(points[0], points[1], t);
        }
        else
        {
            newPoint = Curves.GetBezierPoint(t, points);
        }

        Quaternion newRotation = Quaternion.Euler(transformComponents.rotation.x, transformComponents.rotation.y, 360f - Mathf.Atan2(newPoint.x - transformComponents.position.x, newPoint.y - transformComponents.position.y) * Mathf.Rad2Deg);


        //Logging.Log("TEST: t is " + t + " point is: x " + newPoint.x + " y " + newPoint.y + " angle is " + (360f - Mathf.Atan2(newPoint.x - transformComponents.position.x, newPoint.y - transformComponents.position.y) * Mathf.Rad2Deg) + "velocity " + velocity);

        //Vector3 delta = new Vector3(newPoint.x - transformComponents.position.x, newPoint.y - transformComponents.position.y, 0f);
        //float angle = Mathf.Atan2(newPoint.x - transformComponents.position.x, newPoint.y - transformComponents.position.y) * Mathf.Rad2Deg;
        transformComponents.Translate(new Vector3(newPoint.x - transformComponents.position.x, newPoint.y - transformComponents.position.y, 0f), Space.World);
        //transformComponents.position = newPoint;
        //transformComponents.Rotate(newRotation.eulerAngles - transformComponents.rotation.eulerAngles, 1 * Time.deltaTime);
        //Debug.Log("Angle " + angle);
        transformComponents.Rotate(newRotation.eulerAngles - transformComponents.rotation.eulerAngles, Space.World);
        //transformComponents.rotation = newRotation;

        //if (vehicleTimer != null)
        //{
        //    vehicleTimer.CorrectMarkRotationToStayVertical(Quaternion.Euler(transformComponents.rotation.x, transformComponents.rotation.y, - transform.rotation.z));
        //}

        if (t >= 1)
        {
            isSimulating = false;
            StartNewPathsPart();
        }
    }


    public float CalculateAccelerationForTrafficLight(Vector2 trafficLightPoint)
    {
        float distance = Vector2.Distance(transformComponents.position, trafficLightPoint) - gap * trafficLightGapCorrector - size.y;
        distanceToTrafficLight = distance;
        float newAcceleration = (Mathf.Pow(0f, 2) - Mathf.Pow(velocity, 2)) / (2 * (distance));
        accelerationToTrafficLight = newAcceleration;
        if (Mathf.Abs(newAcceleration) > maxAcceleration) newAcceleration = Mathf.Sign(newAcceleration) * maxAcceleration;

        return newAcceleration;
    }

    public float CalculateAccelerationForVehicles(List<VehicleUnit> vehicles)
    {
        distanceToOtherVehicles.Clear();
        accelerationToOtherVehicles.Clear();
        sizeOfOtherVehicles.Clear();
       
        float maxPossibleAcceleration = maxAcceleration;
        float newAcceleration = normalAcceleration;
        
        if (isInTurn)
        {
            maxPossibleAcceleration = turningAcceleration;
            newAcceleration = turningAcceleration;
        }

        float distance = 1000f;
        float velocityOfNearestVehicle = 0f;
        
        List<float> distanceToVehicleWithZeroVelocity = new List<float>();
        List<VehicleUnit> removeDueToDistance = new List<VehicleUnit>();

        accelerationToOtherVehicles.Add(newAcceleration);

        foreach (VehicleUnit vehicleUnit in vehicles)
        {
            // calculate angle and dot product

            Vector2 unitVector = vehicleUnit.GetSetToOriginVelocityVector();
            Vector2 selfVector = GetSetToOriginVelocityVector();

            if (Vector2.Dot(selfVector.normalized, unitVector.normalized) < minVectorDotProductValueToView)
            {
                continue;
            }

            float angle = Vector2.Angle(GetSetToOriginVelocityVector(), vehicleUnit.GetSetToOriginVelocityVector());
            float angleToInterpolation = angle % 90f;

            float vehicleSize = Mathf.Lerp(vehicleUnit.size.y, vehicleUnit.size.x, angleToInterpolation / 90f);

            float newDistance = Vector2.Distance(transformComponents.position, vehicleUnit.transform.position) - gap - vehicleSize - size.y;
            distanceToOtherVehicles.Add(newDistance);
            newDistance = (newDistance < 0) ? 0 : newDistance;

            sizeOfOtherVehicles.Add(angleToInterpolation);

            if (vehicleUnit.Velocity < velocitySensitivity)
            {
                distanceToVehicleWithZeroVelocity.Add(newDistance);
            }

            if (newDistance < distance)
            {
                distance = newDistance;
                velocityOfNearestVehicle = vehicleUnit.Velocity;
            }

            if (newDistance > vehicleScanner.MaxDistanceToBeFollowed)
            {
                removeDueToDistance.Add(vehicleUnit);
            }

        }

        float temp = (distance != 0) ? (Mathf.Pow(velocityOfNearestVehicle, 2) - Mathf.Pow(velocity, 2)) / (2 * distance) : -1 * maxPossibleAcceleration;
        newAcceleration = Mathf.Min(temp, newAcceleration);

        accelerationToOtherVehicles.Add((Mathf.Pow(velocityOfNearestVehicle, 2) - Mathf.Pow(velocity, 2)) / (2 * distance));

        foreach (float newDistance in distanceToVehicleWithZeroVelocity)
        {
            if (velocity > velocitySensitivity)
            {
                newAcceleration = (newDistance != 0) ? Mathf.Min((Mathf.Pow(0, 2) - Mathf.Pow(velocity, 2)) / (2 * newDistance), newAcceleration) : -1 * maxPossibleAcceleration;
                accelerationToOtherVehicles.Add(newAcceleration);
            }
            else if (newDistance > 0)
            {
                newAcceleration = turningAcceleration;
                accelerationToOtherVehicles.Add(turningAcceleration);
            }
            
        }
        
        if (Mathf.Abs(newAcceleration) > maxAcceleration) newAcceleration = Mathf.Sign(newAcceleration) * maxAcceleration;

        if (removeDueToDistance.Count > 0)
        {
            foreach(VehicleUnit unit in removeDueToDistance)
            {
                vehicleScanner.RemoveVehicleFormFollowedDueToMaxDistance(unit);
            }
        }
        return newAcceleration;
    }

    public void StopTimer()
    {
        if (needTimer) needTimer = false;
    }

    #region("repair site")

    public void EnterRepairSite(RepairSite repairSite)
    {
        isInRepairSide = true;
        //maxRepairSiteVelocity = repairSite.TargetVelocity;

        if (type == TYPE.repairCar)
        {
            if (repairCar.RepairSiteIndex == repairSite.RepairSiteIndex)
                maxRepairSiteVelocity = 0f;
        }

        //repairSitePoint = repairSite.TargetPoint;
    }

    public void ExitRepairSite(RepairSite repairSite)
    {
        if (type == TYPE.repairCar && repairSite != null && repairCar != null)
        {
            if (repairCar.RepairSiteIndex == repairSite.RepairSiteIndex && !repairCar.RepairIsMade)
            {
                repairCar.GoToTargetRepairSite();
            }
            else
            {
                isInRepairSide = false;
            }
        }
        else
        {
            if (type == TYPE.repairCar) Logging.Log("VehicleUnit: exit repair site!");
            isInRepairSide = false;
        }
    }

    public float CalculateAccelerationForRepairSite()
    {
        float distance = Vector2.Distance(transformComponents.position, repairSitePoint);
        float newAcceleration = (Mathf.Pow(maxRepairSiteVelocity, 2) - Mathf.Pow(velocity, 2)) / (2 * (distance));
        if (Mathf.Abs(newAcceleration) > maxAcceleration) newAcceleration = Mathf.Sign(newAcceleration) * maxAcceleration;

        return newAcceleration;
    }

    #endregion

    #region("effects&info")

    public int GetCurrentPathPartPointNumbers(bool needStartPoint = true)
    {
        if (needStartPoint)
        {
            return paths[pathPartIndex].StartPointNumber;
        }
        else
        {
            return paths[pathPartIndex].EndPointNumber;
        }
    }

    public void StartTurningLightEffectWhenDetectCrossroads()
    {
        if (pathPartIndex >= 0 && pathPartIndex + 1 < paths.Count)
        {
            if (paths[pathPartIndex + 1].Turn != Path.TURN.none)
            {
                //effectsControl.StartTurningEffect(paths[pathPartIndex + 1].Turn);
            }
        }
    }

    float CalculateDistance(float currentVelocity, float targetVelocity, float currentAcceleration)
    {
        float timeUntilStop = Mathf.Abs((targetVelocity - currentVelocity) / currentAcceleration);
        return currentVelocity * timeUntilStop + currentAcceleration * timeUntilStop * timeUntilStop / 2;
    }

    public void EnterOrExitTurn(bool isInTurnNow)
    {
        isInTurn = isInTurnNow;
    }

    public void ResetRoadStartPointNumber()
    {
        roadStartPointNumber = -1;
    }

    public void SetRoadStartPointNumber(int number)
    {
        roadStartPointNumber = number;
    }

    public bool CheckUnitForDirectionAndRoad(int index, int checkRoadStartPointNumber, VehicleScanner.DIRECTION direction)
    {
        Logging.Log("VehicleUnit: need be with direction " + (vehicleScanner.Direction == direction).ToString() + " start point " + (roadStartPointNumber == checkRoadStartPointNumber).ToString() + " index is " +
            (managerIndex != index).ToString());
        return managerIndex != index && roadStartPointNumber == checkRoadStartPointNumber && vehicleScanner.Direction == direction;
    }

    public List<VehicleUnit> GetUnitsWhenChangeDirection()
    {
        //return vehicleManager.GetUnitsToAddToVehicleToFollow(vehicleScanner.Direction, roadStartPointNumber, managerIndex);
        return null;
    }

    public float GetTotalTimeOnRoad()
    {
        needTimer = false;
        return totalTimeOnRoad;
    }

    public Vector2 GetSetToOriginVelocityVector()
    {
        return transformComponents.position - boxColliderComponent.bounds.center;
    }

    #endregion

    #region("crash")

    void InCrashTimer()
    {
        if (!inCrash) return;

        //effectsControl.MakeWheelSmoke();

        //if (type == TYPE.emergencyCar) effectsControl.HideMarkAndTimerForTimer();

        needTimer = false;
        StartCoroutine(StayInCrash());
    }

    IEnumerator StayInCrash()
    {
        yield return new WaitForSeconds(stayInCrashTimer);
        if (type != TYPE.train) StopVehicle();
        else
        {
            RestartAfterCollision();
        }
    }

    void RestartAfterCollision()
    {
        //vehicleManager.RemoveCrashByVehicleIndex(managerIndex);

        transform.position = positionToHold;
        transform.rotation = rotationToHold;
        collisionCounter = -1;

        inCrash = false;
        isSimulating = true;
    }

    #endregion

    void StopVehicle()
    {
        // Stop vehicle
        if (inCrash || !stopInParking)
        {
            gameObject.SetActive(false);
        }
        else
        {
            //effectsControl.TurnOnOffLights(false);
        }
        
        vehicleScanner.DisableCollider();
        needTimer = false;

        if (inCrash)
        {
            //vehicleManager.RemoveCrashByVehicleIndex(managerIndex);

            // remove by road timer
            if (pathPartIndex < paths.Count && roadStartPointNumber != -1)
            {
                //vehicleManager.RemoveVehicleFromUnitsOnRoad(roadStartPointNumber, totalTimeOnRoad, type);
            }

            // free end point with parking
            if (stopInParking)
            {
                //vehicleManager.FreeEndPointWithParkingInCrash(paths[paths.Count - 1].EndPointNumber);
            }
        }

        if (type == TYPE.emergencyCar)
        {
            //effectsControl.TimerEnableOrNot(false);
        }
        else if (type == TYPE.repairCar && repairCar != null)
        {
            repairCar.StopVehicle();
        }

        //f (inCrash || !stopInParking) vehicleManager.AddVehicleToFree(managerIndex, type, vehicleName.GetHashCode());
       
        Logging.Log("VehicleUnit: route is finished!");
    }
}
