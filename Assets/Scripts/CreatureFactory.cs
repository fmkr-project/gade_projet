using System;
using System.Collections.Generic;
using Creatures;
using UnityEngine;
using Random = UnityEngine.Random;

abstract class CreatureFactory
{
    public abstract Creature GenerateCreature(int id);
}

class ConcreteCreatureFactory : CreatureFactory
// Class that generates new creatures following base data
{
    private BaseCreatureData _data;
    private bool _debug = true;
    
    public override Creature GenerateCreature(int id)
    {
        // Get creature base data
        // TODO make this more efficient
        switch (id)
        {
            case 1: _data = new C001();
                break;
            default: _data = new C000();
                break;
        }
        
        // Get individual stats
        var final = new Creature
        {
            Id = _data.Id,
            Nickname = _data.Nickname,
            Level = 10,
            Types = _data.Types,
            Attacks = new List<Attack> {new Attacks.Standard()}
        };

        // IV calculation. cf Bulbapedia
        var hpIV = Random.Range(0, 32);
        final.MaxHp = GetHpWithIv(_data.Hp, final.Level, hpIV);
        final.CurrentHp = final.MaxHp;
        var atkIV = Random.Range(0, 32);
        final.Attack = GetStatWithIv(_data.Attack, final.Level, atkIV);
        var defIV = Random.Range(0, 32);
        final.Defense = GetStatWithIv(_data.Defense, final.Level, defIV);
        var spdIV = Random.Range(0, 32);
        final.Speed = GetStatWithIv(_data.Speed, final.Level, spdIV);

        if (_debug)
        {
            UnityEngine.Debug.Log($"Created creature: [{final.Id}] {final.Nickname}, lv{final.Level}, " +
                                  $"of distribution: {final.MaxHp} {final.Attack} {final.Defense} {final.Speed}");
        }

        return final;
        
        int GetHpWithIv(int norm, int level, int iv)
        {
            return (int) Math.Floor((double) (2 * norm + iv) * level / 100) + level + 10;
        }

        int GetStatWithIv(int norm, int level, int iv)
        {
            return (int) Math.Floor((double) (2 * norm + iv) * level / 100) + 5;
        }
    }
}