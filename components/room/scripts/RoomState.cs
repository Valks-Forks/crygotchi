using System;
using System.Collections.Generic;

using Godot;

public partial class RoomState : Node
{
    private Dictionary<string, RoomTileInstance> _tiles = new();
    private RoomMode _mode = RoomMode.Exploring;
    private RoomTile _selectedTile = null;
    private bool _isSeletorOpen = false;

    public event EventHandler OnStateChange;

    public RoomMode GetMode()
    {
        return this._mode;
    }

    public void SetMode(RoomMode newMode)
    {
        this._mode = newMode;
        this.OnStateChange?.Invoke(this, null);
    }

    public RoomTile GetSelected()
    {
        return this._selectedTile;
    }

    public bool IsSelectorOpen()
    {
        return this._isSeletorOpen;
    }

    public void ToggleSelector()
    {
        this._isSeletorOpen = !this._isSeletorOpen;
        this.OnStateChange?.Invoke(this, null);
    }

    public RoomTileInstance GetTileAt(Vector2 position)
    {
        if (!this._tiles.ContainsKey($"{position.X},{position.Y}")) return null;
        return this._tiles[$"{position.X},{position.Y}"];
    }

    public RoomTileInstance PutTileAtPosition(Vector2 position)
    {
        //* Do not try to put a tile if already exists
        if (this._tiles.ContainsKey($"{position.X},{position.Y}")) return null;

        var tile = new RoomTileInstance()
        {
            ID = "todo",
            Position = position,
        };

        this._tiles[$"{position.X},{position.Y}"] = tile;
        this.OnStateChange?.Invoke(this, null);
        return tile;
    }

    public void RemoveTileAtPosition(Vector2 position)
    {
        if (!this._tiles.ContainsKey($"{position.X},{position.Y}")) return;

        var toRemove = this._tiles[$"{position.X},{position.Y}"];
        this._tiles.Remove($"{position.X},{position.Y}");
        toRemove.Dispose();

        this.OnStateChange?.Invoke(this, null);
    }
}

public enum RoomMode
{
    Exploring,
    Building,
}
