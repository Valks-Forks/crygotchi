using Godot;

public partial class RoomTileObject : Node3D
{
    [Export] private CsgBox3D Mesh;

    private RoomTileInstance TileInstance;
    private RoomTile Tile;

    public void Setup(RoomTileInstance tile)
    {
        this.TileInstance = tile;
        this.Name = $"{tile.Position.X},{tile.Position.Y}";
        this.Position = new Vector3(tile.Position.X * 2, 0f, tile.Position.Y * 2);

        var db = this.GetNode<TilesDatabase>("/root/TilesDatabase");
        if (db == null) throw new System.Exception("Cannot get Tiles Database!");

        this.Tile = db.GetTileById(tile.ID);
        this.Mesh.Material = new StandardMaterial3D()
        {
            AlbedoColor = this.Tile?.Color ?? new Color(1, 0, 1, 1)
        };
    }

    public void SetupPreview(RoomTile tile)
    {
        this.Mesh.Material = new StandardMaterial3D()
        {
            AlbedoColor = tile?.Color ?? new Color(1, 0, 1, 1)
        };
    }
}
