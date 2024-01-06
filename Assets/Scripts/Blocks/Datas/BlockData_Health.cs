using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BlockData_Health : GameEntityData_Base
{
    [SerializeField]
    float _currentHealth;

    [SerializeField]
    float _maxHealth;

    public UnityAction<float> OnHealthChange;
    public UnityAction<float> OnMaxHealthChange;
    public UnityAction OnHealthDeplete;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        _currentHealth = _maxHealth;
        return true;
    }

    public void SetHealth(float healthAmount, bool forceEventActivation = false)
    {
        float changedHealth = healthAmount;

        if (!forceEventActivation && Mathf.Approximately(_currentHealth, changedHealth))
            return;

        _currentHealth = changedHealth;
        OnHealthChange?.Invoke(_currentHealth);

        if (Mathf.Approximately(_currentHealth, 0f))
        {
            OnHealthDeplete?.Invoke();
        }
    }

    public void SetMaxHealth(float maxHealthAmount, bool forceEventActivation = false)
    {
        float changedMaxHealth = maxHealthAmount;

        if (!forceEventActivation && Mathf.Approximately(_currentHealth, changedMaxHealth))
            return;

        _maxHealth = changedMaxHealth;
        OnMaxHealthChange?.Invoke(_maxHealth);

        float changedHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

        if (!forceEventActivation && Mathf.Approximately(_currentHealth, changedHealth))
            return;

        _currentHealth = changedHealth;
        OnHealthChange?.Invoke(_currentHealth);

        if (Mathf.Approximately(_currentHealth, 0f))
        {
            OnHealthDeplete?.Invoke();
        }
    }

    public void ChangeHealth(float changeAmount)
    {
        float changedHealth = Mathf.Clamp(_currentHealth + changeAmount, 0f, _maxHealth);

        if (Mathf.Approximately(_currentHealth, changedHealth))
            return;

        _currentHealth = changedHealth;
        OnHealthChange?.Invoke(_currentHealth);

        if (Mathf.Approximately(_currentHealth, 0f))
        {
            OnHealthDeplete?.Invoke();
        }
    }
}
