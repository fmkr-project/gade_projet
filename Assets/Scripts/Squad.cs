using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad 
{
    public Dictionary<Creature, int> Monsters = new Dictionary<Creature, int>();
    
    public void StoreMonster(Creature creature)
    {
        if (Monsters.ContainsKey(creature))
        {
            Monsters[creature]++;
        }
        else
        {
            Monsters.Add(creature, 1);
        }
    }

   

    public void KillMonster(Creature creature)
    {
        if (Monsters.ContainsKey(creature))
        {
            if (Monsters[creature] > 1)
            {
                Monsters[creature]--;
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

    

    

   
}