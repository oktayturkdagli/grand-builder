using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grand Master Assets/Buildings/Standard Building")]
public class BuildingSO : ScriptableObject
{
    public string title;
    public Transform prefab;
    public Sprite sprite;
    public BuildingSO parentObject;
    public List<BuildingSO> childObjects;
}
