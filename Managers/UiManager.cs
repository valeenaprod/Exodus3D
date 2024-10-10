using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Managers;

public partial class UiManager : CanvasLayer
{
    // Singleton instance for global access
    public static UiManager Instance { get; private set; }
    
    // Nodes for different UI element
    private Control _hud;
    private Control _mainMenu;
    private Control _pauseMenu;

    // Nodes for HUD Labels
    private Label _foodLabel;
    private Label _powerLabel;
    private Label _healthLabel;
    private Label _hydrationLabel;

    public override void _Ready()
    {
        Logger.Log($"_Ready called in {Name}, tree available: {GetTree() != null}");
        if (Instance == null)
        {
            Instance = this;
            Logger.Log("UIManager Initialized");
        }
        else
        {
            // Ensure only one instance exists
            QueueFree();
        }
        
        // Initialize UI Nodes
        _hud = GetNode<Control>("HUD");
        _mainMenu = GetNode<Control>("MainMenu");
        _pauseMenu = GetNode<Control>("PauseMenu");
        
        // Initialize HUD Nodes
        _foodLabel = GetNode<Label>("%FoodLabel");
        _powerLabel = GetNode<Label>("%PowerLabel");
        _healthLabel = GetNode<Label>("%HealthLabel");
        _hydrationLabel = GetNode<Label>("%HydrationLabel");
        
        // Set Initial Visibility
        ShowMainMenu(true);
        ShowHUD(false);
        ShowPauseMenu(false);
    }
    
    // Show/Hide HUD
    public void ShowHUD(bool show)
    {
        if (_hud == null) return;
        _hud.Visible = show;
        Logger.Log($"HUD Visibility set to: {show}");
    }
    
    // Show/Hide Main Menu
    public void ShowMainMenu(bool show)
    {
        if (_mainMenu == null) return;
        _mainMenu.Visible = show;
        Logger.Log($"Main Menu Visibility set to: {show}");
    }
    
    // Show/Hide Pause Menu
    public void ShowPauseMenu(bool show)
    {
        if (_pauseMenu == null) return;
        _pauseMenu.Visible = show;
        Logger.Log($"Pause Menu Visibility set to: {show}");
    }

    private void UpdateHUD()
    {
        if (GameManager.Instance.Player == null) return;
        _foodLabel.Text = $"FoodLevel: {GameManager.Instance.Player.FoodLevel}";
        _healthLabel.Text = $"HealthLevel: {GameManager.Instance.Player.HealthLevel}";
        _hydrationLabel.Text = $"HydrationLevel: {GameManager.Instance.Player.HydrationLevel}";
        _powerLabel.Text = $"PowerLevel: {PowerManager.Instance.AvailablePower}";
    }

    public override void _Process(double delta)
    {
        UpdateHUD();
    }
    
}