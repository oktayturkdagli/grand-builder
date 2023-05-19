using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    private InputSystem inputSystem;
    public bool IsPointerOverUI = false;
    
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
    
    private void Update()
    {
        IsPointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }
}