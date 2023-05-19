using UnityEngine;

public class CameraController : MonoBehaviour 
{
	private InputSystem inputSystem;
	public Camera cam;
	
	public float acceleration = 200;
	public float accSprintMultiplier = 2;
	public float dampingCoefficient = 10; // After the input stops, how quickly does the motion come to a halt
	public float zoomSpeed = 20;
	public float rotationSpeed = 0.8f;
	public Vector3 RangeOfMotionMax = new Vector3(10000, 30, 10000); // Borders
	public Vector3 RangeOfMotionMin = new Vector3(-10000, 5, -10000); // Borders
	
	private Vector3 movementVector = Vector3.zero;
	private Vector3 rotationVector = Vector3.zero;
	private bool isSprinting = false;
	private Vector3 velocity; // Current velocity
	
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
		CheckInput();
		Move();
		Rotate();
	}
	
	private void CheckInput()
	{
		Vector2 inputWASD = inputSystem.GeneralControls.WASD.ReadValue<Vector2>(); // WASD
		float inputE = inputSystem.GeneralControls.E.ReadValue<float>(); // E
		float inputQ = inputSystem.GeneralControls.Q.ReadValue<float>(); // Q
		float inputWheel = inputSystem.GeneralControls.MouseWheel.ReadValue<float>();
		isSprinting = inputSystem.GeneralControls.ShiftLeft.ReadValue<float>() > 0.1f; // Left Shift
		
		// Right - Left movement
		switch (inputWASD.x)
		{
			case > 0:
				movementVector += Vector3.right;
				break;
			case < 0:
				movementVector += Vector3.left;
				break;
			default:
				movementVector.x = 0;
				break;
		}
		
		// Up - Down movement
		switch (inputWASD.y)
		{
			case > 0:
				movementVector += Vector3.forward;
				break;
			case < 0:
				movementVector += Vector3.back;
				break;
			default:
				movementVector.z = 0;
				break;
		}
		
		//  Zoom
		switch (inputWheel)
		{
			case > 0:
				movementVector += Vector3.down;
				break;
			case < 0:
				movementVector += Vector3.up;
				break;
			default:
				movementVector.y = 0;
				break;
		}

		// Rotation
		if (inputE > 0.1)
		{
			rotationVector.y += 1;
		}
		else if (inputQ > 0.1)
		{
			rotationVector.y -= 1;
		}
		else
		{
			rotationVector.y = 0;
		}
	}
	
	private void Move()
	{
		if (movementVector == Vector3.zero && velocity == Vector3.zero)
			return;
		
		// Detect direction and speed
		Vector3 direction = transform.TransformVector(movementVector.normalized);
		Vector3 result = direction * acceleration;
		
		// Adjust mouse wheel sensitivity
		result.y *= zoomSpeed; 
		
		// Sprint
		if(isSprinting)
			result = direction * (acceleration * accSprintMultiplier); 
		
		// Velocity
		velocity += result * Time.deltaTime;
		
		// Physics
		velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
		CheckRangeOfMotion(transform, velocity * Time.deltaTime); // Check range of motion
		transform.position += velocity * Time.deltaTime;
	}
	
	private void Rotate()
	{
		if (rotationVector == Vector3.zero)
			return;

		// Rotate
		transform.eulerAngles += new Vector3(0, rotationVector.y * rotationSpeed * Time.deltaTime, 0);
	}
	
	private void CheckRangeOfMotion(Transform obj, Vector3 instantVelocity)
	{
		// Reset velocity if the object is outside of X limits
		if ( ((obj.transform.position.x + instantVelocity.x >= RangeOfMotionMax.x) && movementVector.x > 0) || ((obj.transform.position.x + instantVelocity.x <= RangeOfMotionMin.x) && movementVector.x < 0) )
		{
			velocity.x = 0;
		}
		
		// Reset velocity if the object is outside of Z limits
		if ( ((obj.transform.position.z + instantVelocity.z >= RangeOfMotionMax.z) && movementVector.z > 0) || ((obj.transform.position.z + instantVelocity.z <= RangeOfMotionMin.z) && movementVector.z < 0) )
		{
			velocity.z = 0;
		}
		
		// Reset velocity if the object is outside of Y limits
		if ( ((obj.transform.position.y + instantVelocity.y >= RangeOfMotionMax.y) && movementVector.y > 0) || ((obj.transform.position.y + instantVelocity.y <= RangeOfMotionMin.y) && movementVector.y < 0) )
		{
			velocity.y = 0;
		}
		
		// When zoomed a big object, the camera should not go inside the object
		RaycastHit hitInfo = Utility.Instance.FireRaycastFromCamera(cam, RangeOfMotionMin.y); // A raycast is cast forward from the camera
		if ((hitInfo.collider && movementVector.y < 0))
			movementVector.y = 0;
	}
}