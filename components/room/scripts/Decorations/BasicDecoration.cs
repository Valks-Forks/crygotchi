using Godot;

public partial class BasicDecoration : RoomTileDecoration
{
    public override bool IsInteractable => true;

    public override void Interact()
    {
        GD.Print("Yes! It works!");
    }
}
