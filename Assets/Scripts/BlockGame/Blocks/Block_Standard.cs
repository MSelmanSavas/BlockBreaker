public class Block_Standard : Block_Base
{
    private void OnEnable()
    {
        if (TryGetData(out BlockData_Health health))
        {
            health.OnHealthChange += OnHealthChange;
            health.OnHealthDeplete += OnHealthDeplete;
        }
    }

    private void OnDisable()
    {
        if (TryGetData(out BlockData_Health health))
        {
            health.OnHealthChange -= OnHealthChange;
            health.OnHealthDeplete -= OnHealthDeplete;
        }
    }

    void OnHealthDeplete()
    {
        if (!TryGetData(out BlockData_GameFieldManager gameFieldManager))
            return;

        if (!TryGetData(out BlockData_Index indexData))
            return;

        gameFieldManager.GetAttachedGameFieldManager().TryRemoveGameEntity(indexData.GetIndices()[0]);
        Destroy(gameObject);
    }

    void OnHealthChange(float previousHealth, float currentHealth)
    {
        float scoreChange = previousHealth - currentHealth;
        TryChangeScore(scoreChange);
    }

    void TryChangeScore(float scoreChange)
    {
        if (!TryGetData(out BlockData_GameSystems gameSystems))
            return;

        if (gameSystems.GetAttachedGameGameSystems() == null)
            return;
        
        if(!gameSystems.GetAttachedGameGameSystems().TryGetGameSystemByType(out GameScoreSystem gameScoreSystem))
            return;

        gameScoreSystem.ChangeScore(scoreChange);
    }
}
