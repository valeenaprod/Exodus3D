using Exodus3D.Player;
using Godot;
using Exodus3D.Utility;

namespace Exodus3D.Managers
{
    public partial class GameManager : Node
    {
        public static GameManager Instance { get; private set; }

        public enum GameState { MainMenu, Playing, Paused, GameOver, Loading }
        public GameState CurrentState { get; private set; }

        public PlayerController Player;

        public override void _EnterTree()
        {
            if (Instance == null)
            {
                Instance = this;
                SetProcess(true);
                Logger.Log("GameManager Instance Loaded");
            }
            else
            {
                QueueFree();
                Logger.Log("GameManager Instance was already loaded. Freeing queue.");
            }
        }
        public override void _Ready()
        {

        }

        public void LoadGame()
        {
            ChangeGameState(GameState.Playing);
            Player = Bootstrap.Instance.GameScene.GetNode<PlayerController>("PlayerScene");
            Logger.Log($"Global Player Property Initialized: {Player}");
        }

        public void ChangeGameState(GameState newState)
        {
            CurrentState = newState;
            Logger.Log($"Game State changed to: {CurrentState}", Logger.LogLevel.Info);
            // Avoid direct scene transitions here
        }

        public override void _ExitTree()
        {
            if (Instance != this) return;
            Logger.Log("GameManager instance exiting", Logger.LogLevel.Info);
            Instance = null;
        }
    }
}