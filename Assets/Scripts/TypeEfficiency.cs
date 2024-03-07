using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Efficiency
{
    SuperEffective,
    Normal,
    NotVeryEffective,
    NoEffect
}

public class TypeEfficiency
{
    public float GetSTABMultiplier(Type attackType, List<Type> attackerTypes)
    // Returns a bonus multiplier if the attack has the same type as one of the attacker's
    {
        return attackerTypes.Contains(attackType) ? 1.5f : 1f;
    }

    public float GetEfficiencyMultiplier(Efficiency efficiency)
    // Returns the multiplier associated with an efficiency
    {
        switch (efficiency)
        {
            case Efficiency.SuperEffective: return 2f;
            case Efficiency.Normal: return 1f;
            case Efficiency.NotVeryEffective: return 0.5f;
            case Efficiency.NoEffect: return 0f;
            default:
            {
                Debug.LogWarning("WARNING: efficiency is unknown");
                return 1f;
            }
        }
    }

    private Efficiency GetAttackEfficiency(Type attackType, Type defenderType)
        // Returns the efficiency of an attack according to ONE of the defender's types
        // Multiply the efficiencies obtained from every defender's type
    {
        var typeChart = new TypeChart();
        var weakAgainst = typeChart.WeakAgainst[defenderType];
        var strongAgainst = typeChart.StrongAgainst[defenderType];
        var immuneAgainst = typeChart.ImmuneAgainst[defenderType];
        
        if (weakAgainst.Contains(attackType)) return Efficiency.SuperEffective;
        if (strongAgainst.Contains(attackType)) return Efficiency.NotVeryEffective;
        if (immuneAgainst.Contains(attackType)) return Efficiency.NoEffect;
        return Efficiency.Normal;
    }
    
    public float GetTypeEffectiveness(Type attackType, List<Type> defenderTypes)
        // Returns the type efficiency (multiplier) of an attack against a creature
    {
        return defenderTypes.Aggregate(1f, (mult, type)
            => mult * GetEfficiencyMultiplier(GetAttackEfficiency(attackType, type)));
    }
}