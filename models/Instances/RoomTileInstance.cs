namespace Crygotchi;

public partial class RoomTileInstance : Resource
{
    public string ID { get; set; }
    public Vector2 Position { get; set; }

    public RoomTile TileEntry { get; set; }

#nullable enable
    public RoomTileDecorationInstance? Decoration { get; set; } = null;
#nullable disable
}
