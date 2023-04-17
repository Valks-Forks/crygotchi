using System.Collections.Generic;

using Godot;

public partial class RoomState : Node
{
    private Dictionary<Vector2, RoomTileInstance> _tiles = new();
    private RoomMode _mode = RoomMode.Exploring;
    private RoomTile _selectedTile = null;

    public RoomMode GetMode()
    {
        return this._mode;
    }

    public void SetMode(RoomMode newMode)
    {
        this._mode = newMode;
    }

    public RoomTile GetSelected()
    {
        return this._selectedTile;
    }

    public RoomTileInstance PutTileAtPosition(Vector2 position)
    {
        //* Do not try to put a tile if already exists
        if (this._tiles.ContainsKey(position)) return null;

        var tile = new RoomTileInstance()
        {
            ID = "todo",
            Position = position,
        };

        return tile;
    }
}

public enum RoomMode
{
    Exploring,
    Building,
}
