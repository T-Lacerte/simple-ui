using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SystemManager : PersistentSingleton<SystemManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

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
  
        
        ChangeState(GameState.InMainMenu);
    }

    private void HandleMainMenu() {

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