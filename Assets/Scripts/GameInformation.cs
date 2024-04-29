using System;
using UnityEngine;

public class GameInformation
{
    public static Creature Data;
    public static Vector3 PlayerPosition = new Vector3(50.0f, 0.0f, 50.0f);
    public static Vector3 CameraPosition;
    public static Bag Bag = new();
    public static Squad Squad = new();

    // Battle creature data
    public static Creature GetData()
    {
        Debug.Log($"data is {Data.Id}");
        return Data;
    }

    public static void SetData(Creature creature)
    {
        Data = creature;
        if (creature is not null) Debug.Log($"data to {Data.Id}");
    }

    // Player position
    public static Vector3 GetPosition()
    {
        return PlayerPosition;
    }

    public static void SetPosition(Vector3 position)
    {
        PlayerPosition = position;
    }
    
    // Camera position
    public static Vector3 GetCameraPosition()
    {
        return CameraPosition;
    }

    public static void SetCameraPosition(Vector3 position)
    {
        CameraPosition = position;
    }
    
    // Team
    public static int GetBattleReadyCreatureIndex()
    {
        return Squad.GetBattleReadyCreature().Item1;
    }
    
    public static Creature GetBattleReadyCreature()
    {
        return Squad.GetBattleReadyCreature().Item2;
    }
}