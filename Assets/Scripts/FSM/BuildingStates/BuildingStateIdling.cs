public class BuildingStateIdling : BuildingStateBase
{
    public BuildingStateIdling(Building obj) : base(obj)
    {
        stateId = (int)BuildingStates.Idling;
    }

    public override void Update()
    {
        base.Update();
        if (building.currentHealth < building.maxHealth)
        {
            building.ChangeState((int)BuildingTriggers.ToDamaging);
        }
    }
}