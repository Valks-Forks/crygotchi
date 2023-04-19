using System;

using Godot;

public partial class RoomSelector : Node
{
    [ExportCategory("References")]
    [ExportGroup("UI")]
    [Export] private AnimationPlayer Animator;
    [Export] private HBoxContainer Items;

    private RoomState _roomState;
    private bool _currentOpenState = false;

    public override void _Ready()
    {
        base._Ready();

        this._roomState = this.GetNode<RoomState>("/root/RoomState");

        this._roomState.OnStateChange += this.OnStateChange;
    }

    private void OnStateChange(object sender, EventArgs e)
    {
        if (this._currentOpenState == this._roomState.IsSelectorOpen()) return;

        if (this._roomState.IsSelectorOpen()) this.Animator.Play("ShowSelector");
        else this.Animator.Play("HideSelector");

        this._currentOpenState = this._roomState.IsSelectorOpen();
    }
}
