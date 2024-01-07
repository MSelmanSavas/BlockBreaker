using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData_PositionChange : GameEntityData_Base
{
    public Vector3 PreviousPosition;
    public Vector3 CurrentPosition;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        if (gameEntity is not MonoBehaviour monoBehaviour)
            return false;

        PreviousPosition = monoBehaviour.gameObject.transform.position;
        CurrentPosition = monoBehaviour.gameObject.transform.position;

        return true;
    }
}
