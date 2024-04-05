using System;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public virtual string ZoneName => "";
    public virtual Dictionary<string, CreatureSpawnTable> ZoneSpawnTables => new ();

    public int GetSpawnedCreatureById(string id)
    // Get spawned creature id from trigger with specified id
    {
        if (!ZoneSpawnTables.ContainsKey(id)) throw new NullReferenceException($"No trigger with id: {id}");

        var table = ZoneSpawnTables[id];
        return table.GetRandomCreature();
    }
}