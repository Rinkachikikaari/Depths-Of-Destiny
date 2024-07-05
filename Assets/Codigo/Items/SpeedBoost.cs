using UnityEngine;

public class SpeedBoost : StatModifier
{
    public float speedIncrease = 2f;

    public override void Apply(CharacterStats stats)
    {
        stats.moveSpeed += speedIncrease;
    }
}
