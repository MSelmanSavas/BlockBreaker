using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultBlocksStorage", menuName = "BlockBreaker/Configs/Create Blocks Storage Config")]
public class ScriptableBlocksStorage : ScriptableObject
{
    public TypeToBlockStorageDataDictionary BlockStorageDatas = new();
}
