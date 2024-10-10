using Exodus3D.Player;
using Exodus3D.Utility;
using Godot;

namespace Exodus3D.Managers;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
    
    // Game States
    public enum GameState { MainMenu, Playing, Paused, GameOver, Loading }
    public GameState CurrentState { get; private set; }

    public PlayerController Player;

    public override void _Ready()
    {
        // Check if the GameManager instance exists
        if (Instance == null)
        {
            Instance = this;
            // Log GameManager Initialization
            Logger.Log("GameManager Initialized"); 
        }
        else
        {
            QueueFree(); // Ensure only one GameManager exists
            return;
        }
        
        // Set initial game state
        ChangeGameState(GameState.MainMenu);
    }

    public void ChangeGameState(GameState newState)
    {
        CurrentState = newState;
        Logger.Log($"Game State changed to: {CurrentState}");

        switch (CurrentState)
        {
            case GameState.MainMenu:
                ShowMainMenu();
                break;
            case GameState.Playing:
                StartGame();
                break;
            case GameState.Paused:
                PauseGame();
                break;
            case GameState.GameOver:
                EndGame();
                break;
            case GameState.Loading:
                ShowLoadingScreen();
                break;
            default:
                Logger.Log("Unknown game state encountered.", Logger.LogLevel.Error);
                break;
        }
    }

    private void ShowMainMenu()
    {
        Logger.Log("Main Menu displayed");
        // TODO: Display MainMenu
    }

    private void StartGame()
    {
        // Transition to Loading state first
        ChangeGameState(GameState.Loading);
        // Start loading the game scene
        LoadGameScene();
    }

    private async void LoadGameScene()
    {
        Logger.Log("Loading game scene...");
        
        // Example: simulate a loading delay
        
        // Simulate load time
        await ToSignal(GetTree().CreateTimer(2.0f), "timeout");

        var result = GetTree().ChangeSceneToFile("res://Scenes/Maps/GroundMap.tscn");

        if (result != Error.Ok)
        {
            Logger.Log($"Failed to load scene. Error: {result}", Logger.LogLevel.Error);
        }
        else
        {
            Logger.Log("Game Scene Loaded");
            // After loading, transition to Playing State
            ChangeGameState(GameState.Playing);
        }
    }

    private void ShowLoadingScreen()
    {
        Logger.Log("Showing loading screen");
        // TODO: Show loading screen
    }

    private void PauseGame()
    {
        GetTree().Paused = true;
        Logger.Log("Game paused");
    }

    private void EndGame()
    {
        Logger.Log("Game Over");
        // TODO: Load game over scene or show game over ui
    }

    public void ResumeGame()
    {
        GetTree().Paused = false;
        Logger.Log("Game resumed");
    }
    
    
    
}