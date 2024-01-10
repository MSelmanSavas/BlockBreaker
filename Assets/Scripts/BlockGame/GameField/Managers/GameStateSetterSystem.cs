using UnityEngine;

public class GameStateSetterSystem : GameSystem_Base
{
    GameStateManager _gameStateManager;
    GameFieldPaddleAndBallLoader _paddleAndBallLoader;
    GameplayVariablesSystem _gameplayVariablesSystem;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        if (!GameSystems.TryGetGameSystemByType(out _gameStateManager))
            return false;

        if (!GameSystems.TryGetGameSystemByType(out _paddleAndBallLoader))
            return false;

        if (!GameSystems.TryGetGameSystemByType(out _gameplayVariablesSystem))
            return false;

        _gameplayVariablesSystem.OnCurrentLifeReachesZero += OnCurrentLifeReachesZeroChange;

        return true;
    }

    public override bool TryDeInitialize(GameSystems gameSystems)
    {
        if (GameSystems.TryGetGameSystemByType(out _gameplayVariablesSystem))
        {
            _gameplayVariablesSystem.OnCurrentLifeReachesZero -= OnCurrentLifeReachesZeroChange;
        }

        return base.TryDeInitialize(gameSystems);
    }

    public override void Update(RuntimeGameSystemContext gameSystemContext)
    {
        if (_paddleAndBallLoader.CreatedBall == null)
            return;

        if (_paddleAndBallLoader.CreatedPaddle == null)
            return;

        float ballPositionY = _paddleAndBallLoader.CreatedBall.transform.position.y;
        float paddlePositionY = _paddleAndBallLoader.CreatedPaddle.transform.position.y;

        if (ballPositionY >= paddlePositionY)
            return;

        _paddleAndBallLoader.ResetPaddleAndBall();
        _gameStateManager.TrySetGameState(GameState.CanStart);
        _gameplayVariablesSystem.ChangeCurrentLife(-1);

        if (_gameplayVariablesSystem.CurrentLife > 0)
            GameSystems.TryAddGameSystemByType<BallLaunchSystem>();
    }

    void OnCurrentLifeReachesZeroChange()
    {
        _gameStateManager.TrySetGameState(GameState.Failed);
    }
}
