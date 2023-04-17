using Godot;

public partial class CursorState : Node
{
    public Vector2 Position { get; set; }

    public override void _Ready()
    {
        base._Ready();
        this.Position = new Vector2(0, 0);
    }
}
