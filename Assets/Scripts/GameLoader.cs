using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField]
    GameConfig GameConfig;

    private void Start()
    {
        RefBook.Add(GameConfig);

        GameObject gameSystemsObj = new GameObject
        {
            name = "GameSystems"
        };

        GameSystems gameSystems = gameSystemsObj.AddComponent<GameSystems>();
        gameSystems.TryAddGameSystemByType<GameFieldBoundryLoader>(autoInitialize: false);
        gameSystems.TryAddGameSystemByType<GameFieldManager_Default>(autoInitialize: false);
        gameSystems.TryAddGameSystemByType<GameFieldLoader_Random>(autoInitialize: false);
        gameSystems.TryAddGameSystemByType<GameStateManager>(autoInitialize: false);
        gameSystems.TryAddGameSystemByType<GameFieldPaddleAndBallLoader>(autoInitialize: false);
        gameSystems.TryAddGameSystemByType<PaddleInputManager>(autoInitialize: false);
        gameSystems.TryAddGameSystemByType<BallLaunchSystem>(autoInitialize: false);

        gameSystems.Initialize();
    }
}
