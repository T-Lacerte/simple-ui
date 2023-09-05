using System;
using System.Collections;
using System.Collections.Generic;
using SimpleUI.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class SystemManager : PersistentSingleton<SystemManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    [SerializeField]
    private SceneLoader _sceneLoader;

    [SerializeField]
    private LoadingManager _loadingManager;

    public GameState State { get; private set; } = GameState.StartingUp;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("SystemManager has awaken");
    }
    
    private void Start()
    {
        ChangeState(GameState.Initializing);
    }

    public void ChangeState(GameState newState) {
        if(newState == State)
            return;
        OnBeforeStateChanged?.Invoke(newState);
        State = newState;
        switch (newState) {
            case GameState.Initializing:
                Initialize();
                break;
            case GameState.InMainMenu:
                HandleMainMenu();
                break;
            case GameState.InPauseMenu:
                HandlePauseMenu();
                break;
            case GameState.InGame:
                break;
            case GameState.StartingUp:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, "StartingUp shouldn't be manualy assigned to the GameManager.");
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }

    private void Initialize() {
        
        //Load main menu scene behind main scene
        //When loaded, change state
        _sceneLoader.Load("MainMenu", () => _loadingManager.ShowLoadingScreen(),
            () =>
            {
                _loadingManager.HideLoadingScreen();
                ChangeState(GameState.InMainMenu);
            });
        
    }

    private void HandleMainMenu() {
        //Focus on title screen
        //hide loading screen
    }

    private void HandlePauseMenu() {

    }
}

[Serializable]
public enum GameState {
    StartingUp = 0,
    Initializing = 1,
    InMainMenu = 2,
    InPauseMenu = 3,
    InGame = 4,
}