using UnityEngine;

public class ScriptableObjectManager : Singleton<ScriptableObjectManager>
{
    public ScriptableObject LoadScriptableObjects(string path)
    {
        var tempBuildingSOList =  Resources.Load<ScriptableObject>(path);
        return tempBuildingSOList;
    }
}