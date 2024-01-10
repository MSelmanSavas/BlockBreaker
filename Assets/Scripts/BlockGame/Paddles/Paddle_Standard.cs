using UnityEngine;

[System.Serializable]
public class Paddle_Standard : Paddle_Base
{
    ScriptablePaddleStorage _paddleStorage;
    float _contactPointStrengthToAffectBallTrajectory = 10f;

    public override bool OnSpawned()
    {
        if (!base.OnSpawned())
            return false;

        if (!RefBook.TryGet(out GameConfig gameConfig))
            return false;

        _paddleStorage = gameConfig.PaddleStorage;

        return true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent(out Ball_Base ball))
            return;

        if (!ball.TryGetData(out EntityData_Rigidbody2D ballRigidbody))
            return;

        if (!TryGetData(out EntityData_PositionChange positionChange))
            return;

        ContactPoint2D contactPoint = other.GetContact(0);

        Vector2 contactVector = contactPoint.point - (Vector2)transform.position;

        if (_paddleStorage != null)
            _contactPointStrengthToAffectBallTrajectory = _paddleStorage.ContactPointStrengthToAffectBallTrajectory;

        float magnitude = ballRigidbody.Rigidbody2D.velocity.magnitude;

        ballRigidbody.Rigidbody2D.velocity = Vector2.up + contactVector * _contactPointStrengthToAffectBallTrajectory;
        ballRigidbody.Rigidbody2D.velocity = ballRigidbody.Rigidbody2D.velocity.normalized * magnitude;
    }
}
