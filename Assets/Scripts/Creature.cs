using System.Collections.Generic;

public class Creature
{
    public string Name;
    
    // Stats
    public int Level; // pour l'instant pas de système d'xp
    public int Hp;
    public int Attack;
    public int Defense;
    public int Speed;
    public List<Type> Types;
    public List<Attack> Attacks;

    // todo IV / EV ?

    private void ModifyHp(int hp)
    {
        Hp += hp;
    }

    public void LoseHp(int hp)
    {
        ModifyHp(-hp);
    }

    public void GainHp(int hp)
    {
        ModifyHp(hp);
    }

    public bool IsDead()
    {
        return Hp <= 0;
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

        LoseHp(lostHp);
        if (IsDead())
        {
            // todo
        }
    }

}