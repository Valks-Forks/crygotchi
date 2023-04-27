using Godot;

public partial class RoomTileObject : Node3D
{
    [Export] private CsgBox3D Mesh;
    [Export] private Node3D DecorationParent;

    private RoomTileInstance TileInstance;
    private TilesDatabase Database;
    private RoomTile Tile;

    public void Setup(RoomTileInstance tile)
    {
        this.TileInstance = tile;
        this.Name = $"{tile.Position.X},{tile.Position.Y}";
        this.Position = new Vector3(tile.Position.X * 2, 0f, tile.Position.Y * 2);

        this.Database = this.GetNode<TilesDatabase>("/root/TilesDatabase");
        if (this.Database == null) throw new System.Exception("Cannot get Tiles Database!");

        this.Tile = this.Database.GetTileById(tile.ID);
        this.SetupPreview(this.Tile, true);
    }

    public void SetupPreview(RoomTile tile, bool setupDecoration = false)
    {
        //* Setup the material
        this.Mesh.Material = new StandardMaterial3D()
        {
            AlbedoColor = tile?.Color ?? new Color(1, 0, 1, 1)
        };

        if (!setupDecoration || this.TileInstance.Decoration == null) return;

        //* Setup the decoration
        var deco = this.Database.GetDecorationById(this.TileInstance.Decoration.ID);
        if (deco == null) return;

        var decoInstance = deco.Mesh.Instantiate();
        this.DecorationParent.AddChild(decoInstance);
    }
}
