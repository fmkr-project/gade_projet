using System.Collections.Generic;
using UnityEngine;

public class BaseCreatureData // TODO switch to records
{
    public int Id;
    public string Nickname;

    public int Hp;
    public int Attack;
    public int Defense;
    public int Speed;
    public List<Type> Types;
    public Dictionary<int, Attack> LearnableAttacks;

    public int CatchRate;
}