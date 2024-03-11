using System;
using System.Collections.Generic;

public class Zone
{
    public string Name;
    public Dictionary<int, CreatureSpawnTable> ZoneSpawnTables;

    public Creature GetSpawnedCreatureById(int id)
    // Get spawned creature from trigger with id
    {
        if (!ZoneSpawnTables.ContainsKey(id)) throw new NullReferenceException($"No trigger with id: {id}");

        var table = ZoneSpawnTables[id];
        return table.GetRandomCreature();
    }
}