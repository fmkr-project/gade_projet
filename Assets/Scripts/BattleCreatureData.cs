using UnityEngine;

public class BattleCreatureData : MonoBehaviour
{
    public static Creature Data;

    public Creature GetData()
    {
        return Data;
    }

    public void SetData(Creature creature)
    {
        Data = creature;
    }
}