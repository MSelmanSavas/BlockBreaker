using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity_Base : MonoBehaviour, IGameEntity
{
    Dictionary<System.Type, GameEntityData_Base> _entityDatas = new();
    Dictionary<System.Type, GameEntityAction_Base> _entityActions = new();

    protected virtual void Awake()
    {
        foreach (var entityDataKV in _entityDatas)
        {
            try
            {
                if (!entityDataKV.Value.TryInitialize(this))
                    throw new System.Exception($"Cannot initialize data type : {entityDataKV.Key}!");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error while initializing data on {gameObject.name}! Error : {e}");
            }
        }

        foreach (var entityActionsKV in _entityActions)
        {
            try
            {
                if (!entityActionsKV.Value.TryInitialize(this))
                    throw new System.Exception($"Cannot initialize data type : {entityActionsKV.Key}!");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error while initializing data on {gameObject.name}! Error : {e}");
            }
        }
    }

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
