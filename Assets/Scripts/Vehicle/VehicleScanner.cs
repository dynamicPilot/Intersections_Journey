using UnityEngine;
using System.Collections.Generic;

public class VehicleScanner : MonoBehaviour
{
    public enum DIRECTION { nord, west, south, east, none, south_east, nord_east, nord_west, south_west }
    [SerializeField] private VehicleUnit vehicleUnit;
    [SerializeField] private float scanVisibilityGap = 1f;
    [SerializeField] private BoxCollider2D boxColliderComponent;

    [Header("----- Info -----")]  
    [SerializeField] private TrafficLight trafficLightToFollow; // traffic light to control
    [SerializeField] private List<VehicleUnit> vehicleUnitsToFollow = new List<VehicleUnit>(); // vehicle unit to control

    [SerializeField] private List<VehicleUnit> vehicleUnitsToChangeStateWhenDirectionNone = new List<VehicleUnit>(); // vehicle units state to make when direction becomes NONE
    [SerializeField] private List<bool> vehicleUnitsStateWhenDirectionNone = new List<bool>();
    
    [SerializeField] private List<VehicleUnit> vehicleUnitsToAddBeforeDirectionChange = new List<VehicleUnit>();

    [SerializeField] private bool haveTrafficLightToFollow = false;
    //public bool HaveTrafficLightToFollow { get => haveTrafficLightToFollow; }
    [SerializeField] private bool haveVehicleToFollow = false;
    [SerializeField] private bool isIntoCrossroads = false;
    public bool IsIntoCrossroads { get => isIntoCrossroads; }

    [SerializeField] private DIRECTION direction = DIRECTION.none;
    public DIRECTION Direction { get => direction; }
    private float colliderGap;
    [SerializeField] float maxDistanceToBeFollowed = 0f;
    public float MaxDistanceToBeFollowed { get => maxDistanceToBeFollowed; }
    private Vector2 initialColliderSize;

    private bool needCarToFollowDirectionControl = false;

    private void Awake()
    {
        initialColliderSize = boxColliderComponent.size;
    }

    public void SetScanner(float newVehicleGap, float newStoppingDistance)
    {
        direction = DIRECTION.none;
        needCarToFollowDirectionControl = false;

        // return to initials
        EnableCollider();
        isIntoCrossroads = false;

        haveTrafficLightToFollow = false;
        trafficLightToFollow = null;

        haveVehicleToFollow = false;
        vehicleUnitsToFollow.Clear();

        vehicleUnitsToAddBeforeDirectionChange.Clear();

        vehicleUnitsToChangeStateWhenDirectionNone.Clear();        
        vehicleUnitsStateWhenDirectionNone.Clear();

        // set collider
        colliderGap = newVehicleGap + newStoppingDistance + scanVisibilityGap;
        
        if (vehicleUnit.needLogging)
        {
            Logging.Log("VehicleScanner: collider gap " + colliderGap);
        }

        boxColliderComponent.size = new Vector2 (initialColliderSize.x, initialColliderSize.y + colliderGap);
        boxColliderComponent.offset = new Vector2(0, colliderGap / 2);

        maxDistanceToBeFollowed = (initialColliderSize.y + colliderGap) * transform.localScale.x;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        // Detect trafficLight
        if (!haveTrafficLightToFollow && collision.gameObject.CompareTag("TrafficLight") && !isIntoCrossroads) 
        {
            // Check point
            TrafficLight trafficLight = collision.gameObject.GetComponent<TrafficLight>();
            if (trafficLight == null) return;
            else if (!trafficLight.CheckPointNumber(vehicleUnit.GetCurrentPathPartPointNumbers(true)) &&
                !trafficLight.CheckPointNumber(vehicleUnit.GetCurrentPathPartPointNumbers(false)))
            {
                return;
            }


            // keep trafficLight
            trafficLightToFollow = collision.gameObject.GetComponent<TrafficLight>();
            haveTrafficLightToFollow = (trafficLightToFollow != null);
        }

        // if train --> return
        //if (vehicleUnit.Type == VehicleUnit.TYPE.train) return;

        // Detect vehicle
        if ((collision.gameObject.CompareTag("Car") && collision is PolygonCollider2D && vehicleUnit.Type != VehicleUnit.TYPE.train) ||
            (collision.gameObject.CompareTag("Train") && collision is PolygonCollider2D && vehicleUnit.Type == VehicleUnit.TYPE.train))
        {
            // check that on path if direction is not NONE
            if (direction != DIRECTION.none)
            {
                // compaire directions
                VehicleScanner vehicleScanner = collision.gameObject.GetComponent<VehicleScanner>();
                if (vehicleScanner != null)
                {
                    if (vehicleScanner.Direction != direction)
                    {
                        return;
                    }
                }
            }

            // keep vehicle
            VehicleUnit vehicleUnit = collision.gameObject.GetComponent<VehicleUnit>();
            if (vehicleUnit != null)
            {
                haveVehicleToFollow = true;
                if (!vehicleUnitsToFollow.Contains(vehicleUnit)) vehicleUnitsToFollow.Add(vehicleUnit);

                // keep to add when direction becomes NONE
                if (needCarToFollowDirectionControl)
                {
                    vehicleUnitsToChangeStateWhenDirectionNone.Add(vehicleUnit);
                    vehicleUnitsStateWhenDirectionNone.Add(true);
                }
            }
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Exit trafficLight
        if (haveTrafficLightToFollow && collision.gameObject.CompareTag("TrafficLight"))
        {
           // Logging.Log("VehicleScanner: remove traffic Light");

            if (trafficLightToFollow == collision.gameObject.GetComponent<TrafficLight>())
            {
                haveTrafficLightToFollow = false;
            }                
        }

        // if train --> return
        if (vehicleUnit.Type == VehicleUnit.TYPE.train) return;

        // Exit vehicle
        if ((haveVehicleToFollow && collision is PolygonCollider2D && collision.gameObject.CompareTag("Car") && vehicleUnit.Type != VehicleUnit.TYPE.train) ||
            (haveVehicleToFollow && collision is PolygonCollider2D && collision.gameObject.CompareTag("Train") && vehicleUnit.Type == VehicleUnit.TYPE.train))
        {
            //Logging.Log("VehicleScanner: remove vehicle");

            VehicleUnit vehicleUnit = collision.gameObject.GetComponent<VehicleUnit>();
            VehicleScanner vehicleScanner = collision.gameObject.GetComponent<VehicleScanner>();

            if (vehicleUnitsToFollow.Contains(vehicleUnit))
            {
                // PART: do not remove with the same direction
                if (direction == DIRECTION.none)
                {
                    vehicleUnitsToFollow.Remove(vehicleUnit);
                }
                else if (!needCarToFollowDirectionControl)
                {
                    vehicleUnitsToFollow.Remove(vehicleUnit);
                }
                //else if (direction != vehicleScanner.Direction)
                //{
                //    vehicleUnitsToFollow.Remove(vehicleUnit);
                //}
                else
                {
                    vehicleUnitsToChangeStateWhenDirectionNone.Add(vehicleUnit);
                    vehicleUnitsStateWhenDirectionNone.Add(false);
                }
                // END PART

                //vehicleUnitsToFollow.Remove(vehicleUnit);
            }

            haveVehicleToFollow = (vehicleUnitsToFollow.Count != 0);
        }       
    }

    public void RemoveVehicleFormFollowedDueToMaxDistance(VehicleUnit unit)
    {
        if (vehicleUnitsToFollow.Contains(unit))
        {
            vehicleUnitsToFollow.Remove(unit);
            
        }

        haveVehicleToFollow = vehicleUnitsToFollow.Count > 0;
    }

    public void ChangeDirection(DIRECTION newDirection, int newRoadStartPoint, bool newNeedCarToFollowDirectionControl = false)
    {
        // if train --> return
        //if (vehicleUnit.Type == VehicleUnit.TYPE.train) return;

        // set new values
        DIRECTION prevDirection = direction;
        direction = newDirection;

        // nothing changes
        if (direction == prevDirection && direction == DIRECTION.none)
        {
            return;
        }

        // new direction is not NONE -> check all vehicle to control
        if (direction != DIRECTION.none && prevDirection == DIRECTION.none)
        {
            // new values
            vehicleUnit.SetRoadStartPointNumber(newRoadStartPoint);
            needCarToFollowDirectionControl = newNeedCarToFollowDirectionControl;

            // remove with another direction
            List<VehicleUnit> unitsToRemove = new List<VehicleUnit>();
            foreach (VehicleUnit unit in vehicleUnitsToFollow)
            {
                VehicleScanner vehicleScanner = unit.gameObject.GetComponent<VehicleScanner>();
                if (vehicleScanner != null)
                {
                    if (vehicleScanner.Direction != direction)
                    {
                        unitsToRemove.Add(unit);
                    }
                }
            }

            foreach(VehicleUnit unit in unitsToRemove)
            {
                vehicleUnitsToFollow.Remove(unit);
            }

            if (needCarToFollowDirectionControl)
            {
                // add vehicles that on the same road
                vehicleUnitsToAddBeforeDirectionChange = vehicleUnit.GetUnitsWhenChangeDirection();

                vehicleUnitsToChangeStateWhenDirectionNone.Clear();
                vehicleUnitsStateWhenDirectionNone.Clear();

                if (vehicleUnitsToAddBeforeDirectionChange != null)
                {
                    Logging.Log("VehicleScanner: add range toFollow " + vehicleUnitsToAddBeforeDirectionChange.Count);
                    if (vehicleUnitsToAddBeforeDirectionChange.Count > 0)
                    {
                        foreach (VehicleUnit unit in vehicleUnitsToAddBeforeDirectionChange)
                        {
                            if (!vehicleUnitsToFollow.Contains(unit))
                            {
                                vehicleUnitsToFollow.Add(unit);
                            }
                        }                       
                    }
                }
            }            
        }
        // new direction is NONE but prev is not
        else if (prevDirection != DIRECTION.none && direction == DIRECTION.none)
        {
            if (needCarToFollowDirectionControl)
            {
                foreach (VehicleUnit unit in vehicleUnitsToAddBeforeDirectionChange)
                {
                    if (vehicleUnitsToFollow.Contains(unit))
                    {
                        vehicleUnitsToFollow.Remove(unit);
                    }
                }

                for (int i = 0; i < vehicleUnitsToChangeStateWhenDirectionNone.Count; i++)
                {
                    if (vehicleUnitsStateWhenDirectionNone[i])
                    {
                        vehicleUnitsToFollow.Add(vehicleUnitsToChangeStateWhenDirectionNone[i]);
                    }
                    else
                    {
                        vehicleUnitsToFollow.Remove(vehicleUnitsToChangeStateWhenDirectionNone[i]);
                    }
                }
            }

            needCarToFollowDirectionControl = false;
            vehicleUnit.ResetRoadStartPointNumber();
        }
    }

    public bool NeedTrafficLightControl()
    {
        return (haveTrafficLightToFollow && !isIntoCrossroads);
    }

    public bool NeedVehicleControl()
    {
        haveVehicleToFollow = vehicleUnitsToFollow.Count > 0;
        return haveVehicleToFollow;
    }

    public Vector2 GetDataAboutTrafficLight()
    {
        if (haveTrafficLightToFollow && !isIntoCrossroads)
        {
            return trafficLightToFollow.transform.position;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public List<VehicleUnit> GetDataAboutVehicles()
    {
        return vehicleUnitsToFollow;
    }

    public void CrossBorder(bool needIsIntoCrossroadsChange = true)
    {
        if (needIsIntoCrossroadsChange) isIntoCrossroads = !isIntoCrossroads;
        vehicleUnit.StopTimer();

        if (!isIntoCrossroads)
        {
            vehicleUnit.EnterOrExitTurn(false);
        }
        //if (isInvisibleForTrafficLights) haveTrafficLightToFollow = false;
    }

    public void EnableCollider()
    {
        boxColliderComponent.enabled = true;
    }

    public void DisableCollider()
    {
        direction = DIRECTION.none;
        needCarToFollowDirectionControl = false;
        boxColliderComponent.enabled = false;
    }
}
