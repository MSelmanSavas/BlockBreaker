using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallData_DamageOnCollision : GameEntityData_Base
{
    public float DamageAmount = 200;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        if (RefBook.TryGet(out GameConfig gameConfig))
            DamageAmount = gameConfig.BallStorage.BallDamageOnCollision;

        return true;
    }
}
