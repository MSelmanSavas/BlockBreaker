using UnityEngine;

public class GameEntity_Base : MonoBehaviour, IGameEntity
{
    [SerializeField]
    EntityDataDictionary _entityDatas = new();

    [SerializeField]
    EntityActionDictionary _entityActions = new();

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

    public virtual bool OnLoad() { return true; }
    public virtual bool OnAfter() { return true; }
    public virtual bool OnSpawned() { return true; }
    
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

    public bool TryAddAndGetData<T>(out T data) where T : GameEntityData_Base
    {
        if (TryGetData(out data))
            return true;

        return TryAddData(data);
    }
}
