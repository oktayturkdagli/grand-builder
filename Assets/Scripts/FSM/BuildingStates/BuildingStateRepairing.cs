public class BuildingStateBaseRepairing : BuildingStateBase
{
    public BuildingStateBaseRepairing(Building obj) : base(obj)
    {
        stateId = (int)BuildingStates.Repairing;
    }
    
    public override void Update()
    {
        base.Update();
        if (building.currentHealth >= building.maxHealth)
        {
            if (building.currentProgressBarValue >= building.maxProgressBarValue)
            {
                building.ChangeState((int)BuildingTriggers.ToIdling);
            }
            else
            {
                building.ChangeState((int)BuildingTriggers.ToConstructing);
            }
        }
    }
}