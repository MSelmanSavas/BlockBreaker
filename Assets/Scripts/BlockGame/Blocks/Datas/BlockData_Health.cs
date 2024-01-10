using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BlockData_Health : GameEntityData_Base
{
    [field: SerializeField]
    public float CurrentHealth { get; private set; }

    [field: SerializeField]
    public float MaxHealth { get; private set; }

    [field: SerializeField]
    public bool IsAlreadyDead { get; private set; }

    public UnityAction<float, float> OnHealthChange;
    public UnityAction<float, float> OnMaxHealthChange;
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
        OnHealthChange?.Invoke(CurrentHealth, CurrentHealth);

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

        float previousMaxHealth = MaxHealth;
        MaxHealth = changedMaxHealth;
        OnMaxHealthChange?.Invoke(previousMaxHealth, MaxHealth);

        float changedHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

        if (!forceEventActivation && Mathf.Approximately(CurrentHealth, changedHealth))
            return;

        float previousHealth = CurrentHealth;
        CurrentHealth = changedHealth;
        OnHealthChange?.Invoke(previousHealth, CurrentHealth);

        if (Mathf.Approximately(CurrentHealth, 0f))
        {
            IsAlreadyDead = true;
            OnHealthDeplete?.Invoke();
        }
    }

    public void ChangeHealth(float changeAmount)
    {
        if (IsAlreadyDead)
            return;

        float changedHealth = Mathf.Clamp(CurrentHealth + changeAmount, 0f, MaxHealth);

        if (Mathf.Approximately(CurrentHealth, changedHealth))
            return;

        float previousHealth = CurrentHealth;
        CurrentHealth = changedHealth;
        OnHealthChange?.Invoke(previousHealth, CurrentHealth);

        if (Mathf.Approximately(CurrentHealth, 0f))
        {
            IsAlreadyDead = true;
            OnHealthDeplete?.Invoke();
        }
    }
}
