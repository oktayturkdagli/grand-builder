using UnityEngine;

public interface IElement
{
    public long Id { get; set; }
    public GameObject GameObjectReference { get; }
    public AllObjects ObjectType { get; set; }
}