using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Creature
{
    [NonSerialized] public int Id;
    [NonSerialized] public string Nickname;
    
    // Stats
    [NonSerialized] public int Level; // pour l'instant pas de système d'xp
    [NonSerialized] public int MaxHp;
    [NonSerialized] public int CurrentHp;
    [NonSerialized] public int Attack;
    [NonSerialized] public int Defense;
    [NonSerialized] public int Speed;
    [NonSerialized] public List<Type> Types;
    [NonSerialized] public List<Attack> Attacks;

    [NonSerialized] public int AttackBuff;
    [NonSerialized] public int DefenseBuff;
    [NonSerialized] public int SpeedBuff;
    [NonSerialized] public int AccuracyBuff;
    [NonSerialized] public int CriticalBuff;
    [NonSerialized] public int EvasionBuff;

    [NonSerialized] public int CatchRate;

    private void ModifyHp(int hp)
    {
        CurrentHp = Math.Min(CurrentHp + hp, MaxHp);
    }

    public bool IsDead()
    {
        if (CurrentHp > 0) return false;
        CurrentHp = 0;
        return true;
    }

    public float GetStatModifier(Stats stat)
    {
        // Cf Bulbapedia > Stat modifier
        if (stat == Stats.Critical) // Special formula for crits
        {
            return CriticalBuff switch
            {
                0 => 6.25f / 100,
                1 => 12.5f / 100,
                2 => 25f / 100,
                3 => 1f / 3,
                >= 4 => 1f / 2,
                _ => throw new ArgumentException("should not happn with clamped values")
            };
        }
        var val = stat switch
        {
            Stats.Attack => AttackBuff,
            Stats.Defense => DefenseBuff,
            Stats.Speed => SpeedBuff,
            Stats.Accuracy => AccuracyBuff,
            Stats.Evasion => EvasionBuff,
            _ => throw new ArgumentException("Desired stat does not exist")
        };
        if (stat is Stats.Accuracy or Stats.Evasion) // Gen V+ formula
        {
            return val switch
            {
                <= 0 => 3f / (-val + 3),
                > 0 => (val + 3) / 3f
            };
        }
        return val switch
        {
            <= 0 => 2f / (-val + 2),
            > 0 => (val + 2) / 2f
        };
    }
    
    // Attacks related
    public (float, int) ReceiveAttack(Attack attack, Creature attacker)
    // Calculate damage done by an attack / status alts
    // Cf Bulbapedia article. Using Gen III formula
    {
        if (attack.Target != StatusAttackTarget.Nil)
        {
            // Status attack logic
            var affectedMon = attack.Target == StatusAttackTarget.Enemy ? this : attacker;
            affectedMon.AttackBuff = Math.Clamp(affectedMon.AttackBuff + attack.AttackBuff, -6, 6);
            affectedMon.DefenseBuff = Math.Clamp(affectedMon.DefenseBuff + attack.DefenseBuff, -6, 6);
            affectedMon.SpeedBuff = Math.Clamp(affectedMon.SpeedBuff + attack.SpeedBuff, -6, 6);
            affectedMon.CriticalBuff = Math.Clamp(affectedMon.CriticalBuff + attack.CriticalBuff, 0, 6);
            affectedMon.EvasionBuff = Math.Clamp(affectedMon.EvasionBuff + attack.EvasionBuff, -6, 6);
            affectedMon.AccuracyBuff = Math.Clamp(affectedMon.AccuracyBuff + attack.AccuracyBuff, -6, 6);
            Debug.Log($"{Nickname}'s modifiers: ATK{AttackBuff} " +
                      $"DEF{DefenseBuff} SPD{SpeedBuff} CRT{CriticalBuff} EVA{EvasionBuff} ACC{AccuracyBuff}");
        }

        if (attack.Power == 0) return (1, 1); // Status-only attacks
        
        var typeEfficiency = new TypeEfficiency();

        var effectiveA = attacker.Attack * attacker.GetStatModifier(Stats.Attack);
        var effectiveD = Defense * GetStatModifier(Stats.Defense);
        
        var baseDamage = 2 + (2 + (float) 2 * attacker.Level / 5) * attack.Power * (effectiveA / effectiveD) / 50;
        var effectiveness = typeEfficiency.GetTypeEffectiveness(attack.Type, Types);
        var stab = typeEfficiency.GetSTABMultiplier(attack.Type, attacker.Types);

        var randomFactor = Random.Range(85, 100) / 100f;

        var critFactor = Random.Range(0f, 100f) < attacker.GetStatModifier(Stats.Critical) ? 2 : 1;

        var lostHp = (int) (baseDamage * effectiveness * stab * randomFactor);

        ModifyHp(-lostHp);
        
        UnityEngine.Debug.Log($"{Nickname} now has {CurrentHp} / {MaxHp} HP");

        return (effectiveness, critFactor);
    }

    public bool TestAttackHits(Attack attack, Creature enemy)
        // Compute if an attack hits an enemy
        // Cf Bulbapedia
    {
        var finalAccuracy = attack.Accuracy; // TODO modifiers + enemy

        var value = UnityEngine.Random.Range(1, 100);
        return value <= finalAccuracy;
    }

    public void Heal(HealingItem potion)
    {
        ModifyHp(potion.HealedHp);
    }

    public float GetModifiedCatchRate(CaptureOrb ball)
    // Cf Bulbapedia
    {
        var baseValue = (float) (3.0 * MaxHp - 2 * CurrentHp) / (3 * MaxHp);
        var ballBonus = ball.CaptureMultiplier;
        
        return baseValue * CatchRate * ballBonus;
    }

    public void ResetStatusAlterations()
    {
        AttackBuff = 0;
        DefenseBuff = 0;
        CriticalBuff = 0;
        SpeedBuff = 0;
        AccuracyBuff = 0;
        EvasionBuff = 0;
    }

    public void DropLoot()
    // Return a list of items dropped by the creature.
    // Custom made formula for amount of dropped items
    {
        var nbItems = (int) Math.Ceiling((Attack + Defense + Speed) / 100f);
        for (var i = 0; i < nbItems; i++)
        {
            GameInformation.Bag.StoreItem(ObjectRarities.GenerateObject());
        }
    }
}