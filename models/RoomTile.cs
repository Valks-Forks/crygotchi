using Godot;

public partial class RoomTile : Resource
{
    [ExportCategory("Metadata")]
    [Export] public string Name;
    [Export] public string Description;
    [Export] public Texture2D Icon;
    [Export] public TileType Type = TileType.Floor;

    [ExportCategory("Store")]
    [Export] public bool Purchasable = true;
    [Export] public int Cost = 10;

    [ExportCategory("Visual")]
    [Export] public Color Color = new Color() { R = 1, G = 1, B = 1, A = 1 };
}

public enum TileType
{
    Floor,
    Decoration
}
