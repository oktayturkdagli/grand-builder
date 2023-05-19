using UnityEngine;
using UnityEngine.InputSystem;

public class EventHandler : Singleton<EventHandler>
{
    private InputSystem inputSystem;
    public Vector3 mousePositionIn3D;

    private void OnEnable()
    {
        inputSystem.GeneralControls.Enable();
    }
    
    private void OnDisable()
    {
        inputSystem.GeneralControls.Disable();
    }
    
    private void Awake()
    {
        inputSystem = new InputSystem();
    }
    
    private void Start()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventManager.Instance.onRequestMousePositionIn3D += GetMousePositionIn3D;
        inputSystem.GeneralControls.MouseLeftButton.performed += OnLeftMousePerform;
        inputSystem.GeneralControls.MouseRightButton.performed += OnRightMousePerform;
        inputSystem.GeneralControls.MouseWheel.performed += OnMouseWheelPerform;
        inputSystem.GeneralControls.ControlLeft.performed += OnCtrlKeyPerform;
        inputSystem.GeneralControls.ShiftLeft.performed += OnShiftKeyPerform;
        inputSystem.GeneralControls.Space.performed += OnSpaceKeyPerform;
        inputSystem.GeneralControls.WASD.performed += OnWASDKeyPerform;
        inputSystem.GeneralControls.E.performed += OnEKeyPerform;
        inputSystem.GeneralControls.Q.performed += OnQKeyPerform;
    }

    // Mouse
    private void GetMousePositionIn3D()
    {
        // Debug.DrawRay(cam.transform.position, cam.transform.forward * distance, Color.magenta, 3f);
        Camera cam = Camera.main;
        int layerMask = 0;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue()); // Creates a Ray from the mouse position
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, ~layerMask, QueryTriggerInteraction.Collide))
        {
            mousePositionIn3D = hitInfo.point;
        }
        else
        {
            mousePositionIn3D = new Vector3(-9999,-9999,-9999);
        }
    }

    private void OnLeftMousePerform(InputAction.CallbackContext context)
    {
        if (BuildingManager.Instance.isDraggingABuilding)
        {
            BuildingManager.Instance.DropTheBuilding();
        }

        else if (SelectionManager.Instance)
        {
            SelectionManager.Instance.OnLeftMousePerform();
        }
    }
    
    private void OnRightMousePerform(InputAction.CallbackContext context)
    {
        if (BuildingManager.Instance.isDraggingABuilding)
        {
            BuildingManager.Instance.CancelDragAndDrop();
        }
        else
        {
            RouteManager.Instance.OnRightMousePerform();
        }
    }
    
    private void OnMouseWheelPerform(InputAction.CallbackContext context)
    {
        switch (context.ReadValue<float>())
        {
            case > 0:
                // Debug.Log("Down");
                break;
            case < 0:
                // Debug.Log("Up");
                break;
        }
    }

    // Special Keys
    private void OnSpaceKeyPerform(InputAction.CallbackContext context)
    {
        // Debug.Log("OnSpaceKeyDown");
    }

    private void OnShiftKeyPerform(InputAction.CallbackContext context)
    {
        // Debug.Log("OnShiftKeyDown");
    }
    
    private void OnCtrlKeyPerform(InputAction.CallbackContext context)
    {
        // Debug.Log("OnCtrlKeyDown");
    }
    
    // Normal Keys
    private void OnWASDKeyPerform(InputAction.CallbackContext context)
    {
        Vector2 inputWASD = context.ReadValue<Vector2>(); // WASD
        
        // Right - Left movement
        switch (inputWASD.x)
        {
            case > 0:
                // Debug.Log("Right");
                break;
            case < 0:
                // Debug.Log("Left");
                break;
        }
		
        // Up - Down movement
        switch (inputWASD.y)
        {
            case > 0:
                // Debug.Log("Forward");
                break;
            case < 0:
                // Debug.Log("Back");
                break;
        }
    }
    
    private void OnEKeyPerform(InputAction.CallbackContext context)
    {
        // Debug.Log("OnEKeyDown");
    }
    
    private void OnQKeyPerform(InputAction.CallbackContext context)
    {
        // Debug.Log("OnQKeyDown");
    }
}