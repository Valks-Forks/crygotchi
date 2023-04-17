using Godot;

public partial class CursorInput : Node
{
    private CursorState _state;

    public override void _Ready()
    {
        base._Ready();
        this._state = GetNode<CursorState>("../State");
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("cursor_up"))
            this._state.Position = new Vector2(this._state.Position.X, this._state.Position.Y + 1);

        if (Input.IsActionJustPressed("cursor_down"))
            this._state.Position = new Vector2(this._state.Position.X, this._state.Position.Y - 1);

        if (Input.IsActionJustPressed("cursor_left"))
            this._state.Position = new Vector2(this._state.Position.X + 1, this._state.Position.Y);

        if (Input.IsActionJustPressed("cursor_right"))
            this._state.Position = new Vector2(this._state.Position.X - 1, this._state.Position.Y);
    }
}
