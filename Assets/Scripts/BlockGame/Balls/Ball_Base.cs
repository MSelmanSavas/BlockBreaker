using UnityEngine;

[System.Serializable]
public abstract class Ball_Base : GameEntity_Base
{
    public override bool OnLoad()
    {
        if (!base.OnLoad())
            return false;

        if (TryGetData(out EntityData_Rigidbody2D rigidbody2DData))
        {
            rigidbody2DData.Rigidbody2D.velocity = new Vector3(0f, 0f, 0f);
            rigidbody2DData.Rigidbody2D.angularVelocity = 0f;
        }

        return true;
    }
}
