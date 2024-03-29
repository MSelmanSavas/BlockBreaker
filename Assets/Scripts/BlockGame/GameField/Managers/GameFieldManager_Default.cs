using System.Collections.Generic;
using UnityEngine;

public class GameFieldManager_Default : GameSystem_Base
{
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    public Vector2Int GameFieldSize { get; private set; }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    public Vector2 GameFieldOrigin { get; private set; }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    public Vector2 GameFieldCenter { get; private set; }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    public Vector2 GameFieldOffset { get; private set; }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    public Vector2 GameFieldCellSize { get; private set; }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    Transform _gameFieldParent;

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    Dictionary<Vector2Int, IGameEntity> _entities = new();

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    ScriptableBlocksStorage _blockStorage;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        _entities.Clear();

        if (!RefBook.TryGet(out GameConfig gameConfig))
            return false;

        if (gameConfig.BlocksStorage == null)
            return false;

        RefBook.Add(this);

        _blockStorage = gameConfig.BlocksStorage;

        GameFieldOrigin = Vector2.zero;
        GameFieldCellSize = _blockStorage.BlockWorldSize;
        GameFieldOffset = gameConfig.GameFieldOffset;
        GameFieldCenter = gameConfig.GameFieldCenter;

        _gameFieldParent = new GameObject()
        {
            name = "GameFieldParent"
        }.transform;

        return true;
    }

    public override bool TryDeInitialize(GameSystems gameSystems)
    {
        if (!base.TryDeInitialize(gameSystems))
            return false;

        foreach (var entity in _entities)
        {
            if (entity.Value == null)
                continue;

            if (!entity.Value.TryGetData(out BlockData_GameObject gameObjectData))
                continue;

            GameObject.Destroy(gameObjectData.GetGameObject());
        }

        _entities.Clear();

        GameObject.Destroy(_gameFieldParent.gameObject);

        return true;
    }

    //TODO : When gamefield size is determined, calculate start position of blocks for getting accurate positions on blocks from index.
    public void SetGameFieldSize(Vector2Int gamefieldSize)
    {
        GameFieldSize = gamefieldSize;

        GameFieldOrigin = new Vector2
        {
            x = (-GameFieldCellSize.x / 2f) - ((GameFieldSize.x - 1) / 2f * GameFieldCellSize.x),
            y = (GameFieldCellSize.y / 2f) + ((GameFieldSize.y - 1) / 2f * GameFieldCellSize.y)
        };

        GameFieldOrigin += GameFieldCenter;
        GameFieldOrigin += GameFieldOffset;
    }

    public Vector2 GetGameFieldPositionFromIndex(Vector2Int index)
    {
        return GameFieldOrigin + new Vector2(index.x * GameFieldCellSize.x,
                            index.y * GameFieldCellSize.y);
    }

    public bool TryAddGameEntity(Vector2Int index, IGameEntity gameEntity)
    {
        if (CheckEntityExistance(index))
            return false;

        //TODO : Might introduce bug for assigning instead of adding. Should check if any bad thing happens here!
        if (!CheckIndexExistance(index))
            _entities.Add(index, null);

        _entities[index] = gameEntity;

        if (gameEntity.TryGetOrAddGetData(out BlockData_GameObject gameObjectData))
        {
            gameObjectData.GetGameObject().transform.SetParent(_gameFieldParent);
        }

        if (gameEntity.TryGetOrAddGetData(out BlockData_GameFieldManager gameFieldManagerData))
        {
            gameFieldManagerData.SetAttachedGameFieldManager(this);
        }

        if (gameEntity.TryGetOrAddGetData(out BlockData_GameSystems gameSystemsData))
        {
            gameSystemsData.SetAttachedGameGameSystems(GameSystems);
        }

        if (gameEntity.TryGetOrAddGetData(out BlockData_Index indexData))
        {
            indexData.SetIndices(new List<Vector2Int> { index });
        }

        return true;
    }

    public bool TryRemoveGameEntity(Vector2Int index)
    {
        if (!CheckIndexExistance(index))
            return false;

        if (!CheckEntityExistance(index))
            return false;

        return _entities.Remove(index);
    }

    public bool TryGetEntity(Vector2Int index, out IGameEntity gameEntity)
    {
        if (!CheckEntityExistance(index))
        {
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
            return false;

        if (_entities[index] == null)
            return false;

        return true;
    }
}
