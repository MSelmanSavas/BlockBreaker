using UnityEngine;

public class GameEntity_Base : MonoBehaviour, IGameEntity
{
    TypeReferenceInheritedFrom<GameEntityData_Base> _entityDataTypeRefCache = new();
    TypeReferenceInheritedFrom<GameEntityAction_Base> _entityActionTypeRefCache = new();

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

    public bool TryGetData<T>(out T data) where T : GameEntityData_Base, new()
    {
        _entityDataTypeRefCache.Type = typeof(T);

        if (!_entityDatas.TryGetValue(_entityDataTypeRefCache, out GameEntityData_Base baseData))
        {
            data = null;
            return false;
        }

        data = baseData as T;
        return data != null;
    }

    public bool TryAddData<T>(T data) where T : GameEntityData_Base, new()
    {
        _entityDataTypeRefCache.Type = typeof(T);
        
        if (_entityDatas.ContainsKey(typeof(T)))
            return false;

        if (!data.TryInitialize(this))
            return false;

        _entityDatas.Add(typeof(T), data);
        return true;
    }

    public bool TryRemoveData<T>(T data) where T : GameEntityData_Base, new()
    {
        _entityDataTypeRefCache.Type = typeof(T);

        if (!_entityDatas.ContainsKey(_entityDataTypeRefCache))
            return false;

        return _entityDatas.Remove(_entityDataTypeRefCache);
    }

    public bool TryGetOrAddGetData<T>(out T data) where T : GameEntityData_Base, new()
    {
        if (TryGetData(out data))
            return true;

        data = new T();
        return TryAddData(data);
    }
}
