using Godot;

namespace Exodus3D.Player.PlayerStates;

public partial class PlayerStateMachine : Node3D
{
    private PlayerState _currentState;

    public override void _Ready()
    {
        // Initialize the State Machine with the IdleState
        ChangeState(new PlayerIdleState(this));
    }

    public override void _Process(double delta)
    {
        // Delegates the update logic to the current state
        _currentState.Update(delta);
    }

    public void ChangeState(PlayerState newState)
    {
        // Call the Exit method for the current state (if any)
        _currentState?.Exit();
        _currentState = newState;
        // Enter the new state
        _currentState.Enter();
    }
    
}