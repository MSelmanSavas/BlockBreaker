using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_Game : MonoBehaviour
{
    [field: SerializeField]
    public List<UIElement_Base> GameplayUIElements { get; private set; } = new();

    private void OnEnable()
    {
        RefBook.Add(this);
    }

    private void OnDisable()
    {
        RefBook.Remove(this);

    }
    
    public void InitializeGameplayUIElements()
    {
        foreach (var uiElement in GameplayUIElements)
        {
            if (uiElement == null)
                continue;

            uiElement.Initialize();
        }
    }
}
