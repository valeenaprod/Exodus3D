using System.Collections.Generic;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Managers;

public partial class ResourceManager : Node3D
{
    private Dictionary<string, ILocationManager> _locationManagers = new();

    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
    }
    public void RegisterLocation(string locationId, ILocationManager manager)
    {
        _locationManagers[locationId] = manager;
    }

    public void UnregisterLocation(string locationId)
    {
        _locationManagers.Remove(locationId);
    }

    public void TransferResource(string fromLocation, string toLocation, string resourceType, float amount)
    {
        if (_locationManagers.TryGetValue(fromLocation, out var from) &&
            _locationManagers.TryGetValue(toLocation, out var to))
        {
            from.TransferResourceTo(to, resourceType, amount);
        }
    }
    
    
}