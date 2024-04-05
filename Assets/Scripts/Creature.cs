using System;
using System.Collections.Generic;

[Serializable]
public class Creature
{
    public int Id;
    public string Nickname;
    
    // Stats
    public int Level; // pour l'instant pas de système d'xp
    public int MaxHp;
    public int CurrentHp;
    public int Attack;
    public int Defense;
    public int Speed;
    public List<Type> Types;
    public List<Attack> Attacks;
    
    // todo IV / EV ?

    private void ModifyHp(int hp)
    {
        CurrentHp = Math.Min(CurrentHp + hp, MaxHp);
    }

    public bool IsDead()
    {
        return CurrentHp <= 0;
    }
    
    // Attacks related
    public void ReceiveAttack(Attack attack, Creature attacker)
    // Calculate damage done by an attack
    // Cf Bulbapedia article
    {
        var typeEfficiency = new TypeEfficiency();
        
        var baseDamage = (2 * attacker.Level / 5) + 2 * attack.Power * attacker.Attack / Defense / 50 + 2;
        var effectiveness = typeEfficiency.GetTypeEffectiveness(attack.Type, Types);
        var stab = typeEfficiency.GetSTABMultiplier(attack.Type, attacker.Types);

        var lostHp = (int) (baseDamage * effectiveness * stab);

        ModifyHp(-lostHp);
        if (this.IsDead())
        {
            // todo
        }
    }

    public void SendAttack(Attack attack, Creature enemy)
        // Compute if an attack hits an enemy
        // Cf Bulbapedia
    {
        var finalAccuracy = attack.Accuracy; // TODO modifiers

        var value = UnityEngine.Random.Range(1, 100);
        if (value <= finalAccuracy) // hit
        {
            enemy.ReceiveAttack(attack, this);
        }
        // TODO else miss
    }

    public void Heal(HealingItem potion)
    {
        ModifyHp(potion.HealedHp);
    }

    public float GetModifiedCatchRate(CaptureOrb ball)
    // Cf Bulbapedia
    {
        var baseValue = 1 - 2 * CurrentHp / 3 / MaxHp;
        var creatureCatchRate = 1f; // TODO plusieurs valeurs
        var ballBonus = ball.CaptureMultiplier;

        return baseValue * creatureCatchRate * ballBonus;
    }

}