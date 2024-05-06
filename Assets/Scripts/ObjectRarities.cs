using Objects;
using UnityEngine;

public static class ObjectRarities
{
    public static Item GenerateObject()
    {
        var res = Random.Range(0f, 1f);
        Debug.Log(res);
        return res switch
        {
            < 0.35f => new Potion(),
            < 0.7f => new ClassicOrb(),
            < 0.82f => new BetterOrb(),
            < 0.94f => new SuperPotion(),
            < 0.99999f => new BetterBetterOrb(),
            _ => new ExtraOrb()
        };
    }
}