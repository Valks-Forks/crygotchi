using System;

using Godot;
using Godot.Collections;

public partial class RoomGrid : Node
{
    [ExportCategory("References")]
    [ExportGroup("Objects")]
    [ExportSubgroup("World")]
    [Export] private Node3D Cursor;
    [Export] private Node3D TilesList;
    [ExportSubgroup("UI")]
    [Export] private TextureRect MainIndicator;
    [Export] private TextureRect SubIndicator;
    [Export] private Label TileIndicator;
    [ExportGroup("Templates")]
    [Export] private PackedScene TileTemplate;
    [ExportGroup("Assets")]
    [Export] private Texture2D ExploringSprite;
    [Export] private Texture2D BuildingSprite;

    private Dictionary<string, RoomTileObject> _instances = new();
    private CursorState _cursorState;
    private RoomState _roomState;

    public override void _Ready()
    {
        base._Ready();

        this._cursorState = Cursor.GetNode<CursorState>("/root/CursorState");
        this._roomState = this.GetNode<RoomState>("/root/RoomState");

        this._roomState.OnStateChange += this.OnStateChange;
        this.OnStateChange(this, null);
    }

    public void SwitchMode()
    {
        switch (this._roomState.GetMode())
        {
            case RoomMode.Exploring:
                this._roomState.SetMode(RoomMode.Building);
                break;
            case RoomMode.Building:
                this._roomState.SetMode(RoomMode.Exploring);
                break;
        }
    }

    public void PutTile()
    {
        var position = this._cursorState.GetPosition();
        var key = $"{position.X},{position.Y}";
        var currentHovering = this._roomState.GetTileAt(position);

        if (currentHovering != null)
        {
            //* Shold remove it
            var child = this._instances[key];
            this._roomState.RemoveTileAtPosition(position);
            child.QueueFree();
            this._instances.Remove(key);
            return;
        }

        var tile = this._roomState.PutTileAtPosition(position);
        if (tile == null) return;

        var tileObject = this.TileTemplate.Instantiate<RoomTileObject>();
        tileObject.Setup(tile);
        this._instances[key] = tileObject;

        this.TilesList.AddChild(tileObject);
    }

    private void OnStateChange(object sender, EventArgs e)
    {
        switch (this._roomState.GetMode())
        {
            case RoomMode.Exploring:
                this.MainIndicator.Texture = this.ExploringSprite;
                this.SubIndicator.Texture = null;
                this.TileIndicator.Text = "";
                break;
            case RoomMode.Building:
                this.MainIndicator.Texture = this.BuildingSprite;
                this.SubIndicator.Texture = this._roomState.GetSelected()?.Icon;
                this.TileIndicator.Text = this._roomState.GetSelected()?.Name;
                break;
        }
    }
}
