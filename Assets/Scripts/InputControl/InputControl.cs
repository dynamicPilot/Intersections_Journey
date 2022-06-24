using UnityEngine;
using UnityEngine.InputSystem;

public class InputControl : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private MoveDetection moveDetection;
    [SerializeField] private ZoomDetection zoomDetection;

    private Controllers controllers;
    private Camera mainCamera;

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    public delegate void StartPitch();
    public event StartPitch OnStartPitch;
    public delegate void EndPitch();
    public event EndPitch OnEndPitch;

    public delegate void ScrollWheelPerformed(float distance);
    public event ScrollWheelPerformed OnScrollWheelPerformed;

    private void Awake()
    {
        controllers = new Controllers();
        mainCamera = Camera.main;
    }

    public void StartInputControl()
    {
        controllers.Enable();
        zoomDetection.enabled = true;
        moveDetection.enabled = true;
    }

    public void EndInputControl()
    {
        zoomDetection.enabled = false;
        moveDetection.enabled = false;
        controllers.Disable();
    }

    private void OnDisable()
    {
        EndInputControl();
    }

    private void Start()
    {
        controllers.ControllersMap.PrimaryTouchContact.started += ctx => StartTouchPrimary(ctx);
        controllers.ControllersMap.PrimaryTouchContact.canceled += ctx => EndTouchPrimary(ctx);

        controllers.ControllersMap.SecondaryTouchContact.started += ctx => PitchStart();
        controllers.ControllersMap.SecondaryTouchContact.canceled += ctx => PitchStop();

        controllers.ControllersMap.ScrollWheelYDirection.performed += ctx => MouseScroll(); 
    }

    public Vector2 GetPrimaryTouchPosition()
    {
        return controllers.ControllersMap.PrimaryTouchPosition.ReadValue<Vector2>();
    }

    public Vector2 GetSecondaryTouchPosition()
    {
        return controllers.ControllersMap.SecondaryTouchPosition.ReadValue<Vector2>();
    }

    private void PitchStart()
    {
        if (OnStartPitch != null)
        {
            OnStartPitch.Invoke();
        }

    }

    private void PitchStop()
    {
        if (OnEndPitch != null)
        {
            OnEndPitch.Invoke();
        }
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch.Invoke(CustomFunctions.ScreenToWorld(mainCamera, controllers.ControllersMap.PrimaryTouchPosition.ReadValue<Vector2>()),
                (float)context.startTime);
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch.Invoke(CustomFunctions.ScreenToWorld(mainCamera, controllers.ControllersMap.PrimaryTouchPosition.ReadValue<Vector2>()),
                (float)context.startTime);
        }
    }

    private void MouseScroll()
    {
        if (OnScrollWheelPerformed != null)
        {
            OnScrollWheelPerformed.Invoke(controllers.ControllersMap.ScrollWheelYDirection.ReadValue<Vector2>().y);
        }
    }
}
