using Exodus3D.Managers;
using Exodus3D.Prefabs;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.UI;

public partial class InventoryUI : Control
{
    private Label _title;
    private GridContainer _grid;
    private Texture2D _defaultTexture;
    
    public override void _Ready()
    {
        _title = GetNode<Label>("%InventoryTitle");
        _grid = GetNode<GridContainer>("%InventoryGrid");
        _defaultTexture = GD.Load<Texture2D>("res://icon.png");
    }

    private void DisplayInventory()
    {
        var defaultTexture = GD.Load<Texture2D>("res://icon.png");
        foreach (var slot in InventoryManager.Instance.GetInventoryContent().Values)
        {
            var addItem = AddInventoryItem(slot, _grid.GetChild(slot.SlotID));
            if (addItem == false)
            {
                Logger.Log($"Unable to create the inventory slot for {slot.Item.Name}!", Logger.LogLevel.Error);
                break;
            }

            Logger.Log($"{slot.SlotID} with {slot.Item.ItemName} was created!");
        } 
    }

    private bool AddInventoryItem(InventorySlot slot, Node button)
    {
        if (button is not TextureButton) return false;
        TextureRect textureRect = button.GetChild(0) as TextureRect;
        Label label = textureRect?.GetChild(0) as Label;

        textureRect!.Texture = slot.Item.InventoryIcon ?? _defaultTexture;
        label!.Text = slot.StackSize.ToString();
        return true;
    }

    private void OnInventoryButtonPressed(Node button)
    {
        Logger.Log($"Button {button.Name} was clicked!");
    }
    
    
}