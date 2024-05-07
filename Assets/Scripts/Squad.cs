using System;
using System.Collections.Generic;
using UnityEngine;

public class Squad 
{
    public List<Creature> Monsters = new ();

    public Squad()
    {
        // TODO nicer introduction screen so that the player can choose a starting mon
        Monsters.Add(new ConcreteCreatureFactory().GenerateCreature(13, 50));
        Monsters.Add(new ConcreteCreatureFactory().GenerateCreature(19, 50));
    }
    
    public (int, Creature) GetBattleReadyCreature()
    // Return the first creature fit for battle along with its index in the array
    {
        var index = 0;
        foreach (var mon in Monsters)
        {
            if (mon.CurrentHp > 0)
                return (index, mon);
            index++;
        }

        return (-1, null);
    }

    public void UpdateMonStatus(int index, Creature creature)
        // Put in the array an updated version of the creature
    {
        if (index < 0 || index >= Monsters.Count)
            throw new ArgumentException("Can't update the team; index out of range");
        Monsters[index] = creature;
    }
    
    public void StoreMonster(Creature creature)
    {
        Monsters.Add(creature);
    }

    public void KillMonster(Creature creature)
    {
        if (Monsters.Contains(creature))
        {
            Monsters.Remove(creature);
        }
        else
        {
            Debug.Log("La créature n'existe pas dans la liste.");
        }
    }
    /*public void PrintValues()
    {
        foreach (var value in Monsters.Values)
        {
            Debug.Log(value);
        }
    }*/
}