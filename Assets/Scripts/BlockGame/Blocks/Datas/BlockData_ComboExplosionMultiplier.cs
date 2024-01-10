using UnityEngine;

public class BlockData_ComboExplosionMultiplier : GameEntityData_Base
{
    [field: SerializeField]
    public float ComboMultiplier { get; private set; } = 1f;
    public float DefaultComboMultiplier { get; private set; } = 1f;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        if (!base.TryInitialize(gameEntity))
            return false;

        ComboMultiplier = DefaultComboMultiplier;
        return true;
    }

    public void SetComboMultiplier(float multiplier) => ComboMultiplier = multiplier;
    public void ResetComboMultiplier() => ComboMultiplier = DefaultComboMultiplier;
}
