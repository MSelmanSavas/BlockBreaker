using UnityEngine;

[System.Serializable]
public class Paddle_Standard : Paddle_Base
{
    ScriptablePaddleStorage _paddleStorage;
    float _positionChangeAffectStrengthToBall = 50f;

    private void Start()
    {
        if (RefBook.TryGet(out GameConfig gameConfig))
            _paddleStorage = gameConfig.PaddleStorage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent(out Ball_Base ball))
            return;

        if (!ball.TryGetData(out EntityData_Rigidbody2D rigidbody2DData))
            return;

        if (!TryGetData(out EntityData_PositionChange positionChange))
            return;

        float magnitude = rigidbody2DData.Rigidbody2D.velocity.magnitude;
        Vector2 positionChangeVector = positionChange.CurrentPosition - positionChange.PreviousPosition;

        if (_paddleStorage != null)
            _positionChangeAffectStrengthToBall = _paddleStorage.PositionChangeAffectStrengthToBall;

        rigidbody2DData.Rigidbody2D.velocity += positionChangeVector * _positionChangeAffectStrengthToBall;
        rigidbody2DData.Rigidbody2D.velocity = rigidbody2DData.Rigidbody2D.velocity.normalized * magnitude;
    }
}
