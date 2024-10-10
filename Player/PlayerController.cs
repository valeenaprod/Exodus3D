using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Player;

public partial class PlayerController : CharacterBody3D
{
    private SpringArm3D _springArm;
    private Camera3D _camera;
    private Node3D _pivot;
    
    private Vector3 _targetVelocity = Vector3.Zero;
    public float HealthLevel { get; private set; }
    public float FoodLevel { get; private set; }
    public float HydrationLevel { get; private set; }
    
    public float Gravity = 9.8f;

    public int FallAcceleration = 75;
    [Export] public float MovementSpeed { get; private set; } = 40;

    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
        _pivot = GetNode<Node3D>("Pivot");
    }

    public override void _Input(InputEvent @event)
    {

    }
    
    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector3.Zero;

        if (Input.IsActionPressed("move_right"))
        {
            direction.X += 1.0f;
        }
        if (Input.IsActionPressed("move_left"))
        {
            direction.X -= 1.0f;
        }
        if (Input.IsActionPressed("move_back"))
        {
            direction.Z -= 1.0f;
        }
        if (Input.IsActionPressed("move_forward"))
        {
            direction.Z += 1.0f;
        }

        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
        }

        // Ground velocity
        _targetVelocity.X = direction.X * MovementSpeed;
        _targetVelocity.Z = direction.Z * MovementSpeed;

        // Vertical velocity
        if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
        {
            _targetVelocity.Y -= FallAcceleration * (float)delta;
        }

        // Moving the character
        Velocity = _targetVelocity;
        MoveAndSlide();
    }
    
    // Methods to adjust HydrationLevel
    
    public void AddHydration(float amount)
    {
        HydrationLevel = Mathf.Clamp(HydrationLevel + amount, 0, 100);
        Logger.Log($"Hydration Level Augmented: {HydrationLevel}");
    }

    public void DecreaseHydration(float amount)
    {
        HydrationLevel = Mathf.Clamp(HydrationLevel - amount, 0, 100);
        Logger.Log($"Hydration Level Decreased: {HydrationLevel}");
    }

    // Methods to adjust Health
    public void AddHealth(float amount)
    {
        HealthLevel = Mathf.Clamp(HealthLevel + amount, 0, 100);
        Logger.Log($"Health Level Augmented: {HealthLevel}");
    }

    public void DecreaseHealth(float amount)
    {
        HealthLevel = Mathf.Clamp(HealthLevel - amount, 0, 100);
        Logger.Log($"Health Level Decreased: {HealthLevel}");
    }
    // Methods to adjust FoodLevel
    public void AddFood(float amount)
    {
        FoodLevel = Mathf.Clamp(FoodLevel + amount, 0, 100);
        Logger.Log($"Food Level Augmented: {FoodLevel}");
    }

    public void DecreaseFood(float amount)
    {
        FoodLevel = Mathf.Clamp(FoodLevel - amount, 0, 100);
        Logger.Log($"Food Level Decreased: {FoodLevel}");
    }
}