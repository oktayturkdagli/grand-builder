using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
[RequireComponent(typeof(HealthBar))]
public class Building : Element
{
    [SerializeField] public List<Transform> constructionModels = new List<Transform>();
    public int currentConstructionModel = 0;
    
    public Transform transformReference => transform;
    public SphereCollider colliderReference => GetComponent<SphereCollider>();

    [SerializeField] public ProgressBar progressBar;
    [SerializeField] public HealthBar healthBar;
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float currentHealth = 100f;
    [SerializeField] public float maxProgressBarValue = 100f;
    [SerializeField] public float currentProgressBarValue = 0f;

    // States
    public readonly BuildingStateIdling buildingStateIdling;
    public readonly BuildingStateConstructing buildingStateConstructing;
    public readonly BuildingStateDamaging buildingStateDamaging;
    public readonly BuildingStateBaseRepairing buildingStateRepairing;
    public readonly BuildingStateDestroying buildingStateDestroying;
    
    public Building() 
    {
        buildingStateIdling = new BuildingStateIdling(this);
        buildingStateConstructing = new BuildingStateConstructing(this);
        buildingStateDamaging = new BuildingStateDamaging(this);
        buildingStateRepairing = new BuildingStateBaseRepairing(this);
        buildingStateDestroying = new BuildingStateDestroying(this);
        
        AddState(buildingStateIdling, (int)BuildingTriggers.ToIdling);
        AddState(buildingStateConstructing, (int)BuildingTriggers.ToConstructing);
        AddState(buildingStateDamaging, (int)BuildingTriggers.ToDamaging);
        AddState(buildingStateRepairing, (int)BuildingTriggers.ToRepairing);
        AddState(buildingStateDestroying, (int)BuildingTriggers.ToDestroying);
        
        currentState = buildingStateConstructing;
    }
    
    public override void Start()
    {
        base.Start();
        ObjectType = AllObjects.Building;
    }
    
    public void UpdateProgressBarValue(float value)
    {
        currentProgressBarValue += value;
        
        if (currentProgressBarValue > maxProgressBarValue)
            currentProgressBarValue = maxProgressBarValue;
        
        if (currentProgressBarValue < 0)
            currentProgressBarValue = 0;

        progressBar.UpdateProgressBar(maxProgressBarValue, currentProgressBarValue);
    }
    
    public void UpdateHealthBar(float value)
    {
        currentHealth += value;
        
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        if (currentHealth < 0)
            currentHealth = 0;
        
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
}