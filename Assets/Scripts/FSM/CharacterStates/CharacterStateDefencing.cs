public class CharacterStateDefencing : CharacterStateBase
{
    public CharacterStateDefencing(Character obj) : base(obj)
    {
        stateId = (int)CharacterStates.Defencing;
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