using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockData_Index : GameEntityData_Base
{
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    List<Vector2Int> _indices = new();

    public List<Vector2Int> GetIndices() => _indices;

    public void AddIndices(ICollection<Vector2Int> indices)
    {
        foreach (var index in indices)
            _indices.Add(index);
    }

    public void RemoveIndices(ICollection<Vector2Int> indices)
    {
        foreach (var index in indices)
            _indices.Remove(index);
    }

    public void SetIndices(ICollection<Vector2Int> indices)
    {
        _indices.Clear();
        AddIndices(indices);
    }
}
