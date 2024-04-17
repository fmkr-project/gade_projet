using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    void Awake()
    {
        var spawnLayerMask = 1 << LayerMask.NameToLayer("Spawns");
        
        Collider[] hits = new Collider[1];
        var size = Physics.OverlapSphereNonAlloc(transform.position, 0.05f, hits, spawnLayerMask);
        if (size == 0) // trigger is not near a spawnable zone
        {
            Destroy(gameObject);
        }
    }
}
