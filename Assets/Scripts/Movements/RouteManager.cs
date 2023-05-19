using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RouteManager : Singleton<RouteManager>
{
    private Camera cam;
    LayerMask ignoredLayerMasks = ~(1 << 1 | 1 << 2 | 1 << 4 | 1 << 5);
    private void Start()
    {
        cam = Camera.main;
    }
    
    public void OnRightMousePerform()
    {
        if (SelectionManager.Instance.selectedObjects.Count == 0)
            return;
        
        Vector3 mousePosition = EventManager.Instance.OnRequestMousePositionIn3D();
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue()); // Creates a Ray from the mouse position
        
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, 1 << 6))
        {
            if (hit.collider.gameObject.TryGetComponent(out Element elementObject))
            {
                CheckForMovement(elementObject.transform.position, elementObject);
            }
            else
            {
                CheckForMovement(elementObject.transform.position);
            }
        }
        else
        {
            CheckForMovement(mousePosition);
        }
    }

    private void CheckForMovement(Vector3 destinationPoint, Element destinationObject = null)
    {
        List<Character> movableObjects = new List<Character>();
        foreach (var selectedObject in SelectionManager.Instance.selectedObjects)
        {
            if (!selectedObject.GameObjectReference.TryGetComponent(out Character movementScript))
                continue;
            
            movableObjects.Add(movementScript);
        }

        var formationType = FormationTypes.Standard;
        var formationRadius = 1f;
        if (!destinationObject)
        {
            formationType = FormationTypes.Standard;
            formationRadius = 1f;
        }
        else if (destinationObject.GetType() == typeof(Building))
        {
            formationType = FormationTypes.OneRing;
            formationRadius = destinationObject.GameObjectReference.GetComponent<Building>().colliderReference.radius + 0.2f;
        }
        else
        {
            formationType = FormationTypes.Standard;
            formationRadius = 1f;
        }
        
        List<Vector3> formationList = FormationManager.Instance.CovertToFormation(movableObjects.Count, destinationPoint, formationType, formationRadius); // Formation
        
        for (int i = 0; i < movableObjects.Count; i++)
        {
            movableObjects[i].Move(formationList[i], destinationObject);
        }
        
        if (movableObjects.Count > 0 && !destinationObject)
            ParticleSystemManager.Instance.PlayPointerDestinationPS(destinationPoint);
    }
}

