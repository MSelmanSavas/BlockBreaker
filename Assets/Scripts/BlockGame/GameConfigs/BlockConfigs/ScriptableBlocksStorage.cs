using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultBlocksStorage", menuName = "BlockBreaker/Configs/Create Blocks Storage Config")]
public class ScriptableBlocksStorage : ScriptableObject
{
    [field: SerializeField]
    public Vector2Int BlockPixelSize { get; private set; }
    
    [field: SerializeField]
    public Vector3 BlockWorldSize { get; private set; }
    public TypeToBlockStorageDataDictionary BlockStorageDatas = new();

    private void OnValidate()
    {
        foreach (var storageDataKV in BlockStorageDatas)
        {
            if (storageDataKV.Key == null || storageDataKV.Value == null)
                continue;

            Sprite sprite = storageDataKV.Value.Prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            Vector2 spriteSize = sprite.rect.size;

            Vector2 local_sprite_size = spriteSize / sprite.pixelsPerUnit;
            Vector3 world_size = local_sprite_size;
            world_size.x *= storageDataKV.Value.Prefab.transform.lossyScale.x;
            world_size.y *= storageDataKV.Value.Prefab.transform.lossyScale.y;
            world_size.z = storageDataKV.Value.Prefab.transform.lossyScale.z;

            BlockPixelSize = new Vector2Int((int)spriteSize.x, (int)spriteSize.y);
            BlockWorldSize = world_size;
        }
    }
}
