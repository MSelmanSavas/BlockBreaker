using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEntity
{
    public virtual bool OnLoad() { return true; }
    public virtual bool OnAfterLoad() { return true; }
    public virtual bool OnSpawned() { return true; }
    public bool TryGetData<T>(out T data) where T : GameEntityData_Base, new();
    public bool TryAddData<T>(T data) where T : GameEntityData_Base, new();
    public bool TryGetOrAddGetData<T>(out T data) where T : GameEntityData_Base, new();
    public bool TryRemoveData<T>(T data) where T : GameEntityData_Base, new();
}
