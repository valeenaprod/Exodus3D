using System.Collections.Generic;
using System.Linq;
using Exodus3D.Systems.Power;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Managers;

public partial class PowerManager : Node
{
    private List<IPowerSource> _powerSources = new();
    private List<IPowerConsumer> _powerConsumers = new();
    
    public static PowerManager Instance { get; private set; }
    public float TotalPowerGenerated { get; private set; }
    public float TotalPowerConsumed { get; private set; }
    public float AvailablePower { get; private set; }

    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
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
    
    public void AddPowerSource(IPowerSource source)
    {
        _powerSources.Add(source);
    }

    public void RemovePowerSource(IPowerSource source)
    {
        _powerSources.Remove(source);
    }

    public void AddPowerConsumer(IPowerConsumer consumer)
    {
        _powerConsumers.Add(consumer);
    }

    public void RemovePowerConsumer(IPowerConsumer consumer)
    {
        _powerConsumers.Remove(consumer);
    }

    public void UpdatePower()
    {
        GeneratePower();
        DistributePower();
    }

    private void GeneratePower()
    {
        TotalPowerGenerated = 0;
        foreach (var source in _powerSources.Where(source => source.IsActive))
        {
            TotalPowerGenerated += source.GeneratedPower();
        }

        AvailablePower += TotalPowerGenerated;
    }

    private void DistributePower()
    {
        TotalPowerConsumed = 0;
        foreach (var consumer in _powerConsumers)
        {
            if (AvailablePower >= consumer.PowerConsumptionRate)
            {
                consumer.TogglePower(true);
                AvailablePower -= consumer.PowerConsumptionRate;
                TotalPowerConsumed += consumer.PowerConsumptionRate;
            }
            else
            {
                consumer.TogglePower(false);
            }
        }
    }
    
}