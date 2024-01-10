using UnityEngine;
using TMPro;

public class UIElement_GameCurrentScore : UIElement_Base
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] string _scoreTextPrefix = "Score : ";

    public override void Initialize()
    {
        base.Initialize();

        if (RefBook.TryGet(out GameScoreSystem gameScoreSystem))
        {
            gameScoreSystem.OnCurrentScoreChange += OnCurrentScoreChange;
            OnCurrentScoreChange(gameScoreSystem.CurrentScore, gameScoreSystem.CurrentScore);
        }
    }

    protected override void OnDisableInternal()
    {
        base.OnDisableInternal();

        if (RefBook.TryGet(out GameScoreSystem gameScoreSystem))
            gameScoreSystem.OnCurrentScoreChange -= OnCurrentScoreChange;
    }

    void OnCurrentScoreChange(float previousScore, float currentScore)
    {
        _scoreText?.SetText(_scoreTextPrefix + currentScore.ToString());
    }
}
