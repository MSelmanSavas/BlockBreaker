public class Block_Standard : Block_Base
{
    private void Start()
    {
        if (TryGetData(out BlockData_Health health))
            health.OnHealthDeplete += OnHealthDeplete;
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
}
