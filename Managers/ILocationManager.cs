namespace Exodus3D.Managers;

public interface ILocationManager
{
    void UpdateResource();
    void TransferResourceTo(ILocationManager otherManager, string resourceType, float amount);
}