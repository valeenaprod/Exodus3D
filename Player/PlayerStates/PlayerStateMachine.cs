using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Player.PlayerStates;

public partial class PlayerStateMachine : Node3D
{
    private PlayerState _currentState;

    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
        // Initialize the State Machine with the IdleState
        ChangeState(new PlayerIdleState(this));
    }

    public override void _Process(double delta)
    {
        // Delegates the update logic to the current state
        _currentState.Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState.PhysicsUpdate(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        _currentState.UnhandledInput(@event);
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