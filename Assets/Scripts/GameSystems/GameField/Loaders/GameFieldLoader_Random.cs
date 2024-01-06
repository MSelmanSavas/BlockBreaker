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
        Vector2 blockWorldSize = scriptableBlocksStorage.BlockWorldSize;
        Vector2 screenLeftRightXBounds = new Vector2(Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x, Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x);
        Vector2 screenUpDownYBounds = new Vector2(Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y, Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y);

        float widthSize = screenLeftRightXBounds.y - screenLeftRightXBounds.x;
        float heightSize = screenUpDownYBounds.y - screenUpDownYBounds.x;

        Vector2Int countToFit = new Vector2Int((int)(widthSize / blockWorldSize.x), (int)(heightSize / blockWorldSize.y));

        Vector2 startPosition = new Vector2
        {
            x = (-blockWorldSize.x / 2f) - ((countToFit.x - 1) / 2f * blockWorldSize.x),
            y = (blockWorldSize.y / 2f) + ((countToFit.y - 1) / 2f * blockWorldSize.y)
        };

        for (int y = 0; y < countToFit.y; y++)
            for (int x = 0; x < countToFit.x; x++)
            {
                Vector2Int index = new(x, y);
                Vector2 offsetPosition = startPosition + new Vector2(x * blockWorldSize.x, y * -blockWorldSize.y);

                GameObject randomBlockPrefab = GetRandomBlockPrefab(scriptableBlocksStorage);
                GameObject spawnedBlock = GameObject.Instantiate(randomBlockPrefab, offsetPosition, Quaternion.identity);
            }
    }

    GameObject GetRandomBlockPrefab(ScriptableBlocksStorage scriptableBlocksStorage)
    {
        return scriptableBlocksStorage.BlockStorageDatas.ElementAt(Random.Range(0, scriptableBlocksStorage.BlockStorageDatas.Count)).Value.Prefab;
    }
}
