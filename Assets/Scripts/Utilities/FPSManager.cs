using UnityEngine;

// Assign this script to any object in the Scene to display frames per second
public class FPSManager : Singleton<FPSManager>
{
	[SerializeField] [Range(30, 1000)]public int frameRate = 144;
	private const float updateInterval = 0.5f; // How often should the number update
	private float accumulator = 0.0f;
	private int frames = 0;
	private float timeLeft;
	private float fps;
	
	private readonly GUIStyle textStyle = new GUIStyle();
	
	private void Start()
	{
		timeLeft = updateInterval;
		textStyle.fontStyle = FontStyle.Bold;
		textStyle.normal.textColor = Color.white;
	}
	
	private void Update()
	{
		CalculateFPS();
	}
	
	private void OnGUI()
	{
		DisplayFPS();
	}

	private void OnValidate()
	{
		SetFrameRate();
	}

	private void CalculateFPS()
	{
		timeLeft -= Time.deltaTime;
		accumulator += Time.timeScale / Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if (timeLeft <= 0.0)
		{
			// Display two fractional digits (f2 format)
			fps = (accumulator / frames);
			timeLeft = updateInterval;
			accumulator = 0.0f;
			frames = 0;
		}
	}
	
	private void DisplayFPS()
	{
		//Display the fps and round to 2 decimals
		GUI.Label(new Rect(5, 5, 100, 25), fps.ToString("F2") + "FPS", textStyle);
	}
	
	private void SetFrameRate()
	{
		Application.targetFrameRate = frameRate;
	}
}
