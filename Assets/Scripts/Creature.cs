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
    
    // Attacks related
    public float ReceiveAttack(Attack attack, Creature attacker)
    // Calculate damage done by an attack / status alts
    // Cf Bulbapedia article. Using Gen III formula
    {
        if (attack.Power == 0)
        {
            // Status attack logic

            return 1;
        }
        var typeEfficiency = new TypeEfficiency();
        
        var baseDamage = 2 + (2 + 2 * attacker.Level / 5) * attack.Power * (attacker.Attack / Defense) / 50;
        var effectiveness = typeEfficiency.GetTypeEffectiveness(attack.Type, Types);
        var stab = typeEfficiency.GetSTABMultiplier(attack.Type, attacker.Types);

        var randomFactor = Random.Range(85, 100) / 100f;
        
        // TODO crits

        var lostHp = (int) (baseDamage * effectiveness * stab * randomFactor);

        ModifyHp(-lostHp);
        
        UnityEngine.Debug.Log($"{Nickname} now has {CurrentHp} / {MaxHp} HP");

        return effectiveness;
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
    }

}