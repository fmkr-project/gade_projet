using System.Collections.Generic;

public class Bag
{
    public Dictionary<Item, int> Contents;

    public void StoreItem(Item item)
    {
        Contents[item]++;
    }

    public void UseItem(Item item)
    {
        if (!Contents.ContainsKey(item)) return;
        
        // TODO check use (cannot use Balls outside battle)
        if (!item.Use()) return;
        Contents[item]--;
        if (Contents[item] == 0) Contents.Remove(item);
    }
}