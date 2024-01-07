using UnityEngine;

[System.Serializable]
public class EntityData_Collider2D : GameEntityData_Base
{
    [field: SerializeField]
    public Collider2D Collider { get; private set; }
    public void SetCollider(Collider2D collider2D) => Collider = collider2D;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        if (Collider != null)
            return true;

        if (gameEntity is not MonoBehaviour monoBehaviour)
            return false;

        Collider = monoBehaviour.GetComponentInChildren<Collider2D>();

        if (Collider == null)
            return false;

        return true;
    }
}
