using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultGamefieldBoundryStorage", menuName = "BlockBreaker/Configs/Create Gamefield Boundry Storage Config")]
public class ScriptableGamefieldBoundryStorage : ScriptableObject
{
    public GameObject RightBoundryPrefab;
    public GameObject LeftBoundryPrefab;
    public GameObject TopBoundryPrefab;
    public GameObject DownBoundryPrefab;
}
