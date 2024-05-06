using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public record CaptureOrb : Item
{
    public float CaptureMultiplier;

    public override bool Use()
    {
        return false;
    }
    
    public List<bool> TryCapture(Creature enemy)
    {
        // Cf Bulbapedia for the details
        var res = new List<bool>();
        
        var modifiedCatchRate = enemy.GetModifiedCatchRate(this); // a
        var shakeThreshold = (int) (1048560 / Math.Pow(16711680 / modifiedCatchRate, 0.25)); // b

        for (var i = 0; i < 4; i++)
        {
            var check = (int) (new Random().NextDouble() * 65535);
            Debug.Log($"Capture check {i} > {check} against {shakeThreshold}");
            if (check >= shakeThreshold)
            {
                // Capture failed
                res.Add(false);
                return res;
            }
            res.Add(true);
            //System.Threading.Thread.Sleep(800); // Visual wait
        }
        
        // Capture success
        return res;
    }
}