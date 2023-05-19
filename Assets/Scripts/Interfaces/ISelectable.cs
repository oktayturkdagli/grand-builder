using UnityEngine;

public interface ISelectable
{
    public GameObject GameObjectReference { get;}
    public GameObject CircleBottom { get; set;}
    
    public void OnHover();
    public void OnHoverEnd();
    public void OnSelect();
    public void OnDeSelect();
}