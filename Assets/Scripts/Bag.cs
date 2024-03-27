using System.Collections.Generic;

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
}