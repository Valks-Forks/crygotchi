using System;

using Godot;

public partial class CursorState : Node
{
    private Vector2 Position = new Vector2(0, 0);

    public event EventHandler<CursorActionEventArgs> OnAction;
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

    public void CursorActionPressed(ActionType type)
    {
        //* Should propagate that there was a action pressed
        this.OnAction?.Invoke(this, new CursorActionEventArgs() { action = type });
    }
}

public class CursorActionEventArgs : EventArgs
{
    public ActionType action { get; set; }
}

public enum ActionType
{
    Primary = 0,
    Secondary = 1
}
