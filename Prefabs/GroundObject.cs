using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Prefabs;

public partial class GroundObject : Node3D, IInteractable
{
    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
    }
    public bool IsInteractable { get; } = true;
    public void Interact()
    {
        Logger.Log($"Interacting with {Name}");
    }
}