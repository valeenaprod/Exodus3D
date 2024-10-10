using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Prefabs;

public partial class Item : Node3D
{
    private string _itemName;

    [Export]
    public string ItemName
    {
        get => _itemName;
        set
        {
            _itemName = value;
            Name = $"{_itemName} [{ItemType}]";
        }
    }
    [Export] public string Description { get; set; }
    [Export] public int MaxStackSize { get; set; } = 1;
    [Export] public ItemTypes ItemType { get; set; }

    [Export] public Texture2D InventoryIcon;
    
    public bool IsStackable => MaxStackSize > 1;

    public bool IsUsable = true;
    
    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
    }
    public enum ItemTypes
    {
        Food,
        Resource,
        Armor,
        Weapon
    }
    
    // Additional properties like weight, type, etc
    public Item(string name, string description, int maxStackSize, ItemTypes itemType)
    {
        ItemName = name;
        Description = description;
        MaxStackSize = maxStackSize;
        ItemType = itemType;
    }

    public void UseItem()
    {
        Logger.Log("Item has been used!");
    }

    public Item()
    {
    }
}