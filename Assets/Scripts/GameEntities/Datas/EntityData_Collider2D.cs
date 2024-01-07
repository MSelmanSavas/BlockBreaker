using UnityEngine;

[System.Serializable]
public class EntityData_Collider2D : GameEntityData_Base
{
    [field: SerializeField]
    public Collider2D Collider2D { get; private set; }
    public void SetCollider(Collider2D collider2D) => Collider2D = collider2D;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        if (Collider2D != null)
            return true;

        if (gameEntity is not MonoBehaviour monoBehaviour)
            return false;

        Collider2D = monoBehaviour.GetComponentInChildren<Collider2D>();

        if (Collider2D == null)
            return false;

        return true;
    }
}
