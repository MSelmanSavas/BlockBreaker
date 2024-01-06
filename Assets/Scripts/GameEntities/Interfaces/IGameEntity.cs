using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEntity
{
    public bool TryGetData<T>(out T data) where T : GameEntityData_Base;
    public bool TryAddData<T>(T data) where T : GameEntityData_Base;
    public bool TryRemoveData<T>(T data) where T : GameEntityData_Base;
}
