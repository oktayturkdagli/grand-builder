public class CharacterStateCollecting : CharacterStateBase
{
    public CharacterStateCollecting(Character obj) : base(obj)
    {
        stateId = (int)CharacterStates.Collecting;
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