public abstract class BuildingStateBase : State
{
    public readonly Building building;

    protected BuildingStateBase(Building obj) : base()
    {
        building = obj;
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

public enum BuildingStates
{
    Dragging,
    Idling,
    Constructing,
    Damaging,
    Repairing,
    Destroying
}

public enum BuildingTriggers
{
    ToIdling,
    ToConstructing,
    ToDamaging,
    ToRepairing,
    ToDestroying
}