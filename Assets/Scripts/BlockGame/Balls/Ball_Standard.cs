using UnityEngine;

[System.Serializable]
public class Ball_Standard : Ball_Base
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent(out Block_Base block))
            return;

        if (!block.TryGetData(out BlockData_Health health))
            return;

        if (!TryGetData(out BallData_DamageOnCollision damageOnCollision))
            return;

        health.ChangeHealth(-damageOnCollision.DamageAmount);
    }
}
