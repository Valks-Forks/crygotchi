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
        if (mode == RoomMode.Decorating) this.InputDecoratorMode();
    }

    private void InputExplorerMode()
    {
        //* Stub implementation
    }

    private void InputBuilderMode()
    {
        //* Next/previous selectors change what tile is currently selected
        if (Input.IsActionJustPressed("room_mode_tile_next")) this._roomState.NextSelectedBuilding();
        if (Input.IsActionJustPressed("room_mode_tile_previous")) this._roomState.PreviousSelectedBuilding();
    }

    private void InputDecoratorMode()
    {
        //* Next/previous selectors change what tile is currently selected
        if (Input.IsActionJustPressed("room_mode_tile_next")) this._roomState.NextSelectedDecorating();
        if (Input.IsActionJustPressed("room_mode_tile_previous")) this._roomState.PreviousSelectedDecorating();
    }
}
