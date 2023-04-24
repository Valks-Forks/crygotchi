using System;

using Godot;

public partial class CursorManager : Node
{
    [ExportCategory("General")]
    [ExportGroup("Input")]
    [Export] private float Speed = 25f;
    [ExportGroup("References")]
    [Export] private Node3D IconBase;
    [Export] private Node3D CursorBase;

    [ExportCategory("Apppearance")]
    [ExportGroup("Icons")]
    [Export] private Node3D IconRemove;
    [Export] private RoomTileObject IconTile;

    [ExportGroup("Colors")]
    [Export] private Color NormalColor;
    [Export] private Color HighlightColor;
    [Export] private Color PositiveColor;
    [Export] private Color NegativeColor;

    private CursorState _cursorState;
    private RoomState _roomState;

    private MeshInstance3D _cursorIndicator;
    private MeshInstance3D _cursorBorder;
    private StandardMaterial3D _cursorMat;
    private Color _targetColor;

    private AnimationPlayer _animator;
    private Node3D _parent;

    public override void _Ready()
    {
        base._Ready();

        this._cursorState = GetNode<CursorState>("/root/CursorState");
        this._roomState = GetNode<RoomState>("/root/RoomState");

        this._animator = GetNode<AnimationPlayer>("../Mesh/AnimationPlayer");
        this._parent = GetNode<Node3D>("..");

        this._cursorIndicator = this.CursorBase.GetNode<MeshInstance3D>("./Cursor Walls");
        this._cursorBorder = this.CursorBase.GetNode<MeshInstance3D>("./Cursor Indicator");

        this._cursorMat = new StandardMaterial3D();
        this._cursorIndicator.MaterialOverride = this._cursorMat;
        this._cursorBorder.MaterialOverride = this._cursorMat;

        this._animator.Play("Cursor Idle");

        this._cursorState.OnStateChange += OnStateUpdate;
        this._roomState.OnStateChange += OnStateUpdate;

        this.OnStateUpdate(this, null);
    }

    private void OnStateUpdate(object sender, EventArgs e)
    {
        //* Should check what Icon will be showing
        var mode = this._roomState.GetMode();
        var position = this._cursorState.GetPosition();
        var currentHovering = this._roomState.GetTileAt(position);
        var currentSelected = this._roomState.GetSelected();

        //* Update the texture color of the icon tile
        if (currentSelected != null) this.IconTile.SetupPreview(currentSelected);

        //* On exploring mode, hide icons
        if (mode == RoomMode.Exploring)
        {
            this.IconRemove.Visible = false;
            this.IconTile.Visible = false;

            this._targetColor = currentHovering == null ? NormalColor : HighlightColor;
            return;
        }

        //* On building mode, is focusing a tile?
        if (currentHovering == null)
        {
            //* Nope, not focusing anything
            this._targetColor = PositiveColor;
            this.IconRemove.Visible = false;
            this.IconTile.Visible = true;
        }
        else
        {
            //* Yep, focusing something
            this._targetColor = NegativeColor;
            this.IconRemove.Visible = true;
            this.IconTile.Visible = false;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var pos = this._cursorState.GetPosition();

        //* Interpolate current position
        this._parent.Position = this._parent.Position.Lerp(
            new Vector3(pos.X * 2, 0f, pos.Y * 2),
            (float)delta * this.Speed
        );

        //* Interpolate target color
        this._cursorMat.AlbedoColor = this._cursorMat.AlbedoColor.Lerp(
            this._targetColor,
            (float)delta * 5f
        );
    }
}
