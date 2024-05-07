using System;
using System.Collections.Generic;
using System.Linq;

public class CreatureSpawnTable
{
    public virtual Dictionary<float, int> Data => new();

    public int GetRandomCreature()
        // Selects a random creature to spawn
    {
        var rand = (float) new Random().NextDouble() *
                   Data.Aggregate(0f, (acc, key) => acc + key.Key);
        var spawned = 0;
        var sum = 0f;

        foreach (var couple in Data)
        {
            sum += couple.Key;
            spawned = couple.Value;
            if (rand < sum) break;
        }
        
        return spawned;
    }
}