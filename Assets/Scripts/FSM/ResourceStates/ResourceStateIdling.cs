public class ResourceStateIdling : ResourceStateBase
{
    public ResourceStateIdling(Resource obj) : base(obj)
    {
        stateId = (int)ResourceStates.Idling;
    }
    
    public override void Enter()
    {
        base.Enter();
    }
    
    public override void Update()
    {
        base.Update();
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}