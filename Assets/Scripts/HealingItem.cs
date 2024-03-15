using System;

public class HealingItem : Item
{
    public int HealedHp;

    public override bool Use()
    {
        return false;
    }
}