public class UIElement_GameStart : UIElement_Base
{
    public void StartGame()
    {
        if (!RefBook.TryGet(out GameLoader gameLoader))
            return;

        if (!RefBook.TryGet(out UIManager_Game uIManagerGame))
            return;

        gameLoader.InitializeGameLoop();
        uIManagerGame.InitializeGameplayUIElements();
        gameObject.SetActive(false);
    }
}
