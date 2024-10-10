using System;
using Exodus3D.Systems.Power;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Prefabs;

public partial class Light : Node3D, IPowerConsumer, IInteractable
{

    private OmniLight3D _omniLight;
    public float PowerConsumptionRate { get; private set; } = 5;
    public bool IsPowered { get; private set; }
    public bool IsInteractable { get; private set; } = true;

    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
        _omniLight = GetNode<OmniLight3D>("OmniLight3D");
    }

    public void Interact()
    {
        TogglePower(!IsPowered);
    }
    public void TogglePower(bool powerOn)
    {
        _omniLight.Visible = powerOn;
        IsPowered = powerOn;
    }
}