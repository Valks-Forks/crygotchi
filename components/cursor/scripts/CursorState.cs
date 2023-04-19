using System;

using Godot;

public partial class CursorState : Node
{
    private Vector2 Position = new Vector2(0, 0);

    public event EventHandler OnStateChange;

    public Vector2 GetPosition()
    {
        return Position;
    }

    public void SetPosition(Vector2 newPosition)
    {
        this.Position = newPosition;
        this.OnStateChange?.Invoke(this, null);
    }
}
