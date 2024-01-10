using UnityEngine;
using TMPro;

public class UIElement_GameCurrentLife : UIElement_Base
{
    [SerializeField] TextMeshProUGUI _lifeText;
    [SerializeField] string _lifeTextPrefix = "Life : ";

    public override void Initialize()
    {
        base.Initialize();

        if (RefBook.TryGet(out GameplayVariablesSystem gameVariablesSystem))
        {
            gameVariablesSystem.OnCurrentLifeChange += OnCurrentLifeChange;
            OnCurrentLifeChange(gameVariablesSystem.CurrentLife, gameVariablesSystem.MaxLife);
        }
    }

    protected override void OnDisableInternal()
    {
        base.OnDisableInternal();

        if (RefBook.TryGet(out GameplayVariablesSystem gameVariablesSystem))
            gameVariablesSystem.OnCurrentLifeChange -= OnCurrentLifeChange;
    }

    void OnCurrentLifeChange(int currentLife, int maxLife)
    {
        _lifeText?.SetText(_lifeTextPrefix + currentLife.ToString());
    }
}
