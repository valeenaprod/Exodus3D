using System.Diagnostics;
using Godot;
using Exodus3D.Utility;

namespace Exodus3D.Managers
{
    public partial class Bootstrap : Node
    {
        private Node _sceneContainer;
        public static Bootstrap Instance { get; private set; }
        public Node GameScene { get; private set; }
        
        public override void _EnterTree()
        {
            if (Instance == null)
            {
                Instance = this;
                SetProcess(true);
                Logger.Log("Bootstrap Instance Initialized...");
            }
            else
            {
                QueueFree(); 
                Logger.Log("Bootstrap already initialized. Freeing queue.", Logger.LogLevel.Warning);
            }
        }
        public override void _Ready()
        {
            Logger.Log("Loading Initial Scene...");
            _sceneContainer = GetNode("SceneContainer");
            LoadInitialScene();
        }

        private void LoadInitialScene()
        {
            const string initialScenePath = "res://Ground/GroundMap.tscn";
            SwitchScene(initialScenePath);
            CallDeferred(nameof(CallLoadGame));
        }

        private void CallLoadGame()
        {
            GameManager.Instance.LoadGame();
        }
        
        public void SwitchScene(string scenePath)
        {
            _sceneContainer.QueueFreeChildren();
            var newScene = GD.Load<PackedScene>(scenePath);
            var newSceneInstance = newScene?.Instantiate();
            _sceneContainer.AddChild(newSceneInstance);
            GameScene = newSceneInstance;
            Logger.Log($"Scene Switched to: {scenePath}");
        }
    }
}

// Helper method to free al children in the container
public static class NodeExtensions
{
    public static void QueueFreeChildren(this Node node)
    {
        foreach (var child in node.GetChildren()) child.QueueFree();
    }
}