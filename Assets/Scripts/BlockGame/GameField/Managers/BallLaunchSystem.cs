using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLaunchSystem : GameSystem_Base
{
    public Ball_Base Ball { get; private set; }
    float _ballLaunchAngleLimit = 0f;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        if (!gameSystems.TryGetGameSystemByType(out GameFieldPaddleAndBallLoader paddleAndBallLoader))
            return false;

        if (!RefBook.TryGet(out GameConfig gameConfig))
            return false;

        if (gameConfig.BallStorage == null)
            return false;

        _ballLaunchAngleLimit = gameConfig.BallStorage.BallLaunchAngleLimit;
        Ball = paddleAndBallLoader.CreatedBall;

        return true;
    }

    public override void Update(RuntimeGameSystemContext gameSystemContext)
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        if (!Ball.TryGetOrAddGetData(out EntityData_Rigidbody2D rigidbody2DData))
            return;

        float randomAngle = Random.Range(-_ballLaunchAngleLimit, _ballLaunchAngleLimit);

        Vector2 launchVector = Vector2.up;

        launchVector = Quaternion.AngleAxis(randomAngle, Vector3.forward) * launchVector;

        rigidbody2DData.Rigidbody2D.AddForce(launchVector * 15f, ForceMode2D.Impulse);

        GameSystems.TryRemoveGameSystem(this);
    }
}
