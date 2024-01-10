using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameScoreSystem : GameSystem_Base
{
    [field: SerializeField]
    public float CurrentScore { get; private set; }

    public UnityAction<float, float> OnCurrentScoreChange;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        RefBook.Add(this);
        CurrentScore = 0f;
        OnCurrentScoreChange?.Invoke(CurrentScore, CurrentScore);
        return true;
    }

    public override bool TryUnInitialize(GameSystems gameSystems)
    {
        if (!base.TryUnInitialize(gameSystems))
            return false;

        RefBook.Remove(this);
        return true;
    }

    public void ChangeScore(float changeAmount)
    {
        float calculatedScore = CurrentScore + changeAmount;

        if (Mathf.Approximately(calculatedScore, CurrentScore))
            return;

        float previousScore = CurrentScore;
        CurrentScore = calculatedScore;

        OnCurrentScoreChange?.Invoke(previousScore, CurrentScore);
    }
}
