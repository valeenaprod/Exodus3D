using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Player;

public partial class PlayerController : Node3D
{
    public float HealthLevel { get; private set; }
    public float FoodLevel { get; private set; }
    public float HydrationLevel { get; private set; }

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