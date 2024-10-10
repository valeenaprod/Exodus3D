namespace Exodus3D.Player.PlayerStates;

public abstract class PlayerState
{
    protected PlayerStateMachine _stateMachine;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Update(double delta);
    public abstract void Exit();
}