using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Player.PlayerStates;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        Logger.Log("Enter Idle State");
        // Any setup logic for entering idle state
    }

    public override void Update(double delta)
    {
        // Check for movement input to switch to WalkingState
        if (Input.IsActionPressed("move_up") || Input.IsActionPressed("move_down") ||
            Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right"))
        {
            _stateMachine.ChangeState(new PlayerWalkingState(_stateMachine));
        }
    }

    public override void Exit()
    {
        Logger.Log("Exiting Idle State");
        // Any cleanup logic when exiting idle state
    }

}   