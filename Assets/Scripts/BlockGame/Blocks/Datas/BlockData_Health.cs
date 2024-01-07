using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BlockData_Health : GameEntityData_Base
{
    [field: SerializeField]
    public float CurrentHealth { get; private set; }

    [field: SerializeField]
    public float MaxHealth { get; private set; }

    public UnityAction<float> OnHealthChange;
    public UnityAction<float> OnMaxHealthChange;
    public UnityAction OnHealthDeplete;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        CurrentHealth = MaxHealth;
        return true;
    }

    public void SetHealth(float healthAmount, bool forceEventActivation = false)
    {
        float changedHealth = healthAmount;

        if (!forceEventActivation && Mathf.Approximately(CurrentHealth, changedHealth))
            return;

        CurrentHealth = changedHealth;
        OnHealthChange?.Invoke(CurrentHealth);

        if (Mathf.Approximately(CurrentHealth, 0f))
        {
            OnHealthDeplete?.Invoke();
        }
    }

    public void SetMaxHealth(float maxHealthAmount, bool forceEventActivation = false)
    {
        float changedMaxHealth = maxHealthAmount;

        if (!forceEventActivation && Mathf.Approximately(CurrentHealth, changedMaxHealth))
            return;

        MaxHealth = changedMaxHealth;
        OnMaxHealthChange?.Invoke(MaxHealth);

        float changedHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

        if (!forceEventActivation && Mathf.Approximately(CurrentHealth, changedHealth))
            return;

        CurrentHealth = changedHealth;
        OnHealthChange?.Invoke(CurrentHealth);

        if (Mathf.Approximately(CurrentHealth, 0f))
        {
            OnHealthDeplete?.Invoke();
        }
    }

    public void ChangeHealth(float changeAmount)
    {
        float changedHealth = Mathf.Clamp(CurrentHealth + changeAmount, 0f, MaxHealth);

        if (Mathf.Approximately(CurrentHealth, changedHealth))
            return;

        CurrentHealth = changedHealth;
        OnHealthChange?.Invoke(CurrentHealth);

        if (Mathf.Approximately(CurrentHealth, 0f))
        {
            OnHealthDeplete?.Invoke();
        }
    }
}
