using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField]
    GameConfig GameConfig;

    private void OnEnable()
    {
        RefBook.Add(GameConfig);
        RefBook.Add(this);
    }

    private void OnDisable()
    {
        RefBook.Remove(this);
    }

    public void InitializeGameLoop()
    {
        GameObject gameSystemsObj = new GameObject
        {
            name = "GameSystems"
        };

        GameSystems gameSystems = gameSystemsObj.AddComponent<GameSystems>();
        gameSystems.TryAddGameSystemByTypeImmediately<GameScoreSystem>(autoInitialize: false);
        gameSystems.TryAddGameSystemByTypeImmediately<GameplayVariablesSystem>(autoInitialize: false);
        gameSystems.TryAddGameSystemByTypeImmediately<GameFieldBoundryLoader>(autoInitialize: false);
        gameSystems.TryAddGameSystemByTypeImmediately<GameFieldManager_Default>(autoInitialize: false);
        gameSystems.TryAddGameSystemByTypeImmediately<GameFieldLoader_Random>(autoInitialize: false);
        gameSystems.TryAddGameSystemByTypeImmediately<GameStateManager>(autoInitialize: false);
        gameSystems.TryAddGameSystemByTypeImmediately<GameFieldPaddleAndBallLoader>(autoInitialize: false);
        gameSystems.TryAddGameSystemByTypeImmediately<PaddleInputManager>(autoInitialize: false);
        gameSystems.TryAddGameSystemByTypeImmediately<BallLaunchSystem>(autoInitialize: false);
        gameSystems.TryAddGameSystemByTypeImmediately<GameStateSetterSystem>(autoInitialize: false);

        gameSystems.Initialize();

        if (gameSystems.TryGetGameSystemByType(out GameStateManager gameStateManager))
            gameStateManager.TrySetGameState(GameState.CanStart);
    }

    public void DeInitializeGameLoop()
    {
        if (RefBook.TryGet(out GameSystems gameSystems))
        {
            gameSystems.DeInitialize();
            Destroy(gameSystems.gameObject);
        }
    }
}
