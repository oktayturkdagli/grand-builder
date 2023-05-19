using System.Collections.Generic;
using UnityEngine;

public class FormationManager : Singleton<FormationManager>
{
    // Get Formation
    public List<Vector3> CovertToFormation(int formationSize, Vector3 targetPoint, FormationTypes formationTypes = FormationTypes.Standard, float formationRadius = 1f, float spacing = 2f)
    {
        switch (formationTypes)
        {
            case FormationTypes.Standard: default:
                return CovertToStandardFormation(formationSize, targetPoint, spacing);
            case FormationTypes.Box:
                return CovertToBoxFormation(formationSize, targetPoint, spacing);
            case FormationTypes.Ring:
                return CovertToRingFormation(formationSize, targetPoint, formationRadius, spacing);
            case FormationTypes.OneRing:
                return CovertToOneRingFormation(formationSize, targetPoint, formationRadius, spacing);
        }
    }
    
    // Standard Formation
    private List<Vector3> CovertToStandardFormation(int formationSize, Vector3 targetPoint, float spacing = 2f)
    {
        List<Vector3> formationList = new List<Vector3>();
        List<float> ringDistanceArray = new List<float>(); 
        List<int> ringPositionCountArray = new List<int>(); 
        
        // Calculate ringDistanceArray and ringPositionCountArray
        int counter = 1, sum = 0;
        while (sum < formationSize)
        {
            ringDistanceArray.Add(counter);
            ringPositionCountArray.Add(counter * 5);
            sum += counter * 5;
            counter++;
        }

        List<Vector3> targetPositionList = GetStandardFormationAroundCircleGroups(targetPoint, ringDistanceArray, ringPositionCountArray, spacing);
        
        for (int i = 0; i < formationSize; i++)
        {
            targetPositionList[i] = new Vector3(targetPositionList[i].x, 0, targetPositionList[i].z); // Reset y axis
            formationList.Add(targetPositionList[i]);
        }
        
        return  formationList;
    }
    
    private List<Vector3> GetStandardFormationAroundCircleGroups(Vector3 targetPosition, List<float> ringDistanceArray, List<int> ringPositionCountArray, float spacing = 2f)
    {
        List<Vector3> PositionList = new List<Vector3>();
        PositionList.Add(targetPosition);
        for (int i = 0; i < ringDistanceArray.Count; i++)
        {
            PositionList.AddRange(GetStandardFormationAroundCircle(targetPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return PositionList;
    }
    
    private List<Vector3> GetStandardFormationAroundCircle(Vector3 targetPosition, float distance, int positionCount, float spacing = 2f)
    {
        List<Vector3> PositionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = Quaternion.Euler(0, angle, 0) * new Vector3(1, 1, 0); // ApplyRotationVector
            Vector3 position = targetPosition + dir * (distance * spacing);
            PositionList.Add(position);
        }
        return PositionList;
    }
    
    // Box Formation
    private List<Vector3> CovertToBoxFormation(int formationSize, Vector3 targetPoint, float spacing = 2f)
    {
        List<Vector3> formationList = new List<Vector3>();
        
        // Calculate width and depth
        var boxDepth = (int)Mathf.Sqrt(formationSize);
        var boxWidth = formationSize / boxDepth;
        if (formationSize % boxDepth != 0)
        {
            boxWidth++;
        }

        // Find the positions
        var tempList = new Vector3[formationSize];
        for (int i = 0; i < boxWidth; i++)
        {
            for (int j = 0; j < boxDepth; j++)
            {
                int index = i * boxDepth + j;
                
                if (index >= formationSize)
                    break;
                
                tempList[index] = new Vector3(j * spacing, 0, i * spacing) + targetPoint;
            }
        }
        
        // Calculate box formation origin point (this finds exactly center point of box)
        Vector3 origin = new Vector3((boxDepth - 1) * spacing / 2, 0, (boxWidth - 1) * spacing / 2);
        for (int i = 0; i < formationSize; i++)
        {
            tempList[i] -= origin;
        }
        
        for (int i = 0; i < tempList.Length; i++)
        {
            formationList.Add(tempList[i]);
        }
        
        return formationList;
    }
    
    // Ring Formation
    private List<Vector3> CovertToRingFormation(int formationSize, Vector3 targetPoint, float formationRadius = 1f, float spacing = 2f)
    {
        // Reference: https://www.youtube.com/watch?v=NEFxWkTRVCc
        float radius = formationRadius;
        float ringOffset = 1f; // Next ring radius (radius + ringOffset)
        float radiusGrowthMultiplier = 0f; 
        float rotations = 1f;
        var unitGrowthMultiplier = 10; // First ring unit amount
        float nthOffset = 0;
        
        int numberOfRings = Mathf.CeilToInt(Mathf.Sqrt(formationSize / 5f + 0.25f) - 0.5f);
        
        List<Vector3> formationList = new List<Vector3>();
        var totalRingOffset = 0f;
        for (var i = 0; i < numberOfRings; i++)
        {
            var amountPerRing = unitGrowthMultiplier * (i + 1);
            for (var j = 0; j < amountPerRing; j++)
            {
                var angle = j * Mathf.PI * (2 * rotations) / amountPerRing + (i % 2 != 0 ? nthOffset : 0);
                
                var newRadius = radius + totalRingOffset + j * radiusGrowthMultiplier;
                var x = Mathf.Cos(angle) * newRadius;
                var z = Mathf.Sin(angle) * newRadius;
                
                var pos = new Vector3(x, 0, z);
                // pos += GetNoise(pos);
                pos *= spacing;
                
                formationList.Add(pos + targetPoint);
            }
            totalRingOffset += ringOffset;
        }
        
        return formationList;
    }
    
    // One Ring Formation
    private List<Vector3> CovertToOneRingFormation(int formationSize, Vector3 targetPoint, float formationRadius = 1f, float spacing = 1f)
    {
        // Reference: https://www.youtube.com/watch?v=NEFxWkTRVCc
        float radius = formationRadius;
        float ringOffset = 1f; // Next ring radius (radius + ringOffset)
        float radiusGrowthMultiplier = 0f; 
        float rotations = 1f;
        float nthOffset = 0;
        
        int numberOfRings = 1;
        
        List<Vector3> formationList = new List<Vector3>();
        var totalRingOffset = 0f;
        for (var i = 0; i < numberOfRings; i++)
        {
            var amountPerRing = formationSize;
            for (var j = 0; j < amountPerRing; j++)
            {
                var angle = j * Mathf.PI * (2 * rotations) / amountPerRing + (i % 2 != 0 ? nthOffset : 0);
                
                var newRadius = radius + totalRingOffset + j * radiusGrowthMultiplier;
                var x = Mathf.Cos(angle) * newRadius;
                var z = Mathf.Sin(angle) * newRadius;
                
                var pos = new Vector3(x, 0, z);
                // pos += GetNoise(pos);
                pos *= spacing;
                
                formationList.Add(pos + targetPoint);
            }
            totalRingOffset += ringOffset;
        }
        
        return formationList;
    }
}

public enum FormationTypes
{
    Standard,
    Box,
    Ring,
    OneRing
}