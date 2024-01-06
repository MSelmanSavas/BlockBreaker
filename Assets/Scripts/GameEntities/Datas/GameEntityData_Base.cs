[System.Serializable]
public abstract class GameEntityData_Base
{
    /// <summary>
    /// Used for initializing data before any transformation is done on it.
    /// </summary>
    /// <returns></returns> <summary>
    /// Is initialization successful?
    /// </summary>
    /// <returns></returns>
    public virtual bool TryInitialize(IGameEntity gameEntity) { return true; }
}
