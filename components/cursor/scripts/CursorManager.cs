using System;

using Godot;

public partial class CursorManager : Node
{
    [ExportCategory("General")]
    [ExportGroup("Input")]
    [Export] private float Speed = 25f;
    [ExportGroup("References")]
    [Export] private Node3D IconBase;

    [ExportCategory("Apppearance")]
    [ExportGroup("Icons")]
    [Export] private Node3D IconRemove;
    [Export] private Node3D IconTile;

    [ExportGroup("Colors")]
    [Export] private Color NormalColor;
    [Export] private Color HighlightColor;
    [Export] private Color PositiveColor;
    [Export] private Color NegativeColor;

    private CursorState _cursorState;
    private RoomState _roomState;

    private AnimationPlayer _animator;
    private Node3D _parent;

    public override void _Ready()
    {
        base._Ready();

        this._cursorState = GetNode<CursorState>("/root/CursorState");
        this._roomState = GetNode<RoomState>("/root/RoomState");

        this._animator = GetNode<AnimationPlayer>("../Mesh/AnimationPlayer");
        this._parent = GetNode<Node3D>("..");

        this._animator.Play("Cursor Idle");

        this._cursorState.OnStateChange += OnStateUpdate;
        this._roomState.OnStateChange += OnStateUpdate;

        this.OnStateUpdate(this, null);
    }

    private void OnStateUpdate(object sender, EventArgs e)
    {
        //* Should check what Icon will be showing
        var mode = this._roomState.GetMode();

        if (mode == RoomMode.Exploring)
        {
            //* On exploring mode, for now just hide everything
            GD.Print("State Updated: Exploring mode, ignored");
            return;
        }

        //* On building mode, is focusing a tile?
        var position = this._cursorState.GetPosition();
        var currentHovering = this._roomState.GetTileAt(position);
        if (currentHovering == null)
        {
            //* Nope, not focusing anything
            GD.Print($"State Updated: Building mode at {position} without focus");
            this.IconRemove.Visible = false;
            this.IconTile.Visible = true;
        }
        else
        {
            //* Yep, focusing something
            GD.Print($"State Updated: Building mode at {position} with focus");
            this.IconRemove.Visible = true;
            this.IconTile.Visible = false;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var pos = this._cursorState.GetPosition();

        //* Should interpolate current position
        this._parent.Position = this._parent.Position.Lerp(
            new Vector3(pos.X * 2, 0f, pos.Y * 2),
            (float)delta * this.Speed
        );
    }
}
