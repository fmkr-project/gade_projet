using System;
using System.Collections.Generic;
using System.Linq;
using Attacks;
using Creatures;
using UnityEngine;
using Random = UnityEngine.Random;

abstract class CreatureFactory
{
    public abstract Creature GenerateCreature(int id, int level);
}

class ConcreteCreatureFactory : CreatureFactory
// Class that generates new creatures following base data
{
    private BaseCreatureData _data;
    private bool _debug = true;
    
    public override Creature GenerateCreature(int id, int level)
    {
        // Get creature base data
        // TODO make this more efficient
        _data = id switch
        {
            1 => new C001(),
            
            6 => new C006(),
            7 => new C007(),
            11 => new C011(),
            12 => new C012(),
            13 => new C013(),
            
            _ => new C000()
        };
        
        // Get individual stats
        var final = new Creature
        {
            Id = _data.Id,
            Nickname = _data.Nickname,
            Level = level,
            Types = _data.Types,
            Attacks = new List<Attack>()
        };
        
        // Select attacks
        var usableAttacks = new List<Attack>();
        _data.LearnableAttacks.ToList().ForEach
        (
            couple =>
            {
                if (couple.Key <= final.Level) usableAttacks.Add(couple.Value);
            }
        );
        switch (usableAttacks.Count)
        {
            // safeguard in case there is no suitable attack
            case 0:
                usableAttacks.Add(new Struggle());
                final.Attacks = usableAttacks;
                Debug.Log($"WARNING: Creature id {final.Id} has no learnable moves.");
                break;
            case < 4:
            {
                final.Attacks = usableAttacks;
                break;
            }
            // Pick attacks at random
            default:
            {
                for (var i = 0; i < 4; i++)
                {
                    var chosen = usableAttacks[Random.Range(0, usableAttacks.Count)];
                    final.Attacks.Add(chosen);
                    usableAttacks.Remove(chosen);
                }

                break;
            }
        }

        if (_data.CatchRate == 0)
            Debug.LogWarning("Mons should not have a 0 catch rate (makes them uncatchable)");
        final.CatchRate = _data.CatchRate;
        
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
                                  $"of distribution: {final.MaxHp} {final.Attack} {final.Defense} {final.Speed} " +
                                  $"and with {final.Attacks.Count} attacks");
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