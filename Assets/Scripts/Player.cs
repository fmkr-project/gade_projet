using System;
using System.Collections.Generic;
using Creatures;
using UnityEngine;

public class Player : MonoBehaviour
{
    
   
    [NonSerialized] public int Money;

    private void Awake()
    {
        // debug
        C001 creatureC001 = new C001();
        GameInformation.bag.StoreItem(new Objects.ClassicOrb());
        GameInformation.bag.StoreItem(new Objects.ClassicOrb());
        GameInformation.bag.StoreItem(new Objects.ClassicOrb());
        GameInformation.bag.StoreItem(new Objects.BetterOrb());
        GameInformation.bag.StoreItem(new Objects.BetterOrb());
        
        
       
        
        
        Debug.Log(GameInformation.bag.Contents);
        foreach (var kvp in GameInformation.bag.Contents)
        {
            Debug.Log("Clé : " + kvp.Key + ", Valeur : " + kvp.Value);
        }
        ;
        

    }

   
}