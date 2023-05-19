public class BuildingStateDestroying : BuildingStateBase
{
    public BuildingStateDestroying(Building obj) : base(obj)
    {
        stateId = (int)BuildingStates.Destroying;
    }
    
    public override void Enter()
    {
        base.Enter();
        Utility.Instance.DestroyAnObject(building.gameObject);
    }
}