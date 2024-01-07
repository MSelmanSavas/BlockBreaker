[System.Serializable]
public class BlockData_GameFieldManager : GameEntityData_Base
{
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif

    GameFieldManager_Default _connectedFieldManager;
    public GameFieldManager_Default GetAttachedGameFieldManager() => _connectedFieldManager;
    public void SetAttachedGameFieldManager(GameFieldManager_Default gameFieldManager) => _connectedFieldManager = gameFieldManager;
}
