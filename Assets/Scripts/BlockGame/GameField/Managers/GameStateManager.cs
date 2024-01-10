using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : GameSystem_Base
{
    public GameState CurrentGameState { get; private set; }
    public UnityAction<GameState, GameState> OnGameStateChange;

    public bool TrySetGameState(GameState gameState)
    {
        if (gameState == CurrentGameState)
            return false;

        GameState previousGameState = CurrentGameState;
        CurrentGameState = gameState;

        OnGameStateChange?.Invoke(previousGameState, CurrentGameState);
        return true;
    }

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        CurrentGameState = GameState.NotStarted;
        return true;
    }
}
