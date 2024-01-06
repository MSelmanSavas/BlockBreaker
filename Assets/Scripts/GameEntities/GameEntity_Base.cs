using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity_Base : MonoBehaviour, IGameEntity
{
    Dictionary<System.Type, GameEntityData_Base> _entityDatas = new();

    public bool TryGetData<T>(out T data) where T : GameEntityData_Base
    {
        if (!_entityDatas.TryGetValue(typeof(T), out GameEntityData_Base baseData))
        {
            data = null;
            return false;
        }

        data = baseData as T;
        return data != null;
    }

    public bool TryAddData<T>(T data) where T : GameEntityData_Base
    {
        if (_entityDatas.ContainsKey(typeof(T)))
            return false;

        _entityDatas.Add(typeof(T), data);
        return true;
    }

    public bool TryRemoveData<T>(T data) where T : GameEntityData_Base
    {
        if (!_entityDatas.ContainsKey(typeof(T)))
            return false;

        return _entityDatas.Remove(typeof(T));
    }
}
