using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Creature> Creatures;
    public Bag Bag = new();
    [NonSerialized] public int Money;

    private void Awake()
    {
        // debug
        Bag.StoreItem(new Objects.ClassicOrb());
        Bag.StoreItem(new Objects.ClassicOrb());
        Bag.StoreItem(new Objects.ClassicOrb());
        Bag.StoreItem(new Objects.ClassicOrb());
        Bag.StoreItem(new Objects.ClassicOrb());
        Bag.StoreItem(new Objects.ClassicOrb());
        Bag.StoreItem(new Objects.BetterOrb());
        Bag.StoreItem(new Objects.BetterOrb());
        Bag.StoreItem(new Objects.BetterOrb());
        Bag.StoreItem(new Objects.BetterOrb());
    }
}