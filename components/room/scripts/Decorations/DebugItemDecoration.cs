using Godot;

public partial class DebugItemDecoration : RoomTileDecoration
{
    public override bool IsInteractable => true;

    [ExportCategory("Debugging")]
    [Export] public Item ItemToGive;

    private CursorState _cursorState;

    public override RoomTileDecorationInstance CreateInstance()
    {
        return new()
        {
            DecorationEntry = this,
            ID = this._id,
        };
    }

    public override void Interact(RoomTileDecorationInstance instance, Node source)
    {
        //* Get the cursor state if it is missing
        if (this._cursorState == null)
            this._cursorState = source.GetNode<CursorState>("/root/CursorState");

        //* Is cursor holding something? If yes, do nothing
        if (this._cursorState.IsHoldingItem()) return;

        //* If no, give it a item
        this._cursorState.HoldItem(this.ItemToGive);
    }
}
