using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement_GameReload : UIElement_Base
{
    public void ReloadGame()
    {
        if (!RefBook.TryGet(out GameLoader gameLoader))
            return;

        if (!RefBook.TryGet(out UIManager_Game uIManagerGame))
            return;
        
        gameLoader.DeInitializeGameLoop();
        gameLoader.InitializeGameLoop();
        
        uIManagerGame.InitializeGameplayUIElements();
    }
}
