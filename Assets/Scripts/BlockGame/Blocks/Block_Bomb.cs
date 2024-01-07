using UnityEngine;

public class Block_Bomb : Block_Base
{
    [SerializeField] int _areaExplodeRadius = 3;
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

        ExplodeArea(gameFieldManager.GetAttachedGameFieldManager(), _areaExplodeRadius);

        gameFieldManager.GetAttachedGameFieldManager().TryRemoveGameEntity(indexData.GetIndices()[0]);
        Destroy(gameObject);
    }

    void ExplodeArea(GameFieldManager_Default gameFieldManager, int explodeRadius)
    {
        if (!TryGetData(out BlockData_Index indexData))
            return;

        Vector2Int index = indexData.GetIndices()[0];
        Vector2Int leftDownIndex = index - new Vector2Int(explodeRadius, explodeRadius);
        Vector2Int rightUpIndex = index + new Vector2Int(explodeRadius, explodeRadius);

        int xIterationCount = rightUpIndex.x - leftDownIndex.x;
        int yIterationCount = rightUpIndex.y - leftDownIndex.y;

        for (int y = 0; y < yIterationCount; y++)
            for (int x = 0; x < xIterationCount; x++)
            {
                Vector2Int checkIndex = leftDownIndex + new Vector2Int(x, y);
                if (!gameFieldManager.TryGetEntity(checkIndex, out IGameEntity gameEntity))
                    continue;

                if (!gameEntity.TryGetData(out BlockData_Health blockHealth))
                    continue;

                blockHealth.ChangeHealth(-_damageAmount);
            }
    }
}
