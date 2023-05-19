using UnityEngine;

public interface IRotatable
{
    long Id { get; set;}
    GameObject GameObjectReference { get;}
    float RotateDuration { get; set; }
    
    public void Rotate(Quaternion targetRotation, float duration);
}