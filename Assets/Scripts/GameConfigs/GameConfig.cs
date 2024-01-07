using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultGameConfig", menuName = "BlockBreaker/Configs/Create Game Config")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField]
    public Vector2 GameFieldOffset { get; private set; }

    [field: SerializeField]
    public Vector2 GameFieldCenter { get; private set; }

    [field: SerializeField]
    public ScriptableBlocksStorage BlocksStorage { get; private set; }
}
