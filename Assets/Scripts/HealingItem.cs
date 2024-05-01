using System;

public record HealingItem : Item
{
    public int HealedHp;

    public override bool Use()
    {
        return false;
    }
}