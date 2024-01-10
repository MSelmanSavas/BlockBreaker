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
        CreateBall(_paddleStorage, _ballStorage);

        return true;
    }

    void CreatePaddle(ScriptablePaddleStorage paddleStorage)
    {
        GameObject paddleObject = GameObject.Instantiate(paddleStorage.PaddlePrefab, Vector3.zero, Quaternion.identity, null);
        CreatedPaddle = paddleObject.GetComponent<Paddle_Base>();
        SetPaddlePositionToOrigin(CreatedPaddle, paddleStorage);
        
        CreatedPaddle.OnSpawned();
        CreatedPaddle.OnLoad();
        CreatedPaddle.OnAfterLoad();
    }

    void SetPaddlePositionToOrigin(Paddle_Base paddleBase, ScriptablePaddleStorage paddleStorage)
    {
        if (paddleBase == null)
            return;

        PaddleOriginPosition = paddleStorage.PaddleOriginPosition;
        paddleBase.transform.position = PaddleOriginPosition;
    }

    void CreateBall(ScriptablePaddleStorage paddleStorage, ScriptableBallStorage ballStorage)
    {
        GameObject ballObject = GameObject.Instantiate(ballStorage.BallPrefab, Vector3.zero, Quaternion.identity, null);
        CreatedBall = ballObject.GetComponent<Ball_Base>();
        SetBallPositionToOrigin(CreatedBall, paddleStorage, ballStorage);

        CreatedBall.OnSpawned();
        CreatedBall.OnLoad();
        CreatedBall.OnAfterLoad();
    }

    void SetBallPositionToOrigin(Ball_Base ballBase, ScriptablePaddleStorage paddleStorage, ScriptableBallStorage ballStorage)
    {
        if (ballBase == null)
            return;

        BallOriginPositionOffset = ballStorage.BallOriginPositionOffset;
        Vector3 paddleOriginPosition = paddleStorage.PaddleOriginPosition;
        ballBase.transform.position = paddleOriginPosition + BallOriginPositionOffset;
    }

    public void ResetPaddleAndBall()
    {
        if (CreatedPaddle == null)
            CreatePaddle(_paddleStorage);

        if (CreatedBall == null)
            CreateBall(_paddleStorage, _ballStorage);

        SetPaddlePositionToOrigin(CreatedPaddle, _paddleStorage);
        SetBallPositionToOrigin(CreatedBall, _paddleStorage, _ballStorage);

        CreatedBall.OnLoad();
        CreatedPaddle.OnLoad();
    }
}
