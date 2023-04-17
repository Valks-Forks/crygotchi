using System.Collections.Generic;

using Godot;

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
    [ExportGroup("Templates")]
    [Export] private PackedScene TileTemplate;
    [ExportGroup("Assets")]
    [Export] private Texture2D ExploringSprite;
    [Export] private Texture2D BuildingSprite;

    private CursorState _cursorState;
    private RoomState _roomState;

    public override void _Ready()
    {
        base._Ready();

        this._cursorState = Cursor.GetNode<CursorState>("./State");
        this._roomState = this.GetNode<RoomState>("./State");
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (Input.IsActionPressed("cursor_action"))
        {
            switch (this._roomState.GetMode())
            {
                case RoomMode.Building:
                    PutTile();
                    break;
            }
        }

        if (Input.IsActionPressed("room_mode_switch")) SwitchMode();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        switch (this._roomState.GetMode())
        {
            case RoomMode.Exploring:
                this.MainIndicator.Texture = this.ExploringSprite;
                this.SubIndicator.Texture = null;
                break;
            case RoomMode.Building:
                this.MainIndicator.Texture = this.BuildingSprite;
                this.SubIndicator.Texture = this._roomState.GetSelected()?.Icon;
                break;
        }
    }

    private void SwitchMode()
    {
        this._roomState.SetMode(this._roomState.GetMode() == RoomMode.Building ? RoomMode.Exploring : RoomMode.Building);
    }

    private void PutTile()
    {
        var tile = this._roomState.PutTileAtPosition(this._cursorState.Position);
        if (tile == null) return;

        var tileObject = this.TileTemplate.Instantiate<RoomTileObject>();
        tileObject.Setup(tile);

        this.TilesList.AddChild(tileObject);
    }
}
