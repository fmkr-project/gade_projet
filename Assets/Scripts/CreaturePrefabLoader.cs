using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class CreaturePrefabLoader
{
    public Object GetPrefabFromId(int creatureId)
    // Return the prefab of a creature given an ID
    {
        var assetName = creatureId.ToString("D3");
        var prefab = Resources.Load($"Creatures/{assetName}/{assetName}");

        if (prefab is null)
        {
            throw new NullReferenceException($"Creature {assetName} does not have a valid prefab!");
        }

        return prefab;
    }
}