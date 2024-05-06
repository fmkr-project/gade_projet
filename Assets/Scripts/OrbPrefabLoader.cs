using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class OrbPrefabLoader
{
    public static Object GetOrbObject(CaptureOrb orb)
    {
        var assetName = orb.GetType().Name;
        var prefab = Resources.Load($"Orbs/{assetName}");

        if (prefab is null)
        {
            throw new NullReferenceException("Orb does not have a valid prefab");
        }

        return prefab;
    }
}