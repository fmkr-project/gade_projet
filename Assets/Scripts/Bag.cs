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

    public void UseItem(Item item)
    {
        if (!Contents.ContainsKey(item.Name)) return;
        
        // TODO check use (cannot use Balls outside battle)
        if (!item.Use()) return;
        Contents[item.Name]--;
        if (Contents[item.Name] <= 0) Contents.Remove(item.Name);
    }
}