using System.Collections.Generic;
using UnityEngine;

public class GameFieldManager_Default : GameSystem_Base
{
    public Vector2Int GameFieldSize { get; private set; }
    public Vector2 GameFieldCenter { get; private set; }
    public Vector2 GameFieldCellSize { get; private set; }
    Transform _gameFieldParent;
    Dictionary<Vector2Int, IGameEntity> _entities = new();

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    ScriptableBlocksStorage _blockStorage;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        RefBook.Add(this);
        _entities.Clear();

        if (!RefBook.TryGet(out GameConfig gameConfig))
            return false;

        if (gameConfig.BlocksStorage == null)
            return false;

        _blockStorage = gameConfig.BlocksStorage;

        GameFieldCenter = Vector2.zero;

        _gameFieldParent = new GameObject()
        {
            name = "GameFieldParent"
        }.transform;

        return true;
    }

    //TODO : When gamefield size is determined, calculate start position of blocks for getting accurate positions on blocks from index.
    public void SetGameFieldSize(Vector2Int gamefieldSize)
    {
        GameFieldSize = gamefieldSize;
    }

    public Vector2 GetGameFieldPositionFromIndex(Vector2Int index)
    {
        Vector2 blockWorldSize = _blockStorage.BlockWorldSize;
        GameFieldCellSize = blockWorldSize;

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

        return Vector2.zero;
    }

    public bool TryAddGameEntity(Vector2Int index, IGameEntity gameEntity)
    {
        if (CheckEntityExistance(index))
        {
            Logger.LogErrorWithTag(LogCategory.GameField, $"There is already another entity on index : {index} on gamefield manager : {GetType()}! Cannot add entity : {gameEntity}...");
            return false;
        }

        //TODO : Might introduce bug for assigning instead of adding. Should check if any bad thing happens here!
        if (!CheckIndexExistance(index))
            _entities.Add(index, null);

        _entities[index] = gameEntity;
        return true;
    }

    public bool TryRemoveGameEntity(Vector2Int index)
    {
        if (!CheckIndexExistance(index))
        {
            Logger.LogErrorWithTag(LogCategory.GameField, $"There is no index : {index} on gamefield manager : {GetType()}! Cannot remove entity...");
            return false;
        }

        if (!CheckEntityExistance(index))
        {
            Logger.LogErrorWithTag(LogCategory.GameField, $"There is no entity on index : {index} on gamefield manager : {GetType()}! Cannot remove entity...");
            return false;
        }

        return _entities.Remove(index);
    }

    public bool TryGetEntity(Vector2Int index, out IGameEntity gameEntity)
    {
        if (!CheckEntityExistance(index))
        {
            Logger.LogErrorWithTag(LogCategory.GameField, $"There is no entity on index : {index} on gamefield manager : {GetType()}! Cannot get entity...");
            gameEntity = null;
            return false;
        }

        gameEntity = _entities[index];
        return true;
    }

    private bool CheckIndexExistance(Vector2Int index) => _entities.ContainsKey(index);

    private bool CheckEntityExistance(Vector2Int index)
    {
        if (!CheckIndexExistance(index))
            return true;

        if (_entities[index] == null)
            return true;

        return false;
    }
}
