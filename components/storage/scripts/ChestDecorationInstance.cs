namespace Crygotchi;

using System.Collections.Generic;

public partial class ChestDecorationInstance : RoomTileDecorationInstance
{
    private List<ItemEntry> _items = new();

    public void AddItem(Item item)
    {
        int existingIndex = this._items.FindIndex(x => x.id == item.GetId());
        if (existingIndex != -1)
        {
            //* Already exists, increase the amount
            GD.Print($"Found existing item {item.GetId()}, increasing amount to {this._items[existingIndex].amount + 1}");
            this._items[existingIndex].amount += 1;
            return;
        }

        //* Does not exist, add one
        GD.Print($"Couldn't find existing item {item.GetId()}, adding new");
        this._items.Add(new ItemEntry()
        {
            id = item.GetId(),
            item = item,
            amount = 1
        });
    }

    public ItemEntry[] GetItems()
    {
        return this._items.ToArray();
    }

    public Item TakeItem(string id)
    {
        //* Get the index of it
        int existingIndex = this._items.FindIndex(x => x.id == id);
        if (existingIndex == -1)
        {
            GD.PushError("Could not find item " + id);
            return null;
        }

        //* Grab the item
        var item = this._items[existingIndex].item;

        //* Reduce it's amount and remove if it's empty
        this._items[existingIndex].amount -= 1;
        if (this._items[existingIndex].amount <= 0) this._items.RemoveAt(existingIndex);

        //* Return it from the grabbed instance
        return item;
    }
}

public class ItemEntry
{
    public string id;
    public Item item;
    public int amount;
}
