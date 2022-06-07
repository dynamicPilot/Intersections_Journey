using UnityEngine;

public enum DIRECTION { nord, west, south, east, none, south_east, nord_east, nord_west, south_west }

public interface IDirectionShearer
{
    public abstract DIRECTION GetDirection();
    public abstract int GetRoadStartPoint();
    public float GetTotalTimeOnRoad();
    public void SetRoadStartPointNumber(int number = -1);

    public delegate void DirectionControlChange(DIRECTION _direction, bool _directionControl);
    public event DirectionControlChange OnDirectionControlChangeFromNone;
    public event DirectionControlChange OnDirectionControlChangeToNone;
}
public interface ICrossBorder
{
    public void CrossBorder();
}

[RequireComponent(typeof(VRoadMemberTag))]
public class VScanner : MonoBehaviour, ICrossBorder, IDirectionShearer
{
    [Header("Settings")]
    [SerializeField] private float gap = 0.7f;
    [SerializeField] private float maxDistanceToDetect = 5f;
    [SerializeField] private bool isSelfTrain = false;

    private BoxCollider2D boxCollider;
    private PolygonCollider2D polygonCollider;
    private VScannerUnitsInfo _unitsInfo;
    public VScannerUnitsInfo UnitsInfo { get => _unitsInfo; }

    [SerializeField] private VScannerTrafficLightInfo _trafficLightInfo;
    public VScannerTrafficLightInfo TrafficLightInfo { get => _trafficLightInfo; }
    
    private IPositionShearer _positionShearer;
    public IPositionShearer PositionShearer { get => _positionShearer; }

    private ObjectsDetector detector;
    private DIRECTION direction = DIRECTION.none;

    private bool isIntoCrossroads;
    private bool haveTrafficLightToFollow = false;
    private bool needUpdateTotalTime = true;
    
    private int roadStartPointNumber = -1;
    [SerializeField] private float totalTimeOnRoad = 0f;

    public event IDirectionShearer.DirectionControlChange OnDirectionControlChangeFromNone;
    public event IDirectionShearer.DirectionControlChange OnDirectionControlChangeToNone;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        _positionShearer = new VPositionShearer(gap, polygonCollider.bounds.extents, transform, boxCollider);
    }

    private void OnDestroy()
    {
        _unitsInfo.Destroy();
    }

    public void StartScanner(IStartAndEndPathPoints _routerInfo)
    {
        _unitsInfo = new VScannerUnitsInfo(_positionShearer, maxDistanceToDetect, this, GetComponent<VRoadMemberTag>());
        _trafficLightInfo = new VScannerTrafficLightInfo(_positionShearer);

        detector = new ObjectsDetector(_routerInfo, _unitsInfo, _trafficLightInfo);
        haveTrafficLightToFollow = false;
        boxCollider.enabled = true;
        polygonCollider.enabled = true;
        needUpdateTotalTime = true;

        totalTimeOnRoad = 0;
    }

    public void StopScanner()
    {
        boxCollider.enabled = false;
        polygonCollider.enabled = false;
        isIntoCrossroads = false;
        needUpdateTotalTime = false;
        direction = DIRECTION.none;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrafficLight") && !isIntoCrossroads && !haveTrafficLightToFollow)
        {
            Logging.Log("VScanner: detectTrafficLight");
            haveTrafficLightToFollow = detector.DetectTrafficLight(collision);
        }

        if (collision is PolygonCollider2D && collision.gameObject.CompareTag("Car") && !isSelfTrain)
        {
            detector.DetectCar(collision, direction);
        }

        if (collision is PolygonCollider2D && collision.gameObject.CompareTag("Train"))
        {
            detector.DetectTrain(collision, direction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrafficLight") && haveTrafficLightToFollow)
        {
            haveTrafficLightToFollow = detector.UndetectTrafficLight(collision);
        }

        if (collision is PolygonCollider2D && collision.gameObject.CompareTag("Car") && !isSelfTrain)
        {
            detector.UndetectCar(collision, direction);
        }

        if (collision is PolygonCollider2D && collision.gameObject.CompareTag("Train"))
        {
            detector.UndetectTrain(collision, direction);
        }
    }
    public void ChangeDirection(DIRECTION _direction, int _roadStartPoint, bool _directionControl = false)
    {
        DIRECTION prevDirection = direction;
        direction = _direction;

        // nothing changes
        if (direction == prevDirection && direction == DIRECTION.none) return;

        // new direction is not NONE -> check all vehicle and remove with another direction
        if (direction != DIRECTION.none && prevDirection == DIRECTION.none)
        {
            // new values
            roadStartPointNumber = _roadStartPoint;
            if (OnDirectionControlChangeFromNone != null) OnDirectionControlChangeFromNone.Invoke(_direction, _directionControl);
        }
        // new direction is NONE but prev is not
        else if (prevDirection != DIRECTION.none && direction == DIRECTION.none)
        {
            roadStartPointNumber = -1;
            if (OnDirectionControlChangeToNone != null) OnDirectionControlChangeToNone.Invoke(_direction, false);
        }
    }

    public DIRECTION GetDirection()
    {
        return direction;
    }

    public void CrossBorder()
    {
        isIntoCrossroads = !isIntoCrossroads;
        needUpdateTotalTime = false;
    }

    public bool UpdateTotalTime(float deltaT)
    {
        totalTimeOnRoad += deltaT;
        return needUpdateTotalTime;
    }

    public int GetRoadStartPoint()
    {
        return roadStartPointNumber;
    }

    public float GetTotalTimeOnRoad()
    {
        return totalTimeOnRoad;
    }

    public void SetRoadStartPointNumber(int number = -1)
    {
        roadStartPointNumber = number;
    }
}
