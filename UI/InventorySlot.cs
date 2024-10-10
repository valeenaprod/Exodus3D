using Exodus3D.Prefabs;
using Godot;

namespace Exodus3D.UI;

public partial class InventorySlot : Resource
{
    public int SlotID { get; private set; }
    public Item Item { get; private set; }
    public int StackSize { get; private set; }

    public void SetItem(Item item)
    {
        Item = item;
    }

    public void SetStackSize(int stacksize)
    {
        StackSize = stacksize;
    }

    public void SetSlotID(int slotid)
    {
        SlotID = slotid;
    }

    public InventorySlot(Item item, int stackSize, int slotid)
    {
        Item = item;
        StackSize = stackSize;
    }

}