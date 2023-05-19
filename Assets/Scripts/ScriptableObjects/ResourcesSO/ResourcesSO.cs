using UnityEngine;

[CreateAssetMenu(menuName = "Grand Master Assets/Resources/Standard Resource")]
public class ResourceSO : ScriptableObject
{
    public string title;
    public Transform prefab;
    public Sprite sprite;
    public float maxLootBarValue;
}