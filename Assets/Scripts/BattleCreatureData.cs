using System;
using UnityEngine;

public class BattleCreatureData : MonoBehaviour
{
    public static Creature Data;

    public Creature GetData()
    {
        print($"data is {Data.Id}");
        return Data;
    }

    public void SetData(Creature creature)
    {
        Data = creature;
        print($"data to {Data.Id}");
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}