using Godot;

namespace Exodus3D.Player.PlayerStates;

public abstract class PlayerState
{
    protected PlayerStateMachine _stateMachine;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public abstract void Enter();
    public virtual void Update(double delta) {}
    public virtual void PhysicsUpdate(double delta) {}
    public virtual void UnhandledInput(InputEvent @event) {}
    public abstract void Exit();
}