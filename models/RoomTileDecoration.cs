using Godot;

public abstract partial class RoomTileDecoration : Resource
{
    [ExportCategory("Metadata")]
    [Export] public string Name;
    [Export] public string Description;
    [Export] public Texture2D Icon;

    [ExportCategory("Store")]
    [Export] public bool Purchasable = true;
    [Export] public int Cost = 10;

    [ExportCategory("World")]
    [ExportGroup("Visual")]
    [Export] public PackedScene Mesh;

    public abstract bool IsInteractable { get; }
    private string _id;

    public RoomTileDecoration Setup(string id)
    {
        this._id = id;
        return this;
    }

    public virtual void Interact()
    {
        //* Stub implementation
    }

    public string GetId()
    {
        return this._id;
    }
}
