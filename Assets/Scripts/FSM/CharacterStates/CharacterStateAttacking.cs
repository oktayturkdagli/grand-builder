public class CharacterStateAttacking : CharacterStateBase
{
    public CharacterStateAttacking(Character obj) : base(obj)
    {
        stateId = (int)CharacterStates.Attacking;
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