using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDetection : MonoBehaviour
{
    public enum SIDE { left, up, right, down }
    //public enum DIRECTION { horizontal, vertical }

    [Header("Settings")]
    [SerializeField] private float timeThreshold = 1f;
    [SerializeField] private float distanceMultiplier = 0.01f;

    [Header("Directions Allowed")]
    [SerializeField] private bool horizontal = true;
    [SerializeField] private bool vertical = true;

    [Header("Borders")]
    [SerializeField] private float leftBorder = -18.5f;
    [SerializeField] private float upBorder = 18.5f;
    [SerializeField] private float rightBorder = 18.5f;
    [SerializeField] private float downBorder = - 18.5f;

    [Header("Scripts")]
    [SerializeField] private InputControl inputControl;

    private Transform cameraTransform;
    private Camera mainCamera;
    private Coroutine moveDetectionCoroutine;
    private WaitForSeconds detectionTimer;

    private Vector2 touchPosition = Vector2.zero;
 
    bool pauseUpdate = true;
    bool noMoving = false;
    bool noPrevPosition = true;

    float cameraCurrentSize;
    float cameraHeight = 0f;
    float cameraWidth = 0f;

    IDictionary<SIDE, float> borders = new Dictionary<SIDE, float>();

    private void Awake()
    {
        noMoving = (!horizontal && !vertical);

        cameraTransform = Camera.main.transform;
        mainCamera = Camera.main;
        cameraCurrentSize = 0;
        

        detectionTimer = new WaitForSeconds(timeThreshold);
        pauseUpdate = true;
        noPrevPosition = true;

        // create diactionary for borders
        borders[SIDE.left] = leftBorder;
        borders[SIDE.up] = upBorder;
        borders[SIDE.right] = rightBorder;
        borders[SIDE.down] = downBorder;
    }

    private void OnEnable()
    {
        StartTouchMovingControl();
    }

    private void OnDisable()
    {
        StopTouchMovingControl();
    }

    void StartTouchMovingControl()
    {
        if (noMoving) return;

        inputControl.OnStartTouch += MoveStart;
        inputControl.OnEndTouch += MoveEnd;
    }

    void StopTouchMovingControl()
    {
        if (noMoving) return;

        inputControl.OnStartTouch -= MoveStart;
        inputControl.OnEndTouch -= MoveEnd;
    }

    private void Update()
    {
        if (pauseUpdate) return;

        if (noPrevPosition)
        {
            touchPosition = inputControl.GetPrimaryTouchPosition();
            UpdateCameraWidthAndHeight();
            noPrevPosition = false;
        }

        Vector2 newTouchPosition = inputControl.GetPrimaryTouchPosition();
        Vector2 positionDelta = (newTouchPosition - touchPosition);

        Vector2 direction = positionDelta.normalized;
        Move(positionDelta, direction);
        touchPosition = newTouchPosition;
    }

    private void MoveStart(Vector2 position, float time)
    {
        if (Time.timeScale == 0) return;

        noPrevPosition = true;
        moveDetectionCoroutine = StartCoroutine(DetectMove());

    }

    IEnumerator DetectMove()
    {
        yield return detectionTimer;
        pauseUpdate = false;
    }

    private void MoveEnd(Vector2 position, float time)
    {
        if (Time.timeScale == 0) return;

        pauseUpdate = true;
        StopCoroutine(moveDetectionCoroutine);
    }

    void Move(Vector2 positionDelta, Vector2 direction)
    {

        if (cameraTransform.position.y + cameraHeight / 2 < borders[SIDE.up] ||
            cameraTransform.position.y - cameraHeight / 2 > borders[SIDE.down] ||
            cameraTransform.position.x - cameraWidth / 2 > borders[SIDE.left] ||
            cameraTransform.position.x + cameraWidth / 2 < borders[SIDE.right])
        {
            Vector2 delta = CalculateDeltaToTranslate(new Vector2(-positionDelta.x * distanceMultiplier, -positionDelta.y * distanceMultiplier));
            cameraTransform.Translate(new Vector3(delta.x, delta.y, 0f));
        }
    }

    Vector2 CalculateDeltaToTranslate(Vector2 initialDelta)
    {
        Vector2 delta = initialDelta;
        Vector2 newCameraPosition = new Vector2 (cameraTransform.position.x + delta.x, cameraTransform.position.y + delta.y);

        if (horizontal)
        {
            // check left border
            if (newCameraPosition.x - cameraWidth / 2 < borders[SIDE.left])
            {
                // calculate max available move
                Logging.Log("MoveDetection: touch left border");
                delta.x = cameraTransform.position.x - cameraWidth / 2 - borders[SIDE.left];
            }

            //check right border
            if (newCameraPosition.x + cameraWidth / 2 > borders[SIDE.right])
            {
                Logging.Log("MoveDetection: touch right border");
                delta.x = borders[SIDE.right] - (cameraTransform.position.x + cameraWidth / 2);
            }
        }
        else
        {
            delta.x = 0f;
        }
        
        if (vertical)
        {
            // check up border
            if (newCameraPosition.y + cameraHeight / 2 > borders[SIDE.up])
            {
                // calculate max available move
                delta.y = borders[SIDE.up] - (cameraTransform.position.y + cameraHeight / 2);
            }

            if (newCameraPosition.y - cameraHeight / 2 < borders[SIDE.down])
            {
                // calculate max available move
                delta.y = cameraTransform.position.y - cameraHeight / 2 - borders[SIDE.down];
            }
        }
        else
        {
            delta.y = 0f;
        }

        return delta;
    }

    public void UpdateCameraWidthAndHeight()
    {
        if (cameraCurrentSize == mainCamera.orthographicSize) return;

        cameraCurrentSize = mainCamera.orthographicSize;
        cameraHeight = 2f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
    }



    public bool CanMakeZoom(float newCameraSize)
    {
        float newCameraHeight = 2f * newCameraSize;
        float newCameraWidth = newCameraHeight * mainCamera.aspect;

        if (cameraTransform.position.y + newCameraHeight / 2 > borders[SIDE.up]) return false;
        else if (cameraTransform.position.y - newCameraHeight / 2 < borders[SIDE.down]) return false;
        else if (cameraTransform.position.x - newCameraWidth / 2 < borders[SIDE.left]) return false;
        else if (cameraTransform.position.x + newCameraWidth / 2 > borders[SIDE.right]) return false;
        else return true;
    }

    public bool MoveCameraToMakeZoom(float newCameraSize)
    {
        float newCameraHeight = 2f * newCameraSize;
        float newCameraWidth = newCameraHeight * mainCamera.aspect;

        Vector2 delta = Vector2.zero;

        // horizontal

        if (cameraTransform.position.x + newCameraWidth / 2 > borders[SIDE.right])
        {
            //if (!horizontal) return false;

            // can't make zoom -> right border
            delta.x = -(cameraTransform.position.x + newCameraWidth / 2) + borders[SIDE.right];

            if (cameraTransform.position.x - delta.x - newCameraWidth / 2 < borders[SIDE.left])
            {
                return false;
            }
        }

        if (cameraTransform.position.x - newCameraWidth / 2 < borders[SIDE.left])
        {
            //if (!horizontal) return false;

            // can't make zoom -> left border
            delta.x = borders[SIDE.left] - (cameraTransform.position.x - newCameraWidth / 2);

            if (cameraTransform.position.x - delta.x + newCameraWidth / 2 > borders[SIDE.right])
            {
                return false;
            }
        }


        // vertical
        if (cameraTransform.position.y + newCameraHeight / 2 > borders[SIDE.up])
        {
            //if (!vertical) return false;

            // can't make zoom -> upper border
            delta.y = -(cameraTransform.position.y + newCameraHeight / 2) + borders[SIDE.up];
            
            if (cameraTransform.position.y - delta.y - newCameraHeight / 2 < borders[SIDE.down])
            {
                return false;
            }
        }

        if (cameraTransform.position.y - newCameraHeight / 2 < borders[SIDE.down])
        {
            //if (!vertical) return false;

            // can't make zoom -> down border
            delta.y = borders[SIDE.down] - (cameraTransform.position.y - newCameraHeight / 2);

            if (cameraTransform.position.y - delta.y + newCameraHeight / 2 > borders[SIDE.up])
            {
                return false;
            }
        }
        cameraTransform.Translate(new Vector3(delta.x, delta.y), 0f);
        return true;
    }
}
