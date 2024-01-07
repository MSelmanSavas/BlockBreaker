using UnityEngine;

public class Block_VerticalExploder : Block_Base
{
    [SerializeField] int _verticalExplodeRadius = 3;
    [SerializeField] float _damageAmount = 500;

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

        ExplodeVertical(gameFieldManager.GetAttachedGameFieldManager(), _verticalExplodeRadius);

        gameFieldManager.GetAttachedGameFieldManager().TryRemoveGameEntity(indexData.GetIndices()[0]);
        Destroy(gameObject);
    }
    void ExplodeVertical(GameFieldManager_Default gameFieldManager, int explodeRadius)
    {
        if (!TryGetData(out BlockData_Index indexData))
            return;

        Vector2Int index = indexData.GetIndices()[0];
        int downIndex = index.y - explodeRadius;
        int upIndex = index.y + explodeRadius;

        int iterationCount = upIndex - downIndex;

        for (int i = 0; i < iterationCount; i++)
        {
            Vector2Int checkIndex = new Vector2Int(index.x, downIndex + i);

            if (checkIndex == index)
                continue;

            if (!gameFieldManager.TryGetEntity(checkIndex, out IGameEntity gameEntity))
                continue;

            if (!gameEntity.TryGetData(out BlockData_Health blockHealth))
                continue;

            blockHealth.ChangeHealth(-_damageAmount);
        }
    }
}
