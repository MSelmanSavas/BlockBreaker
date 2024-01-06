using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private void Start()
    {
        GameObject gameSystemsObj = new GameObject
        {
            name = "GameSystems"
        };

        GameSystems gameSystems = gameSystemsObj.AddComponent<GameSystems>();
        gameSystems.TryAddGameSystemByType<GameFieldManager_Default>();
        gameSystems.TryAddGameSystemByType<GameFieldLoader_Random>();

        gameSystems.Initialize();
    }
}
