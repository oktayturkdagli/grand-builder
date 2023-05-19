using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grand Master Assets/Resources/Resource List")]
public class ResourceListSO : ScriptableObject
{
    public List<ResourceSO> resources;
}