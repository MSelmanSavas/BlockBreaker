using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityData_Rigidbody2D : GameEntityData_Base
{
    [field: SerializeField]
    public Rigidbody2D Rigidbody2D { get; private set; }
    public void SetRigidbody2D(Rigidbody2D rigidbody2D) => Rigidbody2D = rigidbody2D;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        if (Rigidbody2D != null)
            return true;

        if (gameEntity is not MonoBehaviour monoBehaviour)
            return false;

        Rigidbody2D = monoBehaviour.GetComponentInChildren<Rigidbody2D>();

        if (Rigidbody2D == null)
            return false;

        return true;
    }
}
