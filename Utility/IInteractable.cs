namespace Exodus3D.Utility;

public interface IInteractable
{
    bool IsInteractable { get; }
    
    void Interact();
}