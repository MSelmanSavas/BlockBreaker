using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameFieldPaddleAndBallLoader : GameSystem_Base
{
    public Paddle_Base CreatedPaddle { get; private set; }
    public Ball_Base CreatedBall { get; private set; }
    public Vector3 PaddleOriginPosition { get; private set; }
    public Vector3 BallOriginPositionOffset { get; private set; }

    ScriptablePaddleStorage _paddleStorage;
    ScriptableBallStorage _ballStorage;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        if (!RefBook.TryGet(out GameConfig gameConfig))
            return false;

        if (gameConfig.PaddleStorage == null)
            return false;

        if (gameConfig.BallStorage == null)
            return false;

        _paddleStorage = gameConfig.PaddleStorage;
        _ballStorage = gameConfig.BallStorage;

        CreatePaddle(_paddleStorage);
        CreateBall(_ballStorage);

        return true;
    }

    void CreatePaddle(ScriptablePaddleStorage paddleStorage)
    {
        PaddleOriginPosition = paddleStorage.PaddleOriginPosition;
        GameObject paddleObject = GameObject.Instantiate(paddleStorage.PaddlePrefab, PaddleOriginPosition, quaternion.identity, null);
        CreatedPaddle = paddleObject.GetComponent<Paddle_Base>();
    }

    void CreateBall(ScriptableBallStorage ballStorage)
    {
        BallOriginPositionOffset = ballStorage.BallOriginPositionOffset;

        GameObject ballObject = GameObject.Instantiate(ballStorage.BallPrefab, PaddleOriginPosition + BallOriginPositionOffset, quaternion.identity, null);
        CreatedBall = ballObject.GetComponent<Ball_Base>();
    }
}
