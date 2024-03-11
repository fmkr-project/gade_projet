using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class CaptureOrb : Item
{
    public float CaptureMultiplier;
    
    public bool TryCapture(Creature enemy)
    {
        // Cf Bulbapedia for the details
        var modifiedCatchRate = enemy.GetModifiedCatchRate(this); // a
        var shakeThreshold = (int) (1048560 / Math.Pow(16711680 / modifiedCatchRate, 0.25)); // b

        for (int i = 0; i < 4; i++)
        {
            var check = (int) new Random().NextDouble() * 65535;
            if (check >= shakeThreshold)
            {
                // Capture failed
                // TODO animate shakes (1 check passed = 1 shake, etc.)
                return false;
            }
            System.Threading.Thread.Sleep(800); // Visual wait
        }
        
        // Capture success
        return true;
    }
}