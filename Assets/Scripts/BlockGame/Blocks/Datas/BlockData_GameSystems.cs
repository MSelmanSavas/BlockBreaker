[System.Serializable]
public class BlockData_GameSystems : GameEntityData_Base
{
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    GameSystems _connectedGameSystems;
    public GameSystems GetAttachedGameGameSystems() => _connectedGameSystems;
    public void SetAttachedGameGameSystems(GameSystems gameGameSystems) => _connectedGameSystems = gameGameSystems;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        if (!base.TryInitialize(gameEntity))
            return false;

        return true;
    }
}
