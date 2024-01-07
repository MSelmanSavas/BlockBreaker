using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultBallStorage", menuName = "BlockBreaker/Configs/Create Ball Storage Config")]
public class ScriptableBallStorage : ScriptableObject
{
    [field: SerializeField]
    public Vector3 BallOriginPositionOffset;

    [field: SerializeField]
    public GameObject BallPrefab { get; private set; }
}

