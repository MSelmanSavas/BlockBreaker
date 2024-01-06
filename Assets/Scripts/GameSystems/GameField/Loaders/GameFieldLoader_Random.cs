using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldLoader_Random : GameSystem_Base
{
    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        if (!gameSystems.TryGetGameSystemByType(out GameFieldManager_Default gameFieldManager))
            return false;
        

        return true;
    }
}
