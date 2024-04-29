using System.Collections.Generic;
using Objects;
using UnityEngine;

public class Bag
{
    public Dictionary<string, int> Contents = new();
    
    public void StoreItem(Item item)
    {
        try
        {
            Contents[item.Name]++;
        }
        catch (KeyNotFoundException exception)
        {
            Contents.Add(item.Name, 1);
        }
    }

    public void TossItem(Item item)
    {
        TossItem(item.Name);
    }

    public void TossItem(string itemName)
    {
        if (!Contents.ContainsKey(itemName)) return;
        if (Contents[itemName] <= 0) return;
        Contents[itemName]--;
        //if (Contents[itemName] <= 0) Contents.Remove(itemName);
    }

    public void UseItem(Item item)
    {
        if (!Contents.ContainsKey(item.Name)) return;
        
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
    
    public void useItem(Item item)
    {
        GameInformation.Bag.removeitem(item);
        //ajouter l'effet de l'item utilisé
    }

    public void removeitem(Item item)
    {
        if (!Contents.ContainsKey(item.Name)) return;
        if (Contents[item.Name] <= 0) return;
        Contents[item.Name]--;
    }
}