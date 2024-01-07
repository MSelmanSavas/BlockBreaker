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

    /// <summary>
    /// Used for resetting data if necessary. Can be used for pooling.
    /// </summary>
    /// <param name="gameEntity"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="gameEntity"></param>
    /// <returns></returns>
    public virtual bool OnReset(IGameEntity gameEntity) { return true; }
}
