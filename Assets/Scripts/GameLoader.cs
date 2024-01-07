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
        gameSystems.TryAddGameSystemByType<GameFieldBoundryLoader>();
        gameSystems.TryAddGameSystemByType<GameFieldManager_Default>();
        gameSystems.TryAddGameSystemByType<GameFieldLoader_Random>();
        gameSystems.TryAddGameSystemByType<GameStateManager>();
        gameSystems.TryAddGameSystemByType<GameFieldPaddleAndBallLoader>();
        gameSystems.TryAddGameSystemByType<GameInputManager>();

        gameSystems.Initialize();
    }
}
