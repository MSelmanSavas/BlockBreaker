using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockData_ChangeSpriteOnHealthChange : GameEntityData_Base
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites = new();
    [SerializeField] List<float> _healthThresholdsToChangeSprite = new();
    [SerializeField] float _maxHealth;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        if (!gameEntity.TryGetData(out BlockData_Health health))
            return false;

        _maxHealth = health.MaxHealth;
        CalculateHealthThresholdsToChangeSprite(_sprites.Count, _maxHealth);
        health.OnHealthChange += OnHealthChange;

        return true;
    }

    void CalculateHealthThresholdsToChangeSprite(int spriteCount, float maxHealth)
    {
        float thresholdOffset = maxHealth / (spriteCount + 1);
        float currentHealthThreshold = maxHealth - thresholdOffset;

        _healthThresholdsToChangeSprite.Clear();

        for (int i = 0; i < spriteCount; i++)
        {
            _healthThresholdsToChangeSprite.Add(currentHealthThreshold);
            currentHealthThreshold -= thresholdOffset;
        }
    }

    void OnHealthChange(float healthAmount)
    {
        int spriteIndex = 0;

        for (int i = 0; i < _healthThresholdsToChangeSprite.Count; i++)
        {
            float checkHealth = _healthThresholdsToChangeSprite[i];

            if (healthAmount > checkHealth)
                continue;

            spriteIndex = i;
        }

        _spriteRenderer.sprite = _sprites[spriteIndex];
    }
}
