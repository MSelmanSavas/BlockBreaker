using UnityEngine;

public class GameEntity_Base : MonoBehaviour, IGameEntity
{
    TypeReferenceInheritedFrom<GameEntityData_Base> _entityDataTypeRefCache = new();

    [SerializeField]
    EntityDataDictionary _entityDatas = new();

    [SerializeField]
    EntityActionDictionary _entityActions = new();

    private void Awake()
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

        AwakeExternal();
    }

    /// <summary>
    /// Any class that inherits this should use this for Awake calls.
    /// Used for preventing accidental overrides on Awake initialization.
    /// </summary>
    protected virtual void AwakeExternal()
    {

    }

    private void OnDisable()
    {
        foreach (var entityDataKV in _entityDatas)
        {
            try
            {
                if (!entityDataKV.Value.OnReset(this))
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
                if (!entityActionsKV.Value.OnReset(this))
                    throw new System.Exception($"Cannot initialize data type : {entityActionsKV.Key}!");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error while initializing data on {gameObject.name}! Error : {e}");
            }
        }
        
        OnDisableExternal();
    }

    /// <summary>
    /// Any class that inherits this should use this for OnDisable calls.
    /// Used for preventing accidental overrides on Awake initialization.
    /// </summary>
    protected virtual void OnDisableExternal()
    {

    }

    public virtual bool OnLoad() { return true; }
    public virtual bool OnAfterLoad() { return true; }
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
