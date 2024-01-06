using UnityEngine.Events;

[System.Serializable]
public class BlockData_Health : GameEntityData_Base
{
    float _currentHealth;
    float _maxHealth;
    public UnityAction<float> OnHealthChange;
    public UnityAction OnHealthDeplete;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        _currentHealth = _maxHealth;
        return true;
    }
}
