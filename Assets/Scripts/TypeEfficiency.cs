using System.Collections;
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
    public float GetSTABMultiplier(Type attackType, Type attackerType) // todo list<Type>
    // Returns a bonus multiplier if the attack has the same type as one of the attacker's
    {
        return attackType == attackerType ? 1.5f : 1f;
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

    public Efficiency GetAttackEfficiency(Type attackType, Type defenderType)
        // Returns the efficiency of an attack according to ONE of the defender's types
        // Multiply the efficiencies obtained from every defender's type
    {
        // TODO table des types avec List<T>
    }
}