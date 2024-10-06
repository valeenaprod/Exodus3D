using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Managers;

public partial class Bootstrap : Node
{
    public override void _Ready()
    {
        // Log Bootstrap Initialization
        Logger.Log("Bootstrap initialized");
        
        // Load the main menu or game scene
        LoadInitialScene();
    }

    private void LoadInitialScene()
    {
        // Load the initial scene (eg. Main Menu)
        const string scenePath = "res://UI/MainMenuScene.tscn"; // Path for Main Menu Scene
        var result = GetTree().ChangeSceneToFile(scenePath);

        if (result != Error.Ok)
        {
            Logger.Log($"Failed to load scene: {scenePath}", Logger.LogLevel.Error);
        }
        else
        {
            Logger.Log($"Successfully loaded scene: {scenePath}");
        }
    }
    
}