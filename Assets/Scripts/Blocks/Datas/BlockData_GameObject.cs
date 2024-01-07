using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockData_GameObject : GameEntityData_Base
{
    [SerializeField] GameObject _connectedGameObj;

    public GameObject GetGameObject() => _connectedGameObj;
    public void SetGameObject(GameObject gameObject) => _connectedGameObj = gameObject;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        if (_connectedGameObj != null)
            return true;
        if (gameEntity is not MonoBehaviour monoBehaviour)
            return false;
            
        _connectedGameObj = monoBehaviour.gameObject;
        return true;
    }
}
