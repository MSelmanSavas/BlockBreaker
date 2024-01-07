using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldBoundryLoader : GameSystem_Base
{
      public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

       
        return true;
    }
}
