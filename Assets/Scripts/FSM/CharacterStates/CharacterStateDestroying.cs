public class CharacterStateDestroying : CharacterStateBase
{
    public CharacterStateDestroying(Character obj) : base(obj)
    {
        stateId = (int)CharacterStates.Destroying;
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