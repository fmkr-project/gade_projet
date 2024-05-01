using System.Collections.Generic;
using Objects;
using UnityEngine;

public class Bag
{
    public Dictionary<string, int> PrintedContents = new();
    public Dictionary<string, Item> ItemLink = new();
    // dict to obtain the original object
    // TODO optimize this
    
    public void StoreItem(Item item)
    {
        try
        {
            PrintedContents[item.Name]++;
        }
        catch (KeyNotFoundException exception)
        {
            PrintedContents.Add(item.Name, 1);
            ItemLink.Add(item.Name, item);
        }
    }

    public void TossItem(Item item)
    {
        TossItem(item.Name);
    }

    public bool CanUseItem(Item item)
    {
        if (!PrintedContents.ContainsKey(item.Name)) return false;
        return PrintedContents[item.Name] > 0;
    }
    
    public void TossItem(string itemName)
    {
        if (!PrintedContents.ContainsKey(itemName)) return;
        if (PrintedContents[itemName] <= 0) return;
        PrintedContents[itemName]--;
        //if (Contents[itemName] <= 0) Contents.Remove(itemName);
    }

    public void UseItem(Item item)
    {
        if (!PrintedContents.ContainsKey(item.Name)) return;
        
        // TODO check use (cannot use Balls outside battle)
        if (!item.Use()) return;
        TossItem(item);
    }


    public void killEntity()
    {
        int randomInt = Random.Range(0, 100);
        switch (randomInt)
        {
            case 0:
                GameInformation.Bag.StoreItem(new ClassicOrb());
                break;
            case 1:
                GameInformation.Bag.StoreItem(new BetterOrb());
                break;
            case 2:
                Debug.Log("Cas 2");
                break;
            case 3:
                Debug.Log("Cas 3");
                break;
            default:
                Debug.Log("Valeur non prévue");
                break;
        }
    }
}