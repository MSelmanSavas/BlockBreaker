using System.Linq;
using UnityEngine;

public class GameFieldLoader_Random : GameSystem_Base
{
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    ScriptableBlocksStorage _blockStorage;
    GameFieldManager_Default _gameFieldManager;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        if (!gameSystems.TryGetGameSystemByType(out _gameFieldManager))
            return false;

        if (!RefBook.TryGet(out GameConfig gameConfig))
            return false;

        if (gameConfig.BlocksStorage == null)
            return false;

        _blockStorage = gameConfig.BlocksStorage;

        try
        {
            LoadLevelOnGameField(_gameFieldManager, _blockStorage);
        }
        catch (System.Exception e)
        {
            Logger.LogErrorWithTag(LogCategory.GameLoader, $"Error while loading level data to gamefield! Error : {e}");
            return false;
        }

        return true;
    }

    //TODO : Remove position calculation from here and carry all of the position stuff to gamefield manager.
    void LoadLevelOnGameField(GameFieldManager_Default gameFieldManager, ScriptableBlocksStorage scriptableBlocksStorage)
    {
        Vector2Int GameFieldSize = new Vector2Int(10, 20);
        _gameFieldManager.SetGameFieldSize(GameFieldSize);

        for (int y = 0; y < GameFieldSize.y; y++)
            for (int x = 0; x < GameFieldSize.x; x++)
            {
                Vector2Int index = new(x, y);
                Vector2 position = _gameFieldManager.GetGameFieldPositionFromIndex(index);

                GameObject randomBlockPrefab = GetRandomBlockPrefab(scriptableBlocksStorage);
                GameObject spawnedBlock = GameObject.Instantiate(randomBlockPrefab, position, Quaternion.identity);
            }
    }

    GameObject GetRandomBlockPrefab(ScriptableBlocksStorage scriptableBlocksStorage)
    {
        return scriptableBlocksStorage.BlockStorageDatas.ElementAt(Random.Range(0, scriptableBlocksStorage.BlockStorageDatas.Count)).Value.Prefab;
    }
}
