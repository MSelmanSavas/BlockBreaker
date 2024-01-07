using UnityEngine;

public class Block_HorizontalExploder : Block_Base
{
    [SerializeField] int _horizontalExplodeRadius = 3;
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

        ExplodeHorizontal(gameFieldManager.GetAttachedGameFieldManager(), _horizontalExplodeRadius);

        gameFieldManager.GetAttachedGameFieldManager().TryRemoveGameEntity(indexData.GetIndices()[0]);
        Destroy(gameObject);
    }

    void ExplodeHorizontal(GameFieldManager_Default gameFieldManager, int explodeRadius)
    {
        if (!TryGetData(out BlockData_Index indexData))
            return;

        Vector2Int index = indexData.GetIndices()[0];
        int leftIndex = index.x - explodeRadius;
        int rightIndex = index.x + explodeRadius;

        int iterationCount = rightIndex - leftIndex;

        for (int i = 0; i < iterationCount; i++)
        {
            Vector2Int checkIndex = new Vector2Int(leftIndex + i, index.y);

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
