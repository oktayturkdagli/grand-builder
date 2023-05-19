public class BuildingStateDamaging : BuildingStateBase
{
    public BuildingStateDamaging(Building obj) : base(obj)
    {
        stateId = (int)BuildingStates.Damaging;
    }

    public override void Update()
    {
        base.Update();
        if (building.currentHealth <= 0)
        {
            building.ChangeState((int)BuildingTriggers.ToDestroying);
        }
    }
}