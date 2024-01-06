public abstract class GameSystem_Base
{
    [Sirenix.OdinInspector.ShowInInspector]
    public GameSystems GameSystems { get; protected set; }

    public virtual bool TryInitialize(GameSystems gameSystems)
    {
        GameSystems = gameSystems;
        return true;
    }

    public virtual void Update(RuntimeGameSystemContext gameSystemContext) { }
}
