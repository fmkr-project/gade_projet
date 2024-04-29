using System.Collections.Specialized;
using UnityEngine;

public class Squad 
{
    
    public OrderedDictionary Monsters = new OrderedDictionary();
    
    public void StoreMonster(Creature creature)
    {
        if (Monsters.Contains(creature))
        {
            Monsters[creature] = (int)Monsters[creature] + 1;
        }
        else
        {
            Monsters.Add(creature, 1);
        }
        
    }

    public void KillMonster(Creature creature)
    {
        if (Monsters.Contains(creature))
        {
            int count = (int)Monsters[creature];
            if (count > 1)
            {
                Monsters[creature] = count - 1;
            }
            else
            {
                Monsters.Remove(creature); // Suppression de l'entrée du dictionnaire
            }
        }
        else
        {
            Debug.Log("La créature n'existe pas dans le dictionnaire.");
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