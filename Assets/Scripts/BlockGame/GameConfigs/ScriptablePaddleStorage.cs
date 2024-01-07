using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultPaddleStorage", menuName = "BlockBreaker/Configs/Create Paddle Storage Config")]
public class ScriptablePaddleStorage : ScriptableObject
{
    [field: SerializeField]
    public Vector3 PaddleOriginPosition { get; private set; }
    
    [field: SerializeField]
    public float PaddleMovementSpeed { get; private set; }

    [field: SerializeField]
    public GameObject PaddlePrefab { get; private set; }
}

