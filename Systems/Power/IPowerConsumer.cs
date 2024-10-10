namespace Exodus3D.Systems.Power;

public interface IPowerConsumer
{
    // Power required by the consumer per tick
    float PowerConsumptionRate { get; }
    
    // Indicates if the consumer is currently powered
    bool IsPowered { get; }

    void TogglePower(bool powerOn);
}