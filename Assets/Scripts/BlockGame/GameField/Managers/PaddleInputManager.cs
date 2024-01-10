using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulExtensions.Vector3;

public class PaddleInputManager : GameSystem_Base
{
    public Paddle_Base Paddle { get; private set; }
    public Ball_Base Ball { get; private set; }
    public Vector2 LeftRightLimits { get; private set; }
    public Vector2 LeftRightPaddleMovementLimites { get; private set; }
    GameFieldBoundryLoader _boundryLoader;
    float _paddleMovementSpeed;
    bool _isInputActive = false;

    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        if (!gameSystems.TryGetGameSystemByType(out _boundryLoader))
            return false;

        if (gameSystems.TryGetGameSystemByType(out GameStateManager gameStateManager))
        {
            gameStateManager.OnGameStateChange += OnGameStateChange;
        }

        if (!gameSystems.TryGetGameSystemByType(out GameFieldPaddleAndBallLoader paddleAndBallLoader))
            return false;

        if (!RefBook.TryGet(out GameConfig gameConfig))
            return false;

        if (gameConfig.PaddleStorage == null)
            return false;


        Paddle = paddleAndBallLoader.CreatedPaddle;
        Ball = paddleAndBallLoader.CreatedBall;

        _paddleMovementSpeed = gameConfig.PaddleStorage.PaddleMovementSpeed;

        CalculateLeftRightLimits(_boundryLoader);
        CalculatePaddleMovementLimits(Paddle);

        return true;
    }

    public override bool TryDeInitialize(GameSystems gameSystems)
    {
        if (gameSystems.TryGetGameSystemByType(out GameStateManager gameStateManager))
        {
            gameStateManager.OnGameStateChange -= OnGameStateChange;
        }

        return base.TryDeInitialize(gameSystems);
    }

    void CalculateLeftRightLimits(GameFieldBoundryLoader boundryLoader)
    {
        Vector3 closestPointToLeftBoundry = boundryLoader.LeftBorder.GetComponent<Renderer>().bounds.ClosestPoint(Paddle.transform.position);
        Vector3 closestPointToRightBoundry = boundryLoader.RightBorder.GetComponent<Renderer>().bounds.ClosestPoint(Paddle.transform.position);

        LeftRightLimits = new()
        {
            x = closestPointToLeftBoundry.x,
            y = closestPointToRightBoundry.x,
        };
    }

    void CalculatePaddleMovementLimits(Paddle_Base paddle)
    {
        if (!paddle.TryGetOrAddGetData(out EntityData_Collider2D colliderData))
            return;

        Vector2 colliderSize = colliderData.Collider2D.bounds.extents;

        LeftRightPaddleMovementLimites = new()
        {
            x = LeftRightLimits.x + colliderSize.x,
            y = LeftRightLimits.y - colliderSize.x,
        };
    }

    void OnGameStateChange(GameState previousGameState, GameState currentGameState)
    {
        _isInputActive = currentGameState == GameState.Playing;
    }

    public override void Update(RuntimeGameSystemContext gameSystemContext)
    {
        if (!_isInputActive)
            return;

        //Calculating paddle movement limits for each frame
        //If paddle resizes, we need to calculate limits based on the size of the paddle every frame.
        //We can connect it to another system and trigger it with an event, but this is easier for now.
        CalculatePaddleMovementLimits(Paddle);

        Vector3 previousPosition = Paddle.transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Paddle.transform.position += Vector3.left * _paddleMovementSpeed * gameSystemContext.DeltaTime;

            if (Paddle.transform.position.x < LeftRightPaddleMovementLimites.x)
                Paddle.transform.position = Paddle.transform.position.WithX(LeftRightPaddleMovementLimites.x);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Paddle.transform.position += Vector3.right * _paddleMovementSpeed * gameSystemContext.DeltaTime;


            if (Paddle.transform.position.x > LeftRightPaddleMovementLimites.y)
                Paddle.transform.position = Paddle.transform.position.WithX(LeftRightPaddleMovementLimites.y);
        }

        if (Paddle.TryGetData(out EntityData_PositionChange positionChange))
        {
            positionChange.PreviousPosition = previousPosition;
            positionChange.CurrentPosition = Paddle.transform.position;
        }
    }
}
