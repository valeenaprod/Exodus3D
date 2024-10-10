using System.Collections.Generic;
using System.Linq;
using Exodus3D.Prefabs;
using Exodus3D.UI;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Managers;

public partial class InventoryManager : Node
{
    private Godot.Collections.Dictionary<int, InventorySlot> _inventory = new();
    private List<int> _availableSlots = new();
    
    public static InventoryManager Instance { get; private set; }
    public int MaxInventorySlots { get; private set; } = 20;

    public bool IsInventoryFull => _inventory.Count >= MaxInventorySlots;

    public override void _Ready()
    {
        InitializeSlots();
    }

    private void InitializeSlots()
    {
        for (var i = 0; i < MaxInventorySlots; i++)
        {
            _availableSlots.Add(i);
        }
    }

    private int GetAvailableSlotID()
    {
        if (_availableSlots.Count > 0)
        {
            var slotID = _availableSlots[0];
            _availableSlots.RemoveAt(0);
            return slotID;
        }

        return -1; // No available slot
    }
    public bool AddItem(Item item, int quantity = 1)
    {
        if (IsInventoryFull)
        {
            Logger.Log("The inventory is full!");
            return false;
        }
        var slot = FindItem(item);
        var newStackSize = slot?.StackSize + quantity ?? quantity;
        if (newStackSize > item.MaxStackSize)
        {
            Logger.Log($"Cannot add {newStackSize} {item} to the inventory. The maximum is {item.MaxStackSize}");
            return false;
        }
        var slotID = slot?.SlotID ?? GetAvailableSlotID();
        if (_inventory.ContainsKey(slotID)) slot?.SetStackSize(newStackSize);
        else _inventory.Add(slotID, new InventorySlot(item, quantity, slotID));
        Logger.Log($"{newStackSize} {item.ItemName} have been added to the inventory!");
        return true;
    }

    public bool RemoveItem(Item item, int quantity = 1, bool removeAll = false)
    {
        var slot = FindItem(item);
        if (slot == null || !_inventory.ContainsKey(slot.SlotID))
        {
            Logger.Log($"{item.ItemName} is not in the inventory!");
            return false;
        }

        var newStackSize = slot.StackSize - quantity;
        if (removeAll == true || newStackSize < 1)
        {
            _inventory.Remove(slot.SlotID);
            Logger.Log($"All {item.ItemName} have been removed from the inventory!");
            return true;
        }
        slot.SetStackSize(newStackSize);
        Logger.Log($"{quantity} {item.ItemName} have been removed from the inventory!");
        return true;
    }

    public bool UseItem(Item item)
    {
        var slot = FindItem(item);
        if (slot == null)
        {
            Logger.Log($"{item.ItemName} is not in the inventory!");
            return false;
        }
        _inventory.Remove(slot.SlotID);
        item.UseItem();
        return true;
    }

    private InventorySlot FindItem(Item item)
    {
        return _inventory.Values.FirstOrDefault(slot => slot.Item == item);
    }

    public Godot.Collections.Dictionary<int, InventorySlot> GetInventoryContent()
    {
        return _inventory;
    }
    
}

