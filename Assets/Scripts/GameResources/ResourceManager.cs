using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public void CreateAResources(ResourceSO resourceSO)
    {
        var mousePositionIn3D = EventHandler.Instance.mousePositionIn3D;
        var newGameObject = Instantiate(resourceSO.prefab, mousePositionIn3D, Quaternion.identity);
        var resource = newGameObject.GetComponent<ResourceWood>();
    }
}