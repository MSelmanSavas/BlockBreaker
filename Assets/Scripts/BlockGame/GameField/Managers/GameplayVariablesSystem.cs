using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class GameplayVariablesSystem : GameSystem_Base
{
    public int MaxLife { get; private set; } = 3;
    public int CurrentLife { get; private set; } = 3;

    public UnityAction<int> OnMaxLifeChange;
    public UnityAction<int, int> OnCurrentLifeChange;
    public UnityAction OnCurrentLifeReachesZero;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        CurrentLife = MaxLife;

        OnMaxLifeChange?.Invoke(MaxLife);
        OnCurrentLifeChange?.Invoke(CurrentLife, MaxLife);

        return base.TryInitialize(gameSystems);
    }

    public void ChangeCurrentLife(int amount)
    {
        int calculatedLife = Mathf.Clamp(CurrentLife + amount, 0, MaxLife);

        if (calculatedLife == CurrentLife)
            return;

        CurrentLife = calculatedLife;

        OnCurrentLifeChange?.Invoke(CurrentLife, MaxLife);

        if (CurrentLife <= 0)
        {
            OnCurrentLifeReachesZero?.Invoke();
        }
    }
}
