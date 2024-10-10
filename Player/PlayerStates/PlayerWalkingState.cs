using Exodus3D.Managers;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Player.PlayerStates;

public class PlayerWalkingState : PlayerState
{
    private Vector3 _velocity;

    private PlayerController _playerController;
    public PlayerWalkingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        Logger.Log("Entering Walking State");
        // Any setup logic for entering walking state
        _playerController = GameManager.Instance.Player;
    }

   /* public override void PhysicsUpdate(double delta)
    {
        var targetVelocity = Vector3.Zero;

        var direction = Vector3.Zero;

        if (Input.IsActionPressed("move_right"))
            direction.X += 1.0f;
        if (Input.IsActionPressed("move_left"))
            direction.X -= 1.0f;
        if (Input.IsActionPressed("move_back"))
            direction.Z += 1.0f;
        if (Input.IsActionPressed("move_forward"))
            direction.X -= 1.0f;
        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            _playerController.GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
        }

        targetVelocity.X = direction.X * _playerController.MovementSpeed;
        targetVelocity.Z = direction.Z * _playerController.MovementSpeed;

        if (_playerController.IsOnFloor())
            targetVelocity.Y -= _playerController.FallAcceleration * (float)delta;

        _playerController.Velocity = targetVelocity;
        _playerController.MoveAndSlide();
    }*/

    public override void Exit()
    {
        Logger.Log("Exiting Walking State");
        // Any cleanup logic when exiting walking state
    }
    
}