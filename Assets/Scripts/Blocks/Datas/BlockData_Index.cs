using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData_Index
{
    HashSet<Vector2Int> _indices = new();
    
    public ICollection<Vector2Int> GetIndices() => _indices;

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
