public class ResourceStateDestroying: ResourceStateBase
{
    public ResourceStateDestroying(Resource obj) : base(obj)
    {
        stateId = (int)ResourceStates.Destroying;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        Utility.Instance.DestroyAnObject(resource.gameObject);
    }
}