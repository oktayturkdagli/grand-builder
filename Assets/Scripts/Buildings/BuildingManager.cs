using System;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    [SerializeField] public bool isDraggingABuilding;
    [SerializeField] public DraggingObject draggingObject;

    private void Update()
    {
        if (isDraggingABuilding)
            DragABuilding();
    }
    
    public void CreateABuilding(BuildingSO buildingSO)
    {
        var mousePositionIn3D = EventHandler.Instance.mousePositionIn3D;
        var newGameObject = Instantiate(buildingSO.prefab, mousePositionIn3D, Quaternion.identity);
        var building = newGameObject.GetComponent<Building>();
        draggingObject = new DraggingObject(building, newGameObject.GetChild(0).GetComponent<Renderer>().material.color);
        isDraggingABuilding = true;
        draggingObject.buildingReference.GetComponent<Selectable>().CanUseOutliner = true;
        draggingObject.buildingReference.GetComponent<Selectable>().CanSelect = true;
        draggingObject.buildingReference.colliderReference.enabled = false;
        draggingObject.rendererReference.material.color = draggingObject.draggingColor;
    }

    private void DragABuilding()
    {
        EventManager.Instance.OnRequestMousePositionIn3D();
        var mousePositionIn3D = EventHandler.Instance.mousePositionIn3D;
        
        if (CanSpawnBuilding(draggingObject.buildingReference, mousePositionIn3D))
        {
            draggingObject.buildingReference.transformReference.position = mousePositionIn3D;
            draggingObject.rendererReference.material.color = draggingObject.draggingColor;
        }
        else
        {
            draggingObject.buildingReference.transformReference.position = mousePositionIn3D;
            draggingObject.rendererReference.material.color = draggingObject.notBuildableColor;
        }
    }
    
    public void DropTheBuilding()
    {
        EventManager.Instance.OnRequestMousePositionIn3D();
        var mousePositionIn3D = EventHandler.Instance.mousePositionIn3D;
        
        if (!CanSpawnBuilding(draggingObject.buildingReference, mousePositionIn3D))
            return;
        
        draggingObject.buildingReference.GetComponent<Selectable>().CanUseOutliner = true;
        draggingObject.buildingReference.GetComponent<Selectable>().CanSelect = true;
        draggingObject.buildingReference.colliderReference.enabled = true;
        
        draggingObject.rendererReference.material.color = draggingObject.defaultColor;
        draggingObject = default;
        isDraggingABuilding = false;
        
        
    }
    
    public void CancelDragAndDrop()
    {
        if (!isDraggingABuilding)
            return;
        
        draggingObject = default;
        isDraggingABuilding = false;
        Destroy(draggingObject.buildingReference.transformReference.gameObject);
    }

    private bool CanSpawnBuilding(Building building, Vector3 position)
    {
        var offset = new Vector3(0.2f, 0, 0.2f);
        var buildingRadius = building.colliderReference.radius;
        LayerMask ignoredLayerMasks = ~(1 << 0 | 1 << 1 | 1 << 2 | 1 << 4 | 1 << 5 | 1 << 3);
        var isOverlap = Physics.OverlapBox(position, new Vector3(buildingRadius, buildingRadius, buildingRadius) + offset, Quaternion.identity, ignoredLayerMasks);
        
        if (isOverlap.Length > 0)
            return false;
       
        return true;
    }
}

[Serializable]
public struct DraggingObject
{
    public readonly Building buildingReference;
    public readonly Renderer rendererReference;
    public Color defaultColor;
    public Color draggingColor;
    public Color notBuildableColor;
    
    public DraggingObject(Building buildingReference, Color defaultColor)
    {
        this.buildingReference = buildingReference;
        rendererReference = buildingReference.transformReference.GetChild(0).GetComponent<Renderer>();
        this.defaultColor = defaultColor;
        draggingColor = Utility.Instance.GetUnityColorOfRGBA(new Color(204, 204, 204, 40));
        notBuildableColor = Utility.Instance.GetUnityColorOfRGBA(new Color(255, 0, 56, 40));
    }
}