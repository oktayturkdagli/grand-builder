public abstract class ResourceStateBase : State
{
    public readonly Resource resource;

    protected ResourceStateBase(Resource obj) : base()
    {
        resource = obj;
    }
    
    public override void Enter()
    {
        base.Enter();
        // Utility.Instance.SentMessageToDeveloper("Entered State: " + (BuildingStates)stateId);
    }
    
    public override void Update()
    {
        base.Update();
        // Utility.Instance.SentMessageToDeveloper("Update State: " + (BuildingStates)stateId);
    }
    
    public override void Exit()
    {
        base.Exit();
        // Utility.Instance.SentMessageToDeveloper("Exited State: " + (BuildingStates)stateId);
    }
}

public enum ResourceStates
{
    Idling,
    Looting,
    Destroying
}

public enum ResourceTriggers
{
    ToIdling,
    ToLooting,
    ToDestroying
}