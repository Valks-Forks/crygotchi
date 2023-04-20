using System.Collections.Generic;
using System.Linq;

using Godot;

public partial class TilesDatabase : Node
{
    private Dictionary<string, RoomTile> _tiles;

    public TilesDatabase() : base()
    {
        var loaded = FileSystemUtils.LoadAll<RoomTile>("res://resources/tiles");

        this._tiles = new();
        foreach (var item in loaded)
        {
            var id = ResourceUid.IdToText(ResourceLoader.GetResourceUid(item.ResourcePath));
            var path = item.ResourcePath;

            if (this._tiles.ContainsKey(id))
            {
                GD.PrintErr($"Cannot add duplicated tile \"{id}\" ({path})");
                continue;
            }

            GD.Print($"Loading in tile \"{id}\" ({path})");
            this._tiles.Add(id, item);
        }
    }

    public int ClampTileIndex(int number)
    {
        int amount = this._tiles.Count;
        if (number >= amount) return 0;
        if (number < 0) return amount - 1;

        return number;
    }

    public RoomTile GetTileByIndex(int index)
    {
        return this._tiles.ElementAt(index).Value;
    }
}
