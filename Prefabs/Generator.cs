using Exodus3D.Systems.Power;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Prefabs;

public partial class Generator : Node3D, IPowerSource, IInteractable
{
    
    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
    }
    public float PowerOutput { get; private set; } = 10;
    public float GeneratedPower()
    {
        return PowerOutput;
    }

    public bool IsActive { get; private set; }
    public bool IsInteractable { get; } = true;
    public void Interact()
    {
        Logger.Log($"Generator turned {IsActive}");
        ToggleGenerator();
    }

    public void ToggleGenerator()
    {
        IsActive = !IsActive;
    }
}