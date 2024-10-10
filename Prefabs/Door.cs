using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Prefabs;

public partial class Door : Node3D, IInteractable
{
    public bool IsInteractable { get; } = true;

    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
    }
    public void Interact()
    {
        Logger.Log("Interacting with Door");
        Visible = false;
    }
}