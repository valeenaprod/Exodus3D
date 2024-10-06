using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Managers;

public partial class PowerManager : Node
{
    public static PowerManager Instance { get; private set; }

    public float PowerLevel { get; private set; } = 100f;

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
            Logger.Log("PowerManager Initialized");
        }
        else
        {
            QueueFree();
        }
    }
    
    public void UsePower(float amount)
    {
        PowerLevel = Mathf.Clamp(PowerLevel - amount, 0, 100);
        Logger.Log($"Power Used. Total Power: {PowerLevel}");
    }

    public void RechargePower(float amount)
    {
        PowerLevel = Mathf.Clamp(PowerLevel + amount, 0, 100);
        Logger.Log($"Power Recharged. Total Power: {PowerLevel}");
    }
    
}