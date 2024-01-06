[System.Serializable]
public class BlockData_Health : GameEntityData_Base
{
    float _currentHealth;
    float _maxHealth;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        _currentHealth = _maxHealth;
        return true;
    }
}
