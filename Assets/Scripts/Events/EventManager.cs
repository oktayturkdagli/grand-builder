using System;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    // Mouse Position
    public event Action onRequestMousePositionIn3D;

    // UI Actions
    public event Action onRequestABuilding;

    //Events cannot be triggered directly from another class so they are triggered via functions
    // Mouse Position
    public Vector3 OnRequestMousePositionIn3D()
    {
        onRequestMousePositionIn3D?.Invoke();
        return EventHandler.Instance.mousePositionIn3D;
    }

    // Buildings
    public void OnRequestABuilding()
    {
        onRequestABuilding?.Invoke();
    }
}