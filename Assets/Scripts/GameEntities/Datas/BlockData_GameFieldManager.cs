[System.Serializable]
public class BlockData_GameFieldManager : GameEntityData_Base
{
    IGameFieldManager _connectedFieldManager;
    public IGameFieldManager GetAttachedGameFieldManager() => _connectedFieldManager;
    public void SetAttachedGameFieldManager(IGameFieldManager gameFieldManager) => _connectedFieldManager = gameFieldManager;
}
