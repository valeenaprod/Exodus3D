using Exodus3D.Managers;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Player.PlayerStates;

public class PlayerWalkingState : PlayerState
{
    private Vector3 _velocity;
    public PlayerWalkingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        Logger.Log("Entering Walking State");
        // Any setup logic for entering walking state
    }

    public override void Update(double delta)
    {
        // Handle player movement logic
        if (Input.IsActionPressed("move_up"))
        {
            _velocity.Y += 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            _velocity.Y -= 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            _velocity.X -= 1;
        }

        if (Input.IsActionPressed("move_right"))
        {
            _velocity.X += 1;
        }

        var deltaFloat = (float)delta;
        GameManager.Instance.Player.Translate(_velocity * deltaFloat * 5f);
        
        // Switch back to IdleState if there's no input
        if (!Input.IsActionPressed("move_up") && !Input.IsActionPressed("move_down") 
        && !Input.IsActionPressed("move_left") && !Input.IsActionPressed("move_right"))
        {
            _stateMachine.ChangeState(new PlayerIdleState(_stateMachine));
        }
    }

    public override void Exit()
    {
        Logger.Log("Exiting Walking State");
        // Any cleanup logic when exiting walking state
    }
    
}