using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    ControlScreen,
    GamePlay,
    CutScene,
    MiniGame
}

/*
 * Sets up and manages game state
 */
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //UpdateGameState(GameState.MainMenu);
        UpdateGameState(GameState.CutScene);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.ControlScreen:
                break;
            case GameState.GamePlay:
                break;
            case GameState.CutScene:
                break;
            case GameState.MiniGame:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }
}
