namespace Exodus3D.Systems.Power;

public interface IPowerSource
{
    // Returns the amount of power generated
    float GeneratedPower(); 
    // Checks if the source is currently active
    float PowerOutput { get; }
    bool IsActive { get; }
}