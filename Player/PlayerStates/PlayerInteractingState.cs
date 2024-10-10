using System.Linq;
using Exodus3D.Managers;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Player.PlayerStates;

public class PlayerInteractingState : PlayerState
{
    public PlayerInteractingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        Logger.Log("Entered Interacting State");
    }

    public override void UnhandledInput(InputEvent @event)
    {
        if (!Input.IsActionPressed("interact")) return;
        Logger.Log("InteractingState UnhandledInput");
        // Checks if object is an interactable
        IInteractable interactable;
        if ((interactable = DetectInteractable()) == null)
        {
            Logger.Log("No Interactable Detected!");
            base.UnhandledInput(@event);
            _stateMachine.ChangeState(new PlayerIdleState(_stateMachine));
            return;
        }
        interactable.Interact();
        Logger.Log("Interacted with an object");
        _stateMachine.ChangeState(new PlayerIdleState(_stateMachine));
    }

    public override void Exit()
    {
        Logger.Log("Exited Interacting State");
    }
    private static IInteractable DetectInteractable(float maxDistance = 10f)
    {
        Logger.Log("Attempting to detect interactable...");
        // Get the world space from the scene
        var spaceState = GameManager.Instance.Player.GetWorld3D().DirectSpaceState;

        // Define the starting point (player's position)
        Vector3 from = GameManager.Instance.Player.GlobalTransform.Origin; // Player's current position

        // Define the ending point (5 units in front of the player, adjustable as needed)
        Vector3 to = from + GameManager.Instance.Player.GlobalTransform.Basis.Z;

        // Cast the ray and cehck for a collision
        var result = spaceState.IntersectRay(new PhysicsRayQueryParameters3D()
        {
            From = from,
            To = to,
        });
        
        Logger.Log($"Raycast result count: {result.Count}");

        // If something has it
        if (result.Count <= 0 || !result.TryGetValue("collider", out var colliderVariant))
        {
            Logger.Log("No collider found by raycast.");

            return null;
        }
        // Check if the collider os a Node and implements IInteractable
        if (colliderVariant.VariantType == Variant.Type.Nil)
        {
            Logger.Log("Collider variant is Nil.");
            return null;
        }
        // Cast the collider as Node (if it's a node)
        Node collider = colliderVariant.As<Node>();
        if (collider == null)
        {
            Logger.Log("Collider is not a valid node.");
            return null;
        }
        
        // Get the parent node (likely the Node3D that holds the collider)
        Node3D parent = collider.GetParent() as Node3D;

        if (parent is not IInteractable interactable)
        {
            Logger.Log($"Detected object {parent.Name} not implement IInteractable.");

            return null;
        }
        // Calculate the distance between player and object
       // var distance = from.DistanceTo(parent.GlobalTransform.Origin);
        Logger.Log($"Detected interactable object: {parent.Name}");

        return interactable;
    }
}