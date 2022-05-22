using UnityEngine;

public class ZoomDetection : MonoBehaviour
{
    [Header("Borders")]
    [SerializeField] private float cameraSpeed = 20f;
    [SerializeField] private float cameraSizeMin = 3f;
    [SerializeField] private float cameraSizeMax = 11f;

    [Header("Scripts")]
    [SerializeField] private InputControl inputControl;
    [SerializeField] private MoveDetection moveDetection;

    bool pauseUpdate = true;
    bool noPrevValues = true;

    Vector2 primaryTouchPrevPosition = Vector2.zero;
    Vector2 secondaryTouchPrevPosition = Vector2.zero;

    Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        pauseUpdate = true;
        noPrevValues = true;
    }

    private void OnEnable()
    {
        inputControl.OnStartPitch += ZoomStart;
        inputControl.OnEndPitch += ZoomStop;
        inputControl.OnScrollWheelPerformed += ZoomByMouseWheel;
    }

    private void OnDisable()
    {
        inputControl.OnStartPitch -= ZoomStart;
        inputControl.OnEndPitch -= ZoomStop;
        inputControl.OnScrollWheelPerformed -= ZoomByMouseWheel;
    }

    private void Update()
    {
        if (pauseUpdate) return;

        if (noPrevValues)
        {
            primaryTouchPrevPosition = inputControl.GetPrimaryTouchPosition();
            secondaryTouchPrevPosition = inputControl.GetSecondaryTouchPosition();
            noPrevValues = false;
            return;
        }

        float prevMagnitude = (primaryTouchPrevPosition - secondaryTouchPrevPosition).magnitude;
        float currentMagnitude = (inputControl.GetPrimaryTouchPosition() - inputControl.GetSecondaryTouchPosition()).magnitude;

        float difference = currentMagnitude - prevMagnitude;

        Zoom(difference * 0.01f);
    }

    void ZoomStart()
    {
        noPrevValues = true;
        pauseUpdate = false;
    }

    void ZoomStop()
    {
        pauseUpdate = true;
    }

    void ZoomByMouseWheel(float distance)
    {
        if (distance < 0) Zoom(-1);
        else Zoom(1);
    }

    void Zoom(float direction)
    {
        float newSize = mainCamera.orthographicSize - direction;
        newSize = Mathf.Clamp(newSize, cameraSizeMin, cameraSizeMax);
        
        if (moveDetection.CanMakeZoom(newSize))
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newSize, Time.deltaTime * cameraSpeed);
            moveDetection.UpdateCameraWidthAndHeight();
        }
        else if (moveDetection.MoveCameraToMakeZoom(newSize))
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newSize, Time.deltaTime * cameraSpeed);
            moveDetection.UpdateCameraWidthAndHeight();
        }
            

        // correct camera position according to borders
        //moveDetection.CorrectPositionWhenZooming();
    }
}
