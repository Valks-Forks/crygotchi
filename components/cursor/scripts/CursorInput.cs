using Godot;

public partial class CursorInput : Node
{
    private CursorState _state;

    public override void _Ready()
    {
        base._Ready();
        this._state = GetNode<CursorState>("/root/CursorState");
    }

    public override void _Input(InputEvent @event)
    {
        if (this._state.IsBusy()) return;

        var pos = this._state.GetPosition();

        if (Input.IsActionJustPressed("cursor_up"))
            this._state.SetPosition(new Vector2(pos.X, pos.Y + 1));

        if (Input.IsActionJustPressed("cursor_down"))
            this._state.SetPosition(new Vector2(pos.X, pos.Y - 1));

        if (Input.IsActionJustPressed("cursor_left"))
            this._state.SetPosition(new Vector2(pos.X + 1, pos.Y));

        if (Input.IsActionJustPressed("cursor_right"))
            this._state.SetPosition(new Vector2(pos.X - 1, pos.Y));

        if (Input.IsActionJustPressed("cursor_action_primary"))
            this._state.CursorActionPressed(ActionType.Primary);

        if (Input.IsActionJustPressed("cursor_action_secondary"))
            this._state.CursorActionPressed(ActionType.Secondary);
    }
}
