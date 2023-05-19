using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotationManager : Singleton<RotationManager>
{
    private class RotationData
    {
        public IRotatable RotatableObj;
        public Quaternion StartRotation;
        public Quaternion EndRotation;
        public float Duration;
        public float ElapsedTime;
    }
    
    private List<RotationData> rotationDataList = new List<RotationData>();
    
    private void Update()
    {
        for (int i = 0; i < rotationDataList.Count; i++)
        {
            RotationData data = rotationDataList[i];
            data.ElapsedTime += Time.deltaTime;
            data.RotatableObj.GameObjectReference.transform.rotation = Quaternion.Slerp(data.StartRotation, data.EndRotation, data.ElapsedTime / data.Duration);
            if (data.ElapsedTime >= data.Duration)
            {
                data.RotatableObj.GameObjectReference.transform.rotation = data.EndRotation;
                rotationDataList.RemoveAt(i);
                i--;
            }
        }
    }
    
    public void RotateOverTime(IRotatable rotatableObj, Quaternion endRotation, float duration)
    {
        RotationData data = new RotationData
        {
            RotatableObj = rotatableObj,
            StartRotation = gameObject.transform.rotation,
            EndRotation = endRotation,
            Duration = duration,
            ElapsedTime = 0
        };
        
        var item = rotationDataList.SingleOrDefault(x => x.RotatableObj.Id == data.RotatableObj.Id);
        if (item != null)
            rotationDataList.Remove(item);

        rotationDataList.Add(data);
    }
}