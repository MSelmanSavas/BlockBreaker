using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement_GameReload : UIElement_Base
{
    public override void Initialize()
    {
        base.Initialize();

        if (RefBook.TryGet(out GameSystems gameSystems))
            if (gameSystems.TryGetGameSystemByType(out GameStateManager gameStateManager))
                gameStateManager.OnGameStateChange += OnGameStateChange;
    }

    protected override void OnDisableInternal()
    {
        if (RefBook.TryGet(out GameSystems gameSystems))
            if (gameSystems.TryGetGameSystemByType(out GameStateManager gameStateManager))
                gameStateManager.OnGameStateChange += OnGameStateChange;
    }

    void OnGameStateChange(GameState previousGameState, GameState currentGameState)
    {
        gameObject.SetActive(currentGameState == GameState.Failed);
    }

    public void ReloadGame()
    {
        if (!RefBook.TryGet(out GameLoader gameLoader))
            return;

        if (!RefBook.TryGet(out UIManager_Game uIManagerGame))
            return;

        gameLoader.DeInitializeGameLoop();
        gameLoader.InitializeGameLoop();

        uIManagerGame.InitializeGameplayUIElements();
        gameObject.SetActive(false);
    }
}
