using Godot;

public partial class RoomTileObject : Node3D
{
    private RoomTileInstance Tile;

    public void Setup(RoomTileInstance _tile)
    {
        this.Tile = _tile;
        this.Name = $"{_tile.Position.X},{_tile.Position.Y}";
        this.Position = new Vector3(_tile.Position.X * 2, 0f, _tile.Position.Y * 2);
    }
}
