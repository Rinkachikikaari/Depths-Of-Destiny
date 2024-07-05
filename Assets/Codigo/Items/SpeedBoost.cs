using UnityEngine;

public class SpeedBoost : StatModifier
{
    public int speedIncrease = 2;

    public override void Apply(CharacterStats stats)
    {
        stats.moveSpeed += speedIncrease;
    }
}
