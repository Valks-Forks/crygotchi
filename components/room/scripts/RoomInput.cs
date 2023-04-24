using Godot;

public partial class RoomInput : Node
{
    private RoomState _roomState;
    private RoomGrid _roomGrid;

    public override void _Ready()
    {
        base._Ready();

        this._roomState = this.GetNode<RoomState>("/root/RoomState");
        this._roomGrid = this.GetNode<RoomGrid>("..");
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        var mode = this._roomState.GetMode();

        //* Global inputs
        if (Input.IsActionJustPressed("room_mode_switch")) this._roomGrid.SwitchMode();

        //* Mode specific inputs
        if (mode == RoomMode.Exploring) this.InputExplorerMode();
        if (mode == RoomMode.Building) this.InputBuilderMode();
    }

    private void InputExplorerMode()
    {
        //* Stub implementation
    }

    private void InputBuilderMode()
    {
        //* Primary action puts/remove tiles
        if (Input.IsActionJustPressed("cursor_action_primary")) this._roomGrid.PutTile();

        //* Secondary action puts/remove decorations

        //* Next/previous selectors change what tile is currently selected
        if (Input.IsActionJustPressed("room_mode_tile_next")) this._roomState.NextSelectedTile();
        if (Input.IsActionJustPressed("room_mode_tile_previous")) this._roomState.PreviousSelectedTile();
    }
}
