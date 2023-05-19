using UnityEngine;

[RequireComponent(typeof(LootBar))]
public abstract class Resource : Element
{
    public Transform transformReference => transform;
    public SphereCollider colliderReference => GetComponent<SphereCollider>();
    
    [SerializeField] public LootBar lootBar;
    [SerializeField] public float maxLootBarValue = 100f;
    [SerializeField] public float currentLootBarValue = 100f;
    
    // States
    public readonly ResourceStateIdling resourceStateIdling;
    public readonly ResourceStateLooting resourceStateLooting;
    public readonly ResourceStateDestroying resourceStateDestroying;
    
    public Resource() : base()
    {
        resourceStateIdling = new ResourceStateIdling(this);
        resourceStateLooting = new ResourceStateLooting(this);
        resourceStateDestroying = new ResourceStateDestroying(this);
        
        AddState(resourceStateIdling, (int)ResourceTriggers.ToIdling);
        AddState(resourceStateLooting, (int)ResourceTriggers.ToLooting);
        AddState(resourceStateDestroying, (int)ResourceTriggers.ToDestroying);
        
        currentState = resourceStateIdling;
    }
    
    public override void Start()
    {
        base.Start();
        ObjectType = AllObjects.Resource;
    }
}