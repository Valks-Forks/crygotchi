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
	[Export] private Node3D IconAdd;
	[Export] private Node3D IconRemoveTile;
	[Export] private Node3D IconRemoveDecoration;

	[ExportGroup("Colors")]
	[Export] private Color NormalColor;
	[Export] private Color HighlightColor;
	[Export] private Color PositiveColor;
	[Export] private Color NegativeColor;

	private AnimationPlayer _animator;
	private CursorState _state;
	private Node3D _parent;

	public override void _Ready()
	{
		base._Ready();

		this._animator = GetNode<AnimationPlayer>("../Mesh/AnimationPlayer");
		this._state = GetNode<CursorState>("../State");
		this._parent = GetNode<Node3D>("..");

		this._animator.Play("Cursor Idle");
	}

	public override void _PhysicsProcess(double delta)
	{
		var pos = this._state.Position;

		//* Should interpolate current position
		this._parent.Position = this._parent.Position.Lerp(
			new Vector3(pos.X * 2, 0f, pos.Y * 2),
			(float)delta * this.Speed
		);
	}
}
