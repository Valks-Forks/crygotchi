using System.Collections.Generic;

using Godot;

public partial class TilesDatabase : Node
{
    public static TilesDatabase Instance { get; private set; }

    private Dictionary<string, RoomTile> _tiles;

    public TilesDatabase() : base()
    {
        TilesDatabase.Instance = this;

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
}
