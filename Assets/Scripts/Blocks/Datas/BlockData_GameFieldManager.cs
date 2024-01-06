using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData_GameFieldManager : MonoBehaviour
{
    IGameFieldManager _connectedFieldManager;
    public IGameFieldManager GetAttachedGameFieldManager() => _connectedFieldManager;
    public void SetAttachedGameFieldManager(IGameFieldManager gameFieldManager) => _connectedFieldManager = gameFieldManager;
}
