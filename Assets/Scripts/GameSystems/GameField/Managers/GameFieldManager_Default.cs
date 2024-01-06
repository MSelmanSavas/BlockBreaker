using System.Collections.Generic;
using UnityEngine;

public class GameFieldManager_Default : GameSystem_Base
{
    Vector2Int GameFieldSize;
    Dictionary<Vector2Int, IGameEntity> _entities = new();
    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        RefBook.Add(this);
        _entities.Clear();

        return true;
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
