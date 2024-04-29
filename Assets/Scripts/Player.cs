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
        GameInformation.Bag.StoreItem(new Objects.ClassicOrb());
        GameInformation.Bag.StoreItem(new Objects.ClassicOrb());
        GameInformation.Bag.StoreItem(new Objects.ClassicOrb());
        GameInformation.Bag.StoreItem(new Objects.BetterOrb());
        GameInformation.Bag.StoreItem(new Objects.BetterOrb());
        
        
       
        
        
        Debug.Log(GameInformation.Bag.Contents);
        foreach (var kvp in GameInformation.Bag.Contents)
        {
            Debug.Log("Clé : " + kvp.Key + ", Valeur : " + kvp.Value);
        }
        ;
        

    }

   
}