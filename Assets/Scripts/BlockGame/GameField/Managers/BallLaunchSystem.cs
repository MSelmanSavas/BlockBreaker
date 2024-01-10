using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLaunchSystem : GameSystem_Base
{
    public Ball_Base Ball { get; private set; }
    float _ballLaunchAngleLimit = 0f;
    float _ballSpeedLimit = 0f;
    bool _isInputActive = false;
    GameStateManager _gameStateManager;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        if (!gameSystems.TryGetGameSystemByType(out GameFieldPaddleAndBallLoader paddleAndBallLoader))
            return false;

        if (gameSystems.TryGetGameSystemByType(out _gameStateManager))
        {
            _gameStateManager.OnGameStateChange += OnGameStateChange;

            OnGameStateChange(_gameStateManager.CurrentGameState, _gameStateManager.CurrentGameState);
        }

        if (!RefBook.TryGet(out GameConfig gameConfig))
            return false;

        if (gameConfig.BallStorage == null)
            return false;

        _ballLaunchAngleLimit = gameConfig.BallStorage.BallLaunchAngleLimit;
        _ballSpeedLimit = gameConfig.BallStorage.BallSpeedLimit;
        Ball = paddleAndBallLoader.CreatedBall;

        return true;
    }

    public override bool TryUnInitialize(GameSystems gameSystems)
    {
        if (gameSystems.TryGetGameSystemByType(out _gameStateManager))
        {
            _gameStateManager.OnGameStateChange -= OnGameStateChange;
        }

        return base.TryUnInitialize(gameSystems);
    }

    void OnGameStateChange(GameState previousGameState, GameState currentGameState)
    {
        _isInputActive = currentGameState == GameState.CanStart;
    }

    public override void Update(RuntimeGameSystemContext gameSystemContext)
    {
        if (!_isInputActive)
            return;

        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        if (!Ball.TryGetOrAddGetData(out EntityData_Rigidbody2D rigidbody2DData))
            return;

        float randomAngle = Random.Range(-_ballLaunchAngleLimit, _ballLaunchAngleLimit);

        Vector2 launchVector = Vector2.up;

        launchVector = Quaternion.AngleAxis(randomAngle, Vector3.forward) * launchVector;

        rigidbody2DData.Rigidbody2D.AddForce(launchVector * _ballSpeedLimit, ForceMode2D.Impulse);

        _gameStateManager.TrySetGameState(GameState.Playing);
        GameSystems.TryRemoveGameSystem(this);
    }
}
