using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Utility : Singleton<Utility>
{
	private static long id = 0;
	
	public long GetId()
	{
		return id++;
	}
	
	public RaycastHit FireRaycastFromCamera(Camera cam, float distance, int layerMask = 0)
	{
		// Debug.DrawRay(cam.transform.position, cam.transform.forward * distance, Color.magenta, 3f);
		Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, ~layerMask, QueryTriggerInteraction.Ignore))
		{
			return hitInfo;
		}
		return default;
	}

	private RaycastHit FireRaycastFromMouse(Camera cam, float distance, int layerMask = 0)
	{
		// Debug.DrawRay(cam.transform.position, cam.transform.forward * distance, Color.magenta, 3f);
		Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue()); // Creates a Ray from the mouse position
		if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, ~layerMask, QueryTriggerInteraction.Collide))
		{
			return hitInfo;
		}
		return default;
	}
	
	public RaycastHit[] FireRaycastFromMouseFindAll(Camera cam)
	{
		// Debug.DrawRay(cam.transform.position, cam.transform.forward * 100, Color.magenta, 3f);
		Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue()); // Creates a Ray from the mouse position
		RaycastHit[] hits = Physics.RaycastAll(ray);
		Array.Sort(hits, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance)); // Sorts the Raycast results by distance
		foreach(RaycastHit obj in hits)
		{
			Debug.Log(obj.transform.gameObject);
		}
		return hits;
	}
	
	public Color GetUnityColorOfRGBA(Color color)
	{
		var r = color.r == 0 ? 0 : color.r / 255;
		var g = color.g == 0 ? 0 : color.g / 255;
		var b = color.b == 0 ? 0 : color.b / 255;
		var a = color.a == 0 ? 0 : color.a / 255;
		return new Color(r, g, b, a);
	}
	
	public Color GetRGBAOfColor32(Color32 color)
	{
		return new Color32(128, 255, 128, 255);
	}
	
	public string GetHexCodeOfRGB(Color color)
	{
		return ColorUtility.ToHtmlStringRGBA(color);
	}
	
	public void SentMessageToDeveloper(string message)
	{
		Debug.Log(message);
	}
	
	public void DestroyAnObject(GameObject obj)
	{
		if (!obj)
			return;
		
		Destroy(obj);
	}
}