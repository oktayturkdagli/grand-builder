public class ResourceStateLooting : ResourceStateBase
{
    public ResourceStateLooting(Resource obj) : base(obj)
    {
        stateId = (int)ResourceStates.Looting;
    }
    
    public override void Update()
    {
        base.Update();
        if (resource.currentLootBarValue <= 0)
        {
            resource.ChangeState((int)ResourceTriggers.ToDestroying);
        }
    }
}