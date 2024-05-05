using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class OrbPrefabLoader
{
    public static GameObject GetOrbObject(CaptureOrb orb)
    {
        var assetName = orb.GetType().Name;
        var prefab = Resources.Load<GameObject>($"Orbs/{assetName}");

        if (prefab is null)
        {
            throw new NullReferenceException("Orb does not have a valid prefab");
        }

        return prefab;
    }
}