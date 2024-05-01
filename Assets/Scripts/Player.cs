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
        // Initialize the bag if there is nothing in it
        // TODO improve this
        if (GameInformation.Bag.PrintedContents.Count == 0)
        {
            GameInformation.Bag.StoreItem(new Objects.ClassicOrb());
            GameInformation.Bag.StoreItem(new Objects.ClassicOrb());
            GameInformation.Bag.StoreItem(new Objects.ClassicOrb());
            GameInformation.Bag.StoreItem(new Objects.BetterOrb());
            GameInformation.Bag.StoreItem(new Objects.BetterOrb());
            GameInformation.Bag.StoreItem(new Objects.Potion());
            GameInformation.Bag.StoreItem(new Objects.Potion());
        }

        Debug.Log(GameInformation.Bag.PrintedContents);
        foreach (var kvp in GameInformation.Bag.PrintedContents)
        {
            Debug.Log("Clé : " + kvp.Key + ", Valeur : " + kvp.Value);
        }
        
        

    }

   
}