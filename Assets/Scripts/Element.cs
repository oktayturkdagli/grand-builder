using UnityEngine;

public abstract class Element : FiniteStateMachine<Element>, IElement
{
    public virtual long Id { get; set; }
    public GameObject GameObjectReference => gameObject;
    public virtual AllObjects ObjectType { get; set; } = AllObjects.Empty;

    public virtual void OnEnable()
    {
        // Do Something
    }
    
    public virtual void OnDisable()
    {
        // Do Something
    }
    
    public virtual void Awake()
    {
        Id = Utility.Instance.GetId();
    }
    
    public virtual void Start()
    {
        // Do Something
    }
    
    public override void Update()
    {
        base.Update();
        // Do Something
    }
}