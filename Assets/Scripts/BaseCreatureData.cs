using System.Collections.Generic;
using UnityEngine;

public class BaseCreatureData
{
    public int Id;
    public string Nickname;
    public GameObject BattleSprite;
    // public GameObject MiniatureSprite;

    public int Hp;
    public int Attack;
    public int Defense;
    public int Speed;
    public List<Type> Types;
    // public Dictionary<int, Attack> LearnableAttacks;
    
}